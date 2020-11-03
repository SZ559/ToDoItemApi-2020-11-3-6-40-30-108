using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoItemApi.Entity;
using ToDoItemApi.Service;

namespace ToDoItemApi.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToDoItemController : ControllerBase
    {
        private IToDoItemService _service;
        public ToDoItemController(IToDoItemService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get To Do Item By Id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("id")]
        public ActionResult<ToDoItem> GetToDoItem(long? id)
        {
            if (id == null)
            {
                return BadRequest(new Dictionary<string, string>() { { "message", "Id is required" } });
            }
            long id_NotNull = id.Value;

            var target_ToDoItem = _service.GetToDoItemById(id_NotNull);
            if (target_ToDoItem == null)
            { 
                return NotFound(new Dictionary<string, string>() { { "message", $"Can't find {id}" } }); 
            }
            return Ok(target_ToDoItem);
        }

        /// <summary>
        /// Get All To Do Item
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<ToDoItem>> GetAllToDoItem()
        {
            return Ok(_service.GetAllToDoItems());
        }

        /// <summary>
        /// Create To Do Item. If Id already exists, update the To Do item with id.
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut]
        public ActionResult<ToDoItem> CreateToDoItem(ToDoItem toDoItem)
        {
            if (toDoItem == null)
            {
                return BadRequest(new Dictionary<string, string>() { { "message", "To Do Item cannot be null" } });
            }
            if (toDoItem.Id == null)
            {
                return BadRequest(new Dictionary<string, string>() { { "message", "Id is required in the To Do Item" } });
            }
            if (toDoItem.Name == null)
            {
                return BadRequest(new Dictionary<string, string>() { { "message", "Name is required in the To Do Item" } });
            }
            if (toDoItem.IsComplete == null)
            {
                return BadRequest(new Dictionary<string, string>() { { "message", "IsComplete is required in the To Do Item" } });
            }

            return Ok(_service.UpsertToDoItem(toDoItem));
        }
    }
}
