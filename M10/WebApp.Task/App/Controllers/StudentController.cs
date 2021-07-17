using App.Domain.core.Models;
using App.Domain.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using App.AppModels;
using App.AppPostModels;

namespace App.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly ILogger<StudentController> _logger;
        private readonly IGenericBaseRepository<Student> _repo;
        private readonly IMapper _mapper;

        public StudentController(ILogger<StudentController> logger, IGenericBaseRepository<Student> repoRepository, IMapper mapper)
        {
            _logger = logger;
            _repo = repoRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<StudentApp>> Get()
        {
            var studentsList = _mapper.Map<IEnumerable<Student>,List<StudentApp>>(_repo.Get());
        
            return Ok(studentsList);
        }

        [HttpGet("{id:int}")]
        public ActionResult<StudentApp> Get(int id)
        {
            var student = _mapper.Map<StudentApp>( _repo.GetById(id));

            return Ok(student);
        }

        [HttpPost]
        public ActionResult<Student> Post(StudentAppPost student)
        {
            var createdStudent = _repo.Create(_mapper.Map<Student>(student));

            return Created(nameof(Post), createdStudent);
        }

        [HttpPut]
        public IActionResult Put(StudentApp studentInput)
        {
            var student = _mapper.Map<Student>(studentInput);
            _repo.Update(student);

            return Ok();
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            _repo.Remove(id);

            return Ok();
        }
    }
}
