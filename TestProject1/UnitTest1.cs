using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Task_Manager.Data;
using Task_Manager.DTOs;
using Task_Manager.Models;
using Task_Manager.services;
using Xunit;

namespace TestProject1
{
   
    public class UnitTest1
    {
      
        [Fact]
        public void CreateTask_ShouldSaveTask_WhenInputIsValid()
        {

             var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
                

            
            var context = new ApplicationDbContext(options);
           
            var taskservice = new TaskService(context,null);





            var userId = 1;
            var taskDto = new CreateTaskDTO
            {
                Title = "Test Task",
                Description = "This is a test task",
                Category = Task_Manager.enums.TaskCategory.Work,
                Status = Task_Manager.enums.TaskStat.Pending
            };

           
            var result = taskservice.CreateTask(userId, taskDto);


           Assert.NotNull(result);
              Assert.Equal(taskDto.Title, result.Title);
                Assert.Equal(taskDto.Description, result.Description);
                Assert.Equal(taskDto.Category, result.Category);
                Assert.Equal(taskDto.Status, result.Status);


            Assert.Equal(context.TaskItems.Count(),1 );

               var taskInDb = context.TaskItems.First();

            Assert.Equal(taskInDb.UserId, userId);

            Assert.Equal(taskInDb.Title , taskDto.Title);
            Assert.Equal(taskInDb.Description, taskDto.Description);
            Assert.Equal(taskInDb.Status, taskDto.Status);
            Assert.Equal(taskInDb.Category, taskDto.Category);




        }






        [Fact]
        public void UpdateTask_ShouldUpdateTaskSuccessfully()
        {

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new ApplicationDbContext(options);
           
            var taskservice = new TaskService(context, null);


            var userId = 1;

            var task = new TaskItem
            {
                UserId = userId,
                Title = "Original Task",
                Description = "This is the original task",
                Category = Task_Manager.enums.TaskCategory.Personal,
                Status = Task_Manager.enums.TaskStat.Pending
            };
           

            context.TaskItems.Add(task);


            context.SaveChanges();


            var taskDto = new UpdateTaskDTO
            {
                Title = "Updated Task",
                Description = "This is the updated task",
                Category = Task_Manager.enums.TaskCategory.Work,
                Status = Task_Manager.enums.TaskStat.Completed
            };


            var result = taskservice.UpdateTask(task.Id, userId, taskDto);
            var inDbTask = context.TaskItems.First();



            Assert.NotNull(result);
            Assert.Equal(userId, result.UserId);

            Assert.Equal(taskDto.Title, result.Title);  
            Assert.Equal(taskDto.Description, result.Description);
            Assert.Equal(taskDto.Category, result.Category);
            Assert.Equal(taskDto.Status, result.Status);

            Assert.Equal(inDbTask.Title, taskDto.Title);    
            Assert.Equal(inDbTask.Description, taskDto.Description);
            Assert.Equal(inDbTask.Category, taskDto.Category);
            Assert.Equal(inDbTask.Status, taskDto.Status);

        }

        [Fact]

        public void UpdateTask_ShouldReturnNull_WhenTaskDoesNotExist()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                  .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                  .Options;
            var context = new ApplicationDbContext(options);

            var taskservice = new TaskService(context , null);

            var userId = 1;
            var taskDto = new UpdateTaskDTO
            {
                Title = "Non-existent Task",
                Description = "This task does not exist",
                Category = Task_Manager.enums.TaskCategory.Work,
                Status = Task_Manager.enums.TaskStat.Pending
            };

            var result = taskservice.UpdateTask(999, userId, taskDto);
            Assert.Null(result);

        }

        [Fact]

        public void PatchTask_ShouldUpdateOnlyTitle_WhenOnlyTitleIsProvided()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new ApplicationDbContext(options);

            var taskService = new TaskService(context , null);

            var userId = 1;

            var task = new TaskItem
            {
                UserId = userId,
                Title = "Original Task",
                Description = "This is the original task",
                Category = Task_Manager.enums.TaskCategory.Personal,
                Status = Task_Manager.enums.TaskStat.Pending
            };

            context.TaskItems.Add(task);
            context.SaveChanges();


            var patchedTask = new PatchTaskDTO
            {
                Title = "Patched Task",
            };

            var result = taskService.PatchTask(task.Id, userId, patchedTask);

            var inDbTask = context.TaskItems.First();
            Assert.NotNull(result);
            Assert.Equal(userId, result.UserId);
            Assert.Equal(patchedTask.Title, inDbTask.Title);
            Assert.Equal(task.Description,inDbTask.Description);
            Assert.Equal(task.Category,inDbTask.Category);
            Assert.Equal(task.Status, inDbTask.Status);

        }

        [Fact]
        public void DeleteTask_ShouldRemoveTask_WhenTaskExists()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var context = new ApplicationDbContext(options);
            var taskservice = new TaskService(context , null);

            var userId = 1;
            var task = new TaskItem
            {
                UserId = userId,
                Title = "Task to Delete",
                Description = "This task will be deleted",
                Category = Task_Manager.enums.TaskCategory.Personal,
                Status = Task_Manager.enums.TaskStat.Pending
            };
            context.TaskItems.Add(task);
            context.SaveChanges();

            var result = taskservice.DeleteTask(task.Id, userId);
            Assert.True(result);
            Assert.Empty(context.TaskItems);
       

        }

        [Fact]

        public void DeleteTask_ShouldThrowException_WhenTaskDoesNotExist()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var context = new ApplicationDbContext(options);
            var taskservice = new TaskService(context , null);

            var userId = 1;

          var ex =  Assert.Throws<InvalidOperationException>(() => taskservice.DeleteTask(858, userId));
           
            Assert.Equal("Sequence contains no elements", ex.Message);


        }



    }
}