using Microsoft.AspNetCore.Mvc;
using MovieManagement.BLL.DTO;
using MovieManagement.BLL.Services;
using MovieManagement.BLL.Services.Consracts;



namespace MovieManagement.API.Controllers
{
    [Route("ado/[controller]")]
    [ApiController]
    public class MovieActorController : ControllerBase
    {
        IMovieActorService _movieActorService;

        public MovieActorController(IMovieActorService MovieActorService)
        {
            _movieActorService = MovieActorService;
        }



        [HttpGet] // GET: ado/author
        public async Task<ActionResult<IEnumerable<MovieActorDTO>>> GetAllAsync()
        {
            try
            {
                var result = await _movieActorService.GetAllAsync();
                Console.WriteLine("All MovieActors were successfully extracted from [MovieActors]");

                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in [MovieActorConstoller]->[GetAllAsync]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")] // GET: ado/author/id
        public async Task<ActionResult<MovieActorDTO>> GetByIdAsync(int id)
        {
            try
            {
                var result = await _movieActorService.GetAsync(id); // чи взагалі є такий запис в БД

                if (result == null)
                {
                    Console.WriteLine($"MovieActor {id} from [MovieActors] not found");
                    return NotFound();
                }
                else
                {
                    Console.WriteLine($"MovieActor {result.actor_id} were successfully extracted from [MovieActors]");
                    return Ok(result);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in [MovieActorController]->[GetByIdAsync]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost] // POST: ado/MovieActor
        public async Task<ActionResult> AddAsync(MovieActorDTO newMovieActor)
        {
            try
            {
                // Чи введені валідні данні
                if (newMovieActor.actor_id == 0)
                {
                    return BadRequest("Invalid information");
                }
                else
                {
                    var id = await _movieActorService.CreateAsync(newMovieActor);
                    Console.WriteLine($"MovieActor {id} successfully added to [MovieActors]");

                    return Ok(id);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in [MovieActorConstoller]->[AddAsync]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut] // PUT: ado/MovieActor
        public async Task<ActionResult> UpdateAsync(MovieActorDTO upMovieActor)
        {
            try
            {
                // Чи введені валідні данні
                if (upMovieActor.actor_id == 0)
                {
                    return BadRequest("Invalid information");
                }
                else
                {
                    var result = await _movieActorService.GetAsync(upMovieActor.actor_id); // чи взагалі є такий запис в БД

                    if (result == null)
                    {
                        Console.WriteLine($"MovieActor {upMovieActor.actor_id} from [MovieActor] not found");
                        return NotFound();
                    }
                    else
                    {
                        await _movieActorService.UpdateAsync(upMovieActor);
                        Console.WriteLine($"MovieActor {upMovieActor.actor_id} successfully update to [MovieActor]");

                        return Ok();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in [MovieActorConstoller]->[UpdateAsync]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")] // DELETE: ado/MovieActor/id
        public async Task<ActionResult> DeleteByIdAsync(int id)
        {
            try
            {
                var result = await _movieActorService.GetAsync(id);

                if (result == null)
                {
                    Console.WriteLine($"MovieActor {id} from [MovieActors] not found");
                    return NotFound();
                }
                else
                {
                    await _movieActorService.DeleteAsync(id);
                    Console.WriteLine($"MovieActor {id} successfully deleted to [MovieActors]");

                    return Ok();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in [MovieActorConstoller]->[UpdateAsync]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
