﻿using Microsoft.AspNetCore.Http;
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
                Title = "Etüte katıl.",
                Description = "Bu haftaki etüte katıl.",
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
                return BadRequest("Geçersiz eleman verileri");
            }
            toDoItem.Id = toDoItems.Count > 0 ? toDoItems.Max(i => i.Id) + 1 : 1;

            toDoItem.CreatedAt = DateTime.Now;
            toDoItem.UpdatedAt = DateTime.Now;

            toDoItems.Add(toDoItem);
            return Ok("Yapılacaklar listesine girilen elemanı eklenmiştir.");
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
            return Ok("Yapılacaklar listesi elemanı güncellenmiştir.");
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

        [HttpGet("completed")]
        public IActionResult GetCompleted()
        {
            var completedItems = toDoItems.Where(i => i.IsCompleted).ToList();
            if (!completedItems.Any())
            {
                return NotFound("Tamamlanmış eleman bulunmamaktadır.");
            }
            return Ok(completedItems);
        }
        
        [HttpGet("incompleted")]
        public IActionResult GetIncompleted()
        {
            var incompletedItems = toDoItems.Where(i => !i.IsCompleted).ToList();
            if (!incompletedItems.Any())
            {
                return NotFound("Tamamlanmamış eleman bulunmamaktadır.");
            }
            return Ok(incompletedItems);
        }

        [HttpPut("complete/{id}")]
        public IActionResult MarkAsCompleted(int id)
        {
            var item = toDoItems.FirstOrDefault(i => i.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            else if (item.IsCompleted)
            {
                return BadRequest("Seçilen yapılacaklar listesi elemanı zaten tamamlanmış.");
            }
            item.IsCompleted = true;
            item.UpdatedAt = DateTime.Now;
            return Ok("Yapılacaklar listesi elemanı tamamlandı.");
        }

        [HttpPut("incomplete/{id}")]
        public IActionResult MarkAsIncompleted(int id)
        {
            var item = toDoItems.FirstOrDefault(i => i.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            else if (item.IsCompleted != true)
            {
                return BadRequest("Seçilen yapılacaklar listesi elemanı zaten tamamlanmamış.");
            }
            item.IsCompleted = false;
            item.UpdatedAt = DateTime.Now;
            return Ok("Yapılacaklar listesi elemanı güncellenmiştir.");
        }




    }
}
