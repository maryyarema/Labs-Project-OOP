using System.Dynamic;
using Microsoft.AspNetCore.Mvc;
using UserManagementEF.UserManagementEF.BLL.DTO;
using UserManagementEF.UserManagementEF.BLL.DTO.HATEOAS;
using UserManagementEF.UserManagementEF.BLL.Services;
using UserManagementEF.UserManagementEF.BLL.Services.Contracts;
using UserManagementEF.UserManagementEF.DAL.Paging.Entities;

namespace UserManagementEF.UserManagementEF.API.Controllers
{
    [Produces("application/json")]
    [Route("ef/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;
        private readonly ILogger _logger;
        private readonly IUrlHelper _urlHelper;

        public MovieController(IMovieService movieService, ILoggerFactory loggerFactory, IUrlHelper urlHelper)
        {
            _movieService = movieService;
            _urlHelper = urlHelper;
            _logger = loggerFactory.CreateLogger($"{this.GetType().Name}_Logger");
        }


        /// <summary>
        /// Gets the list of all Movies
        /// </summary>
        /// <remarks>
        /// Sample request:
    
        /// </remarks>
        /// <returns>Returns list of MovieDTO</returns>
        /// <response code="200">Success</response>
        /// <response code="500">If it was not possible to get a list of elements from the database</response>
        [HttpGet(Name = nameof(GetAllMoviesAsync))] // GET: ef/movie
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<MovieDTO>>> GetAllMoviesAsync
            ([FromQuery] MovieParameters parameters)
        {
            try
            {
                var collection = (await _movieService.GetAllAsync(parameters)).ToList();
                _logger.LogInformation
                    ("{Count} entities were successfully extracted from [Movies]", collection.Count);
            
                collection.ForEach(item => this.CreateLinksForEntity(item));
                return Ok(collection);
            }
            catch (Exception ex)
            {
                _logger.LogError
                    ("Error in [{ErrorClassName}]->[GetAllAsync] => {ErrorMessage}", this.GetType().Name, ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Gets the Movie by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET ef/Movie/13ce1333-7b7c-4395-8565-0474a6ad05ad
        /// </remarks>
        /// <param name="id">Movie id (int)</param>
        /// <returns>Returns element of MovieDTO</returns>
        /// <response code="200">Success</response>
        /// <response code="404">If the element with such ID is not found in the database</response>
        /// <response code="500">If it was not possible to get element from the database</response>
        [HttpGet("{id:int}", Name = nameof(DeleteMovieAsync))] // GET: ef/movie/id
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<MovieDTO>> GetMovieByIdAsync(int id)
        {
            try
            {
                var entity = await _movieService.GetAsync(id);

                if (entity == null)
                {
                    _logger.LogError("Entity with id: [{EntityId}] from [Movies] not found", id);

                    return StatusCode(StatusCodes.Status404NotFound);
                    //return NotFound();
                }
                else
                {
                    _logger.LogInformation
                        ("Entity with id: [{EntityId}] were successfully extracted from [Movies]", id);

                    return Ok(this.CreateLinksForEntity(entity));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError
                    ("Error in [{ErrorClassName}]->[GetAllAsync] => {ErrorMessage}", this.GetType().Name, ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    
        [HttpPost(Name = nameof(AddMovieAsync))] // POST: ef/Movie
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<int>> AddMovieAsync(MovieDTO newMovie)
        {
            try
            {
                // Checking whether valid data has been entered
                if (newMovie.Title == null )
                {
                    _logger.LogError("Invalid data entered");

                    return StatusCode(StatusCodes.Status400BadRequest);
                    //return BadRequest("Invalid data entered");
                }
                else
                {
                    var id = await _movieService.CreateAsync(newMovie);
                    _logger.LogInformation
                        ("Entity with id: [{EntityId}] were successfully added to [Movies]", id);

                    return Ok(id);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError
                    ("Error in [{ErrorClassName}]->[GetAllAsync] => {ErrorMessage}", this.GetType().Name, ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        /// <param name="updateMovie">MovieDTO updateEntity</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="400">If invalid data entered</response>
        /// <response code="500">If it was not possible to adding element to the database</response>
        [HttpPut(Name = nameof(UpdateMovieAsync))] // PUT: ef/Movie
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateMovieAsync(MovieDTO updateMovie)
        {
            try
            {
                // Checking whether valid data has been entered
                if (updateMovie.Title == null )
                {
                    _logger.LogError("Invalid data entered");

                    return StatusCode(StatusCodes.Status400BadRequest);
                    //return BadRequest("Invalid data entered");
                }
                else
                {
                    // Whether there is such a record in the database at all
                    var findResult = await _movieService.GetAsync(updateMovie.MovieId);

                    if (findResult == null)
                    {
                        _logger.LogError
                            ("Entity with id: [{EntityId}] from [Movies] not found",
                                updateMovie.MovieId);

                        return StatusCode(StatusCodes.Status404NotFound);
                        //return NotFound();
                    }
                    else
                    {
                        await _movieService.UpdateAsync(updateMovie);
                        _logger.LogInformation
                            ("Entity with id: [{EntityId}] were successfully updated from [Movies]",
                                updateMovie.MovieId);

                        return StatusCode(StatusCodes.Status204NoContent);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError
                    ("Error in [{ErrorClassName}]->[GetAllAsync] => {ErrorMessage}", this.GetType().Name, ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Delete the Movie by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE: ef/Movie/13ce1333-7b7c-4395-8565-0474a6ad05ad
        /// </remarks>
        /// <param name="id">Movie id (Guid)</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="400">If invalid data entered</response>
        /// <response code="500">If it was not possible to adding element to the database</response>
        [HttpDelete("{id:int}", Name = nameof(DeleteMovieAsync))] // DELETE: ef/Movie/id
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteMovieAsync(int id)
        {
            try
            {
                // Whether there is such a record in the database at all
                var findResult = await _movieService.GetAsync(id);

                if (findResult == null)
                {
                    _logger.LogError("Entity with id: [{EntityId}] from [Movies] not found", id);

                    return StatusCode(StatusCodes.Status404NotFound);
                    //return NotFound();
                }
                else
                {
                    await _movieService.DeleteAsync(id);
                    _logger.LogInformation
                        ("Entity with id: [{EntityId}] were successfully deleted from [Movies]", id);

                    return StatusCode(StatusCodes.Status204NoContent);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError
                    ("Error in [{ErrorClassName}]->[GetAllAsync] => {ErrorMessage}", this.GetType().Name, ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }



        /// <summary>
        /// Gets the list of all Movie
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET ef/Movie/datashaping?Fields=UserId%2C%20FirstName%2C%20LastName%2C%20Password
        /// </remarks>
        /// <returns>Returns list of ExpandoObject(Movie)</returns>
        /// <response code="200">Success</response>
        /// <response code="500">If it was not possible to get a list of elements from the database</response>
        [HttpGet("datashaping/", Name = nameof(GetAllMovies_DataShaping_Async))] // ef/Movie/datashaping?Fields=UserId%2C%20FirstName%2C%20LastName%2C%20Password
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetAllMovies_DataShaping_Async([FromQuery] MovieParameters parameters)
        {
            try
            {
                var collection = await _movieService.GetAll_DataShaping_Async(parameters);
                _logger.LogInformation
                    ("{Count} entities were successfully extracted from [Movies]", collection.Count());

                return Ok(collection);
            }
            catch (Exception ex)
            {
                _logger.LogError
                    ("Error in [{ErrorClassName}]->[GetAllAsync] => {ErrorMessage}", this.GetType().Name, ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Gets the Movie by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET ef/Movie/datashaping/b12c5ca7-ab3f-4d0c-bc58-0512bbb30e69?Fields=UserId%2C%20FirstName%2C%20Email
        /// </remarks>
        /// <param name="id">Movie id (Guid)</param>
        /// <param name="parameters">Movie parameters for sort/paging/... (MovieParameters)</param>
        /// <returns>Returns element of ExpandoObject(Movie)</returns>
        /// <response code="200">Success</response>
        /// <response code="404">If the element with such ID is not found in the database</response>
        /// <response code="500">If it was not possible to get element from the database</response>
        [HttpGet("datashaping/{id:int}", Name = nameof(GetMovieById_DataShaping_Async))] // ef/Movie/datashaping/b12c5ca7-ab3f-4d0c-bc58-0512bbb30e69?Fields=UserId%2C%20FirstName%2C%20Email
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetMovieById_DataShaping_Async
            (int id, [FromQuery] MovieParameters? parameters)
        {
            try
            {
                var entity = await _movieService.GetById_DataShaping_Async(id, parameters);

                if (entity == default(ExpandoObject))
                {
                    _logger.LogError("Movie with id: {id}, hasn't been found in db.", id);
                    return StatusCode(StatusCodes.Status404NotFound);
                }

                return Ok(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError
                    ("Error in [{ErrorClassName}]->[GetAllAsync] => {ErrorMessage}", this.GetType().Name, ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        



        private MovieDTO CreateLinksForEntity(MovieDTO entity)
        {
            var idObj = new { id = entity.MovieId };
            
            entity.Links.Add(
                new Link(this._urlHelper.Link(nameof(this.GetMovieByIdAsync), idObj)!,
                    "self",
                    "GET"));
            
            entity.Links.Add(
                new Link(this._urlHelper.Link(nameof(this.UpdateMovieAsync), idObj)!,
                    "update_user",
                    "UPDATE"));
            
            entity.Links.Add(
                new Link(this._urlHelper.Link(nameof(this.DeleteMovieAsync), idObj)!,
                    "delete_user",
                    "DELETE"));

            return entity;
        } 
    }
}
