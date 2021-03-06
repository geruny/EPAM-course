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
    public class HomeworkController : ControllerBase
    {
        private readonly ILogger<HomeworkController> _logger;
        private readonly IGenericBaseRepository<Homework> _repo;
        private readonly IMapper _mapper;

        public HomeworkController(ILogger<HomeworkController> logger, IGenericBaseRepository<Homework> repository, IMapper mapper)
        {
            _logger = logger;
            _repo = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<HomeworkApp>> Get()
        {
            var homeworksList = _mapper.Map<IEnumerable<Homework>, List<HomeworkApp>>(_repo.Get());

            return Ok(homeworksList);
        }

        [HttpGet("{id:int}")]
        public ActionResult<HomeworkApp> Get(int id)
        {
            var homework = _mapper.Map<HomeworkApp>(_repo.GetById(id));

            return Ok(homework);
        }

        [HttpPost]
        public ActionResult<Homework> Post(HomeworkAppPost homework)
        {
            var createdHomework = _repo.Create(_mapper.Map<Homework>(homework));

            return Created(nameof(Post), createdHomework);
        }

        [HttpPut]
        public IActionResult Put(HomeworkApp homeworkInput)
        {
            var homework = _mapper.Map<Homework>(homeworkInput);
            _repo.Update(homework);

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
