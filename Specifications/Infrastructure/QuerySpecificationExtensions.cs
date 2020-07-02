namespace SpecificationsDemo.Specifications.Infrastructure
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// The query specification extensions.
    /// Original Source: https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/infrastructure-persistence-layer-implemenation-entity-framework-core
    /// </summary>
    public static class QuerySpecificationExtensions
    {
        /// <summary>
        /// The specify.
        /// </summary>
        /// <param name="query">
        /// The query.
        /// </param>
        /// <param name="spec">
        /// The spec.
        /// </param>
        /// <typeparam name="T">
        /// The type.
        /// </typeparam>
        /// <returns>
        /// The <see cref="IQueryable"/>.
        /// </returns>
        public static IQueryable<T> Specify<T>(this IQueryable<T> query, ISpecification<T> spec)
            where T : class
        {
            // Fetch a Queryable that includes all expression-based includes
            IQueryable<T> queryableResultWithIncludes = spec.Includes.Aggregate(
                query,
                (current, include) => current.Include(include));

            // Modify the IQueryable to include any string-based include statements
            IQueryable<T> secondaryResult = spec.IncludeStrings.Aggregate(
                queryableResultWithIncludes,
                (current, include) => current.Include(include));

            // Return the result of the query using the specification's criteria expression
            return secondaryResult.Where(spec.Criteria);
        }
    }
}
