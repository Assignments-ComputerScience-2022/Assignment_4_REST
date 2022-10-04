using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment_Rest.Managers;
using Assignment1_UnitTests;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assignment_Rest.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FootballPlayersController : ControllerBase
    {
        
        private readonly FootballPlayersManager _playersManager;

        public FootballPlayersController()
        {
            _playersManager = new FootballPlayersManager();
        }
        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<IEnumerable<FootballPlayer>> GetAll()
        {
            var result = _playersManager.GetAll();
            if (result.Any()) return Ok(result);
            return NoContent();
        }
        
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id:int}", Name = "Get")]
        public ActionResult<FootballPlayer?> Get(int id)
        {
            FootballPlayer? player = _playersManager.GetById(id);
            if (player == null) return NotFound("The player was not found, id: " + id);
            return Ok(player);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public ActionResult<FootballPlayer> Post([FromBody] FootballPlayer? value)
        {
            if (value == null) return BadRequest("The player is null");
            return Ok(CreatedAtAction(nameof(Get), new { id = value.Id }, _playersManager.Add(value)));
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("{id:int}")]
        public ActionResult<FootballPlayer> Put(int id, [FromBody] FootballPlayer value)
        {
            try
            {
                FootballPlayer? result = _playersManager.Update(id, value);
                if (result == null) return NotFound("The player was not found, id: " + id); 
                return Ok(result);
            }
            catch (Exception ex)
                when (ex is ArgumentNullException || ex is ArgumentOutOfRangeException)
            {
                return BadRequest(ex.Message); 
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id:int}")]
        public ActionResult<FootballPlayer?> Delete(int id)
        {
            FootballPlayer? result = _playersManager.Delete(id);
            if (result == null) return NotFound("The player was not found, id: " + id);
            return Ok(result);
        }
        
        
    }
}
