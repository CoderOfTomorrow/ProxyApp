using Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace ServerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        [HttpGet]
        public List<string> GetCourses()
        {
            List<string> courses = new();

            foreach (var course in Enum.GetValues(typeof(Course)))
            {
                courses.Add(course.ToString());
            }

            return courses;
        }
    }
}
