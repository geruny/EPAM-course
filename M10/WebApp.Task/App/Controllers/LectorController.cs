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
    public class LectorController : ControllerBase
    {
        private readonly ILogger<Lector> _logger;
        private readonly IGenericBaseRepository<Lector> _repo;
        private readonly IMapper _mapper;

        public LectorController(ILogger<Lector> logger, IGenericBaseRepository<Lector> repository, IMapper mapper)
        {
            _logger = logger;
            _repo = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<LectorApp>> Get()
        {
            var lectorsList = _mapper.Map<IEnumerable<Lector>,List<LectorApp>>(_repo.Get());

            return Ok(lectorsList);
        }

        [HttpGet("{id:int}")]
        public ActionResult<LectorApp> Get(int id)
        {
            var lector = _mapper.Map<LectorApp>(_repo.GetById(id));

            return Ok(lector);
        }

        [HttpPost]
        public ActionResult<Lector> Post(LectorAppPost lector)
        {
            var createdLector = _repo.Create(_mapper.Map<Lector>(lector));

            return Created(nameof(Post), createdLector);
        }

        [HttpPut]
        public IActionResult Put(LectorApp lectorInput)
        {
            var lector = _mapper.Map<Lector>(lectorInput);
            _repo.Update(lector);

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
