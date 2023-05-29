using Microsoft.AspNetCore.Mvc;
using MovieManagement.BLL.DTO;
using MovieManagement.BLL.Services.Consracts;



namespace MovieManagement.API.Controllers
{
    [Route("ado/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        ICategoryService _categoryService;

        public CategoryController(ICategoryService CategoryService)
        {
            _categoryService = CategoryService;
        }



        [HttpGet] // GET: ado/Category
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetAllAsync()
        {
            try
            {
                var result = await _categoryService.GetAllAsync();
                Console.WriteLine("All Category were successfully extracted from [Category]");

                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in [CategoryConstoller]->[GetAllAsync]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")] // GET: ado/Category/id
        public async Task<ActionResult<CategoryDTO>> GetByIdAsync(int id)
        {
            try
            {
                var result = await _categoryService.GetAsync(id); // чи взагалі є такий запис в БД

                if (result == null)
                {
                    Console.WriteLine($"Category {id} from [Category] not found");
                    return NotFound();
                }
                else
                {
                    Console.WriteLine($"Category {result.category_id} were successfully extracted from [Category]");
                    return Ok(result);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in [CategoryController]->[GetByIdAsync]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }




        [HttpPost] // POST: ado/Category
        public async Task<ActionResult> AddAsync(CategoryDTO newCategory)
        {
            try
            {
                // Чи введені валідні данні
                if (newCategory.name == null)
                {
                    return BadRequest("Invalid information");
                }
                else
                {
                    var id = await _categoryService.CreateAsync(newCategory);
                    Console.WriteLine($"Category {id} successfully added to [Category]");

                    return Ok(id);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in [CategoryConstoller]->[AddAsync]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut] // PUT: ado/Category
        public async Task<ActionResult> UpdateAsync(CategoryDTO upCategory)
        {
            try
            {
                // Чи введені валідні данні
                if (upCategory.name == null)
                {
                    return BadRequest("Invalid information");
                }
                else
                {
                    var result = await _categoryService.GetAsync(upCategory.category_id); // чи взагалі є такий запис в БД

                    if (result == null)
                    {
                        Console.WriteLine($"Category {upCategory.category_id} from [Category] not found");
                        return NotFound();
                    }
                    else
                    {
                        await _categoryService.UpdateAsync(upCategory);
                        Console.WriteLine($"Category {upCategory.category_id} successfully update to [Category]");

                        return Ok();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in [CategoryConstoller]->[UpdateAsync]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")] // DELETE: ado/Category/id
        public async Task<ActionResult> DeleteByIdAsync(int id)
        {
            try
            {
                var result = await _categoryService.GetAsync(id);

                if (result == null)
                {
                    Console.WriteLine($"Category {id} from [Category] not found");
                    return NotFound();
                }
                else
                {
                    await _categoryService.DeleteAsync(id);
                    Console.WriteLine($"Category {id} successfully deleted to [Category]");

                    return Ok();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in [CategoryConstoller]->[UpdateAsync]\n " + ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
