using Domain.Enums;
using Domain.Models;
using MessageBus;
using Microsoft.AspNetCore.Mvc;
using Persistence.Abstractions;
using Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.Json;

namespace ServerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IRepository<Student> repository;
        private readonly SyncService<Student> syncService;
        public StudentsController(IRepository<Student> repository, SyncService<Student> syncService)
        {
            this.syncService = syncService;
            this.repository = repository;
        }

        [HttpGet]
        [Route("all")]
        public async Task<List<StudentDto>> GetStudents()
        {
            var students = await repository.GetEntities();
            List<StudentDto> studentsDto = new();

            foreach (var obj in students)
            {
                var studentDto = new StudentDto()
                {
                    Id = obj.Id,
                    FirstName = obj.FirstName,
                    LastName = obj.LastName,
                    UniversityYear = obj.UniversityYear.ToString(),
                    Courses = obj.Courses
                };
                studentsDto.Add(studentDto);
            }

            return studentsDto;
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<StudentDto> GetStudent(string id)
        {
            var student = await repository.GetEntity(Guid.Parse(id));
            var studentDto = new StudentDto()
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                UniversityYear = student.UniversityYear.ToString(),
                Courses = student.Courses
            };

            return studentDto;
        }

        [HttpPost]
        public async Task<IActionResult> AddStudent([FromBody] StudentDto entity)
        {
            var student = new Student()
            {
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                UniversityYear = (UniversityYear)Enum.Parse(typeof(UniversityYear), entity.UniversityYear),
                Courses = entity.Courses
            };

            if (await repository.AddEntity(student))
            {
                await syncService.Upsert(student);
                return Ok();
            }
            else
                return BadRequest();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteStudent(Guid id)
        {

            if (await repository.DeleteEntity(id))
            {
                var student = await repository.GetEntity(id);
                await syncService.Delete(student);
                return Ok();
            }
            else
                return BadRequest();
        }
    }
}
