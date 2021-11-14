using Domain.Common;
using Domain.Enums;
using System.Collections.Generic;

namespace Domain.Models
{
    public class Student : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UniversityYear UniversityYear { get; set; }
        public List<string> Courses { get; set; }
    }
}
