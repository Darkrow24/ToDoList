using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Entities;

namespace ToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        static List<ToDoItem> toDoItems = [
            new ToDoItem
            {
                Id = 1,
                Title = "Learn ASP.NET Core",
                Description = "Learn how to build web applications using ASP.NET Core.",
                IsCompleted = false,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            }
        ];

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(toDoItems);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var item = toDoItems.FirstOrDefault(i => i.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPost]
        public IActionResult Add([FromBody] ToDoItem toDoItem)
        {
            if (toDoItem == null)
            {
                return BadRequest("Invalid ToDoItem data.");
            }

            // Check if an item with the same Id already exists
            if (toDoItems.Any(i => i.Id == toDoItem.Id))
            {
                return BadRequest($"An item with Id {toDoItem.Id} already exists.");
            }

            toDoItem.CreatedAt = DateTime.Now;
            toDoItem.UpdatedAt = DateTime.Now;

            toDoItems.Add(toDoItem);
            return Ok("Yapılacaklar listesine eklendi");
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ToDoItem updatedItem)
        {
            if (updatedItem == null || updatedItem.Id != id)
            {
                return BadRequest();
            }
            var item = toDoItems.FirstOrDefault(i => i.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            item.Title = updatedItem.Title;
            item.Description = updatedItem.Description;
            item.IsCompleted = updatedItem.IsCompleted;
            item.UpdatedAt = DateTime.Now;
            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = toDoItems.FirstOrDefault(i => i.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            toDoItems.Remove(item);
            return NoContent();
        }


    }
}
