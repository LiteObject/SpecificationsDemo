namespace SpecificationsDemo.Specifications
{
    using System;
    using System.Linq.Expressions;

    using SpecificationsDemo.Entities;
    using SpecificationsDemo.Specifications.Infrastructure;

    /// <summary>
    /// The new user specification.
    /// </summary>
    public class NewUserSpecification : BaseSpecification<User>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NewUserSpecification"/> class.
        /// </summary>
        public NewUserSpecification()
        {
            this.Criteria = userRecord => userRecord.EndDate == null && userRecord.StartDate > DateTime.Now.AddDays(-360);
        }
    }
}
