using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Upskilling.DTOs;
using Upskilling.Entities;
using Upskilling.Models;

namespace Upskilling.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        UpskillingDbContext Db = new UpskillingDbContext();

        [HttpGet]
        public IActionResult GetAll()
        {
            var cats = Db.Categories.Include(c => c.Books).AsNoTracking().ToList();

            List<CategoryWithBooks> CatBookDTO = new List<CategoryWithBooks>();
            List<string> names = new List<string>();

            foreach (var c in cats)
            {
                foreach (var b in c.Books)
                {
                    if(b.CategoryId == c.CategoryId)
                        names.Add(b.Name);
                }
                CatBookDTO.Add(new CategoryWithBooks() { CategoryId = c.CategoryId, CategoryName = c.Name, Books = names });
            }

            if(cats is not null)
            {
                return Ok(new { Categories = CatBookDTO, Message = "Data Exist" });
            }
            else
            {
                return NotFound(new { ErrorMessage = "Sorry! No Categories exist!" });
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var categories = Db.Categories.Include(c => c.Books).AsNoTracking().FirstOrDefault(c => c.CategoryId == id);
            var CategoryBooksDTO = new CategoryWithBooks();
            CategoryBooksDTO.CategoryId = categories.CategoryId;
            CategoryBooksDTO.CategoryName = categories.Name;
            List<string> names = new List<string>();

            foreach(var c in categories.Books)
                names.Add(c.Name);

            if (categories is not null)
                return Ok(new { CategoryBooksDTO, Message = $"Category with Id = {id} exists!" });
            else
                return
                    NotFound(new { ErrorMessage = $"Sorry! Category with {id} is not exist" });
        }

        [HttpPost]
        public IActionResult Add(Category category)
        {
            if (category is not null)
            {
                Db.Categories.Add(category);
                Db.SaveChanges();
                return Ok(new { depts = Db.Categories.ToList(), Message = "Category Added Successfully!" });
            }
            else
                return BadRequest(new { ErrorMessage = $"{category} not Valid!" });
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(int id,[FromBody]Category category)
        {
            if(ModelState.IsValid == true)
            {
                var oldCategory = Db.Categories.Include(c => c.Books).FirstOrDefault(c => c.CategoryId == id);

                if (oldCategory is not null)
                {
                    oldCategory.Name = category.Name;
                    oldCategory.Description = category.Description;
                    Db.SaveChanges();
                    return Ok(new { message = $"Category with {id} updated Successfully!" });
                }
                else
                {
                    return BadRequest(new { ErrorMessage = $"{category} not Valid!" });
                }
                                            
            }
            return BadRequest(ModelState);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var oldCategory = Db.Categories.FirstOrDefault(c => c.CategoryId == id);
            if (oldCategory is not null)
            {
                try
                {
                    Db.Categories.Remove(oldCategory);
                    Db.SaveChanges();
                    return Ok(new { oldCategory, ErrorMessage = $"Category with id {id} Removed Successfully" });
                }
                catch(Exception ex)
                {
                    BadRequest(new { ErrorMessage = $"Category with {id} didn't remove! Please try again!" });
                }
            }                
            return BadRequest(new { ErrorMessage = $"Category with {id} didn't remove or Id is not found! Please try again!" });
       
        }
    }
}
