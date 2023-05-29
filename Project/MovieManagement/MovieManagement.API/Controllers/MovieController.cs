using Microsoft.AspNetCore.Mvc;
using MovieManagement.BLL.DTO;
using MovieManagement.BLL.Services.Consracts;



namespace MovieManagement.API.Controllers
{
    [Route("ado/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        IMovieService _movieService;

        public MovieController(IMovieService MovieService)
        {
            _movieService = MovieService;
        }



        [HttpGet] // GET: ado/Movie
        public async Task<ActionResult<IEnumerable<MovieDTO>>> GetAllAsync()
        {
            try
            {
                var result = await _movieService.GetAllAsync();
                Console.WriteLine("All Movie were successfully extracted from [Movie]");

                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in [MovieConstoller]->[GetAllAsync]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")] // GET: ado/Movie/id
        public async Task<ActionResult<MovieDTO>> GetByIdAsync(int id)
        {
            try
            {
                var result = await _movieService.GetAsync(id); // чи взагалі є такий запис в БД

                if (result == null)
                {
                    Console.WriteLine($"Movie {id} from [Movie] not found");
                    return NotFound();
                }
                else
                {
                    Console.WriteLine($"Movie {result.movie_id} were successfully extracted from [Movie]");
                    return Ok(result);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in [MovieController]->[GetByIdAsync]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }




        [HttpPost] // POST: ado/Movie
        public async Task<ActionResult> AddAsync(MovieDTO newMovie)
        {
            try
            {
                // Чи введені валідні данні
                if (newMovie.title == null)
                {
                    return BadRequest("Invalid information");
                }
                else
                {
                    var id = await _movieService.CreateAsync(newMovie);
                    Console.WriteLine($"Movie {id} successfully added to [Movie]");

                    return Ok(id);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in [MovieConstoller]->[AddAsync]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut] // PUT: ado/Movie
        public async Task<ActionResult> UpdateAsync(MovieDTO upMovie)
        {
            try
            {
                // Чи введені валідні данні
                if (upMovie.title == null)
                {
                    return BadRequest("Invalid information");
                }
                else
                {
                    var result = await _movieService.GetAsync(upMovie.movie_id); // чи взагалі є такий запис в БД

                    if (result == null)
                    {
                        Console.WriteLine($"Movie {upMovie.movie_id} from [Movie] not found");
                        return NotFound();
                    }
                    else
                    {
                        await _movieService.UpdateAsync(upMovie);
                        Console.WriteLine($"Movie {upMovie.movie_id} successfully update to [Movie]");

                        return Ok();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in [MovieConstoller]->[UpdateAsync]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")] // DELETE: ado/Movie/id
        public async Task<ActionResult> DeleteByIdAsync(int id)
        {
            try
            {
                var result = await _movieService.GetAsync(id);

                if (result == null)
                {
                    Console.WriteLine($"Movie {id} from [Movie] not found");
                    return NotFound();
                }
                else
                {
                    await _movieService.DeleteAsync(id);
                    Console.WriteLine($"Movie {id} successfully deleted to [Movie]");

                    return Ok();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in [MovieConstoller]->[UpdateAsync]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
