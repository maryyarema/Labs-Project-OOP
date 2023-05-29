using Microsoft.AspNetCore.Mvc;
using MovieManagement.BLL.DTO;
using MovieManagement.BLL.Services;
using MovieManagement.BLL.Services.Consracts;
using MovieManagement.DAL.Repositories.Contracts;



namespace MovieManagement.API.Controllers
{
    [Route("ado/[controller]")]
    [ApiController]
    public class ActorController : ControllerBase
    {
        IActorService _actorService;

        public ActorController(IActorService ActorService)
        {
            _actorService = ActorService;
        }



        [HttpGet] // GET: ado/Actor
        public async Task<ActionResult<IEnumerable<ActorDTO>>> GetAllAsync()
        {
            try
            {
                var result = await _actorService.GetAllAsync();
                Console.WriteLine("All Actors were successfully extracted from [Actors]");

                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in [ActorConstoller]->[GetAllAsync]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")] // GET: ado/Actor/id
        public async Task<ActionResult<ActorDTO>> GetByIdAsync(int id)
        {
            try
            {
                var result = await _actorService.GetAsync(id); // чи взагалі є такий запис в БД

                if (result == null)
                {
                    Console.WriteLine($"Actor {id} from [Actors] not found");
                    return NotFound();
                }
                else
                {
                    Console.WriteLine($"Actor {result.actor_id} were successfully extracted from [Actors]");
                    return Ok(result);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in [ActorController]->[GetByIdAsync]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }




        [HttpPost] // POST: ado/Actor
        public async Task<ActionResult> AddAsync(ActorDTO newActor)
        {
            try
            {
                // Чи введені валідні данні
                if (newActor.name == null)
                {
                    return BadRequest("Invalid information");
                }
                else
                {
                    var id = await _actorService.CreateAsync(newActor);
                    Console.WriteLine($"Actor {id} successfully added to [Actors]");

                    return Ok(id);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in [ActorConstoller]->[AddAsync]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut] // PUT: ado/Actor
        public async Task<ActionResult> UpdateAsync(ActorDTO upActor)
        {
            try
            {
                // Чи введені валідні данні
                if (upActor.name == null)
                {
                    return BadRequest("Invalid information");
                }
                else
                {
                    var result = await _actorService.GetAsync(upActor.actor_id); // чи взагалі є такий запис в БД

                    if (result == null)
                    {
                        Console.WriteLine($"Actor {upActor.actor_id} from [Actors] not found");
                        return NotFound();
                    }
                    else
                    {
                        await _actorService.UpdateAsync(upActor);
                        Console.WriteLine($"Actor {upActor.actor_id} successfully update to [Actors]");

                        return Ok();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in [ActorConstoller]->[UpdateAsync]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")] // DELETE: ado/actor/id
        public async Task<ActionResult> DeleteByIdAsync(int id)
        {
            try
            {
                var result = await _actorService.GetAsync(id); 

                if (result == null)
                {
                    Console.WriteLine($"Actor {id} from [Actors] not found");
                    return NotFound();
                }
                else
                {
                    await _actorService.DeleteAsync(id);
                    Console.WriteLine($"Actor {id} successfully deleted to [Actors]");

                    return Ok();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in [ActorConstoller]->[UpdateAsync]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
