namespace SpecificationsDemo.Specifications
{
    using SpecificationsDemo.Entities;
    using SpecificationsDemo.Specifications.Infrastructure;

    /// <summary>
    /// The daily updated records.
    /// </summary>
    public class ActiveUserSpecification : BaseSpecification<User>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ActiveUserSpecification"/> class.
        /// </summary>
        public ActiveUserSpecification()
        {
            this.Criteria = userRecord => userRecord.EndDate == null;
        }
    }
}
