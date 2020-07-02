namespace SpecificationsDemo.Data
{
    using Microsoft.EntityFrameworkCore;

    using SpecificationsDemo.Entities;

    /// <summary>
    /// The user db context.
    /// </summary>
    public class UserDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserDbContext"/> class.
        /// </summary>
        public UserDbContext()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserDbContext"/> class.
        /// </summary>
        /// <param name="options">
        /// The options.
        /// </param>
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the users.
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// The on configuring.
        /// </summary>
        /// <param name="optionsBuilder">
        /// The options builder.
        /// </param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(
                "Server=(localdb)\\mssqllocaldb;Database=SpecificationsDemo-local;Trusted_Connection=True;MultipleActiveResultSets=true",
                option =>
                    {
                        option.EnableRetryOnFailure();
                    });

            optionsBuilder.EnableSensitiveDataLogging();
        }
    }
}
