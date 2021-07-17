using App.Domain.core.Models;
using App.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using App.AppModels;
using App.AppPostModels;
using AutoMapper;

namespace App.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LectureController : ControllerBase
    {
        private readonly ILogger<LectureController> _logger;
        private readonly IGenericBaseRepository<Lecture> _repo;
        private readonly IMapper _mapper;

        public LectureController(ILogger<LectureController> logger, IGenericBaseRepository<Lecture> repository, IMapper mapper)
        {
            _logger = logger;
            _repo = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<LectureApp>> Get()
        {
            var lecturesList =_mapper.Map<IEnumerable<Lecture>,List<LectureApp>>( _repo.Get());

            return Ok(lecturesList);
        }

        [HttpGet("{id:int}")]
        public ActionResult<LectureApp> Get(int id)
        {
            var lecture =_mapper.Map<LectureApp>(_repo.GetById(id));

            return Ok(lecture);
        }

        [HttpPost]
        public ActionResult<Lecture> Post(LectureAppPost lecture)
        {
            var createdLecture = _repo.Create(_mapper.Map<Lecture>(lecture));

            return Created(nameof(Post), createdLecture);
        }

        [HttpPut]
        public IActionResult Put(LectureApp lectureInput)
        {
            var lecture = _mapper.Map<Lecture>(lectureInput);
            _repo.Update(lecture);

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
