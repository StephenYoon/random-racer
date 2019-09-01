using System;
using System.Collections.Generic;

namespace MetricsApi.EntityModels
{
    public partial class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? LastUpdated { get; set; }
        public DateTime? LastLogin { get; set; }
    }
}
