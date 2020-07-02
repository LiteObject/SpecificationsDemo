namespace SpecificationsDemo.Specifications.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// The base specification.
    /// Original Source: https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/infrastructure-persistence-layer-implemenation-entity-framework-core
    /// </summary>
    /// <typeparam name="T">
    /// The type.
    /// </typeparam>
    public abstract class BaseSpecification<T> : ISpecification<T>
    {
        /// <summary>
        /// Gets or sets the criteria.
        /// </summary>
        public Expression<Func<T, bool>> Criteria { get; set; }

        /// <summary>
        /// Gets the includes.
        /// </summary>
        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();

        /// <summary>
        /// Gets the include strings.
        /// </summary>
        public List<string> IncludeStrings { get; } = new List<string>();

        /// <summary>
        /// The add include.
        /// </summary>
        /// <param name="includeExpression">
        /// The include expression.
        /// </param>
        protected virtual void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }

        /// <summary>
        /// The add include.
        /// </summary>
        /// <param name="includeString">
        /// The include string.
        /// </param>
        protected virtual void AddInclude(string includeString)
        {
            IncludeStrings.Add(includeString);
        }
    }
}
