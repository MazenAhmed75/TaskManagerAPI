using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Task_Manager.DTOs;
using Task_Manager.enums;
using Task_Manager.Models;
using Task_Manager.services;

namespace Task_Manager.Controllers
{

    [ApiController]
    [Route("Api/[controller]")]

    public class TasksController : BaseController
    {

      

        private readonly TaskService taskService;

        public TasksController( TaskService taskService)
        { 
            this.taskService = taskService;

        }



        [Authorize]
        [HttpPost]
        [EndpointSummary("Creates a new task")]
        [ProducesResponseType(typeof(TaskItem),201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]

        public IActionResult CreateTask([FromBody] CreateTaskDTO task)
        {
            
          

            var createdTask = taskService.CreateTask(GetUserId() , task );

            if (createdTask == null)
            {
                return BadRequest("Failed to create task");
            }
            return CreatedAtAction(
                nameof(GetTaskById),
                new { id = createdTask.Id },
                createdTask
                );
        }


        [Authorize]
        [HttpGet]
        [EndpointSummary("Retrieves all tasks")]
        [ProducesResponseType(typeof(List<TaskItem>), 200)]
        [ProducesResponseType(401)]
        public IActionResult GetTasksAll(TaskStat? status, TaskCategory? category)

        {
           var AllTasks = taskService.GetTasks(GetUserId(), status, category);

            return Ok(AllTasks);
            

        }

        [Authorize]
        [HttpGet("{id}")]
        [EndpointSummary("Retrieves a specific task by ID")]
        [ProducesResponseType(typeof(TaskItem), 200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public IActionResult GetTaskById(int id)
        {

            var theTask = taskService.GetTaskById(id, GetUserId());
            if (theTask == null) {
                return NotFound("Task not found");
            }

            return Ok(theTask);
        }


        [Authorize]
        [HttpPut("{id}")]
        [EndpointSummary("Update tasks")]
        [ProducesResponseType(typeof(TaskItem), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public IActionResult UpdateTask(int id, [FromBody] UpdateTaskDTO task)
        {
         
            var updatedTask = taskService.UpdateTask(id, GetUserId(), task);

            if (updatedTask == null)
            {
                return NotFound("Task not found or update failed");
            }


            return Ok(updatedTask);

        }





        [Authorize]
        [HttpPatch("{id}")]
        [EndpointSummary("Partially update a task")]
        [ProducesResponseType(typeof(TaskItem), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public IActionResult PatchTask(int id, [FromBody] PatchTaskDTO task)
        {

            var patchedTask = taskService.PatchTask(id, GetUserId(), task);

            if (patchedTask == null)
            {
                return NotFound("Task not found or update failed");
            }


            return Ok(patchedTask);

        }





        [Authorize]
        [HttpDelete("{id}")]
        [EndpointSummary("Deletes tasks")]
        [ProducesResponseType(typeof(string),200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]

        public IActionResult DeleteTask(int id)
        {
         

          

            if (!taskService.DeleteTask(id, GetUserId()))
            {
                return NotFound("Task not found or delete failed");
            }



            return Ok("Task deleted successfully");


        }




    }
}
