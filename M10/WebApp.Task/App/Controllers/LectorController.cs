using App.Domain.core.Models;
using App.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
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
        public ActionResult<IEnumerable<Lector>> Get()
        {
            var lectorsList = _repo.Get();
            if (!lectorsList.Any())
                return NotFound();

            return Ok(lectorsList);
        }

        [HttpGet("{id}")]
        public ActionResult<Lector> Get(int id)
        {
            var lector = _repo.GetById(id);
            if (lector == null)
                return NotFound();

            return Ok(lector);
        }

        [HttpPost]
        public ActionResult<Lector> Post(LectorAppPost lector)
        {
            var createdLector = _repo.Create(_mapper.Map<Lector>(lector));
            if (createdLector == null)
                return BadRequest();

            return Created(nameof(Post), createdLector);
        }

        [HttpPut]
        public IActionResult Put(Lector lector)
        {
            if (_repo.GetById(lector.Id) == null)
                return NotFound();

            _repo.Update(lector);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (_repo.GetById(id) == null)
                return NotFound();

            _repo.Remove(id);
            return Ok();
        }
    }
}
