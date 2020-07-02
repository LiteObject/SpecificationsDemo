namespace SpecificationsDemo
{
    using System;
    using System.Linq;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    using SpecificationsDemo.Data;
    using SpecificationsDemo.Entities;
    using SpecificationsDemo.Specifications;
    using SpecificationsDemo.Specifications.Infrastructure;

    /// <summary>
    /// The program.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The services.
        /// </summary>
        private static readonly IServiceProvider Services;

        /// <summary>
        /// The Program logger.
        /// </summary>
        private static readonly ILogger<Program> Logger;

        /// <summary>
        /// Initializes static members of the <see cref="Program"/> class.
        /// </summary>
        static Program()
        {
            var serviceCollection = new ServiceCollection();
            string[] arguments = Environment.GetCommandLineArgs();
            Configure(serviceCollection, arguments);

            Services = serviceCollection.BuildServiceProvider();
            Logger = Services.GetRequiredService<ILogger<Program>>();
        }

        /// <summary>
        /// The main.
        /// </summary>
        /// <param name="args">
        /// The args.
        /// </param>
        private static void Main(string[] args)
        {
            UserDbContext db = Services.GetRequiredService<UserDbContext>();

            /*var user = new User
                           {
                               FirstName = "Lucas",
                               LastName = "Campbell",
                               Email = "Lucas.Campbell@email.com",
                               StartDate = DateTime.Now.AddDays(-120),
                               EndDate = DateTime.Today
                           };
            db.Add(user);
            db.SaveChanges(); */
            
            // var users = db.Users.Where(u => u.EndDate == null && u.StartDate > DateTime.Today.AddDays(-360)).ToList();
            var users = db.Users.Specify(new ActiveUserSpecification()).ToList();

            foreach (User user in users)
            {
                Console.WriteLine($"\n\t{user.Email}\n");
            }
        }

        /// <summary>
        /// The configure.
        /// </summary>
        /// <param name="services">
        /// The services.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        private static void Configure(IServiceCollection services, string[] args)
        {
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            IConfigurationBuilder configBuilder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environmentName}.json", true, true)
                .AddEnvironmentVariables();

            services.AddLogging(
                builder =>
                    {
                        builder.ClearProviders();
                        builder.SetMinimumLevel(LogLevel.Trace);
                        builder.AddConsole();
                    });

            ILoggerFactory loggerFactory = services.BuildServiceProvider().GetService<ILoggerFactory>();
            var connectionString =
                "Server=(localdb)\\mssqllocaldb;Database=SpecificationsDemo-local;Trusted_Connection=True;MultipleActiveResultSets=true";



            services.AddScoped<UserDbContext>(
                provider =>
                    {
                        var optionsBuilder = new DbContextOptionsBuilder<UserDbContext>();
                        optionsBuilder.EnableSensitiveDataLogging();
                        optionsBuilder.UseLoggerFactory(loggerFactory);
                        optionsBuilder.UseSqlServer(
                            connectionString,
                            option =>
                                {
                                    option.EnableRetryOnFailure();
                                });

                        return new UserDbContext(optionsBuilder.Options);

                    });
        }
    }
}
