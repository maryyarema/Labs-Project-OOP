using Microsoft.AspNetCore.Mvc;
using MovieManagement.BLL.DTO;
using MovieManagement.BLL.Services;
using MovieManagement.BLL.Services.Consracts;



namespace MovieManagement.API.Controllers
{
    [Route("ado/[controller]")]
    [ApiController]
    public class MovieCategoryController : ControllerBase
    {
        IMovieCategoryService _movieCategoryService;

        public MovieCategoryController(IMovieCategoryService MovieCategoryService)
        {
            _movieCategoryService = MovieCategoryService;
        }



        [HttpGet] // GET: ado/MovieCategoryService
        public async Task<ActionResult<IEnumerable<MovieCategoryDTO>>> GetAllAsync()
        {
            try
            {
                var result = await _movieCategoryService.GetAllAsync();
                Console.WriteLine("All MovieCategoryService were successfully extracted from [MovieCategoryService]");

                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in [MovieCategoryServiceConstoller]->[GetAllAsync]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")] // GET: ado/MovieCategory/id
        public async Task<ActionResult<MovieCategoryDTO>> GetByIdAsync(int id)
        {
            try
            {
                var result = await _movieCategoryService.GetAsync(id); // чи взагалі є такий запис в БД

                if (result == null)
                {
                    Console.WriteLine($"MovieCategory {id} from [MovieCategory] not found");
                    return NotFound();
                }
                else
                {
                    Console.WriteLine($"MovieCategory {result.movie_id} were successfully extracted from [MovieCategory]");
                    return Ok(result);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in [MovieCategoryController]->[GetByIdAsync]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }




        [HttpPost] // POST: ado/MovieCategory
        public async Task<ActionResult> AddAsync(MovieCategoryDTO newMovieCategory)
        {
            try
            {
                // Чи введені валідні данні
                if (newMovieCategory.category_id == 0)
                {
                    return BadRequest("Invalid information");
                }
                else
                {
                    var id = await _movieCategoryService.CreateAsync(newMovieCategory);
                    Console.WriteLine($"MovieCategory {id} successfully added to [MovieCategory]");

                    return Ok(id);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in [MovieCategoryConstoller]->[AddAsync]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut] // PUT: ado/MovieCategory
        public async Task<ActionResult> UpdateAsync(MovieCategoryDTO upMovieCategory)
        {
            try
            {
                // Чи введені валідні данні
                if (upMovieCategory.category_id == 0)
                {
                    return BadRequest("Invalid information");
                }
                else
                {
                    var result = await _movieCategoryService.GetAsync(upMovieCategory.movie_id); // чи взагалі є такий запис в БД

                    if (result == null)
                    {
                        Console.WriteLine($"Actor {upMovieCategory.movie_id} from [Actors] not found");
                        return NotFound();
                    }
                    else
                    {
                        await _movieCategoryService.UpdateAsync(upMovieCategory);
                        Console.WriteLine($"Actor {upMovieCategory.movie_id} successfully update to [Actors]");

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

        [HttpDelete("{id}")] // DELETE: ado/MovieCategory/id
        public async Task<ActionResult> DeleteByIdAsync(int id)
        {
            try
            {
                var result = await _movieCategoryService.GetAsync(id);

                if (result == null)
                {
                    Console.WriteLine($"MovieCategory {id} from [MovieCategory] not found");
                    return NotFound();
                }
                else
                {
                    await _movieCategoryService.DeleteAsync(id);
                    Console.WriteLine($"MovieCategory {id} successfully deleted to [MovieCategory]");

                    return Ok();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in [MovieCategoryConstoller]->[UpdateAsync]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
