using System;
using System.Collections.Generic;

namespace Shared
{
    public class StudentDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UniversityYear { get; set; }
        public List<string> Courses { get; set; }
    }
}
