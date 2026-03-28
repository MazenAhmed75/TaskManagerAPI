using Task_Manager.Data;
using Task_Manager.DTOs;
using Task_Manager.enums;
using Task_Manager.Models;


namespace Task_Manager.services
{
    public class TaskService 
    {

            private readonly ApplicationDbContext context;
        private readonly  ILogger<TaskService> logger;
        

        public TaskService(ApplicationDbContext dbContext, ILogger<TaskService> logger)
        {

            this.context = dbContext;
            this.logger = logger;
        }

       

        public TaskItem CreateTask(int userID , CreateTaskDTO task)
            {
              

                TaskItem taskItem = new TaskItem
                {
                    Title = task.Title,
                    Description = task.Description,
                    Category = (TaskCategory)task.Category,
                    Status = (TaskStat)task.Status,
                    UserId = userID
                };

            if (task.Title == null || task.Description == null || task.Category == null || task.Status == null)
            {

                logger.LogWarning("Invalid task data provided for User ID: {UserId}", userID);
                return null;

            }


                context.TaskItems.Add(taskItem);
                context.SaveChanges();

            logger.LogInformation("Task created with ID: {TaskId} for User ID: {UserId}", taskItem.Id, userID);

            return taskItem;
            }

            public List<TaskItem> GetTasks(int userId, TaskStat? status, TaskCategory? category)

            {
              

                var query = context.TaskItems.Where(x => x.UserId == userId);


                if (status != null)
                {
                    query = query.Where(x => x.Status == status);
                }

                if (category != null)
                {
                    query = query.Where(x => x.Category == category);
                }


                var tasks = query.ToList();

            logger.LogInformation("Retrieved {TaskCount} tasks for User ID: {UserId} with Status: {Status} and Category: {Category}", tasks.Count, userId, status, category);
            return tasks;

            }

            public TaskItem GetTaskById(int id , int userId)
            {
               



                var task = context.TaskItems.FirstOrDefault(x => x.Id == id && x.UserId == userId);

                if (task == null)
                {
                    logger.LogWarning("Task with ID: {TaskId} not found for User ID: {UserId}", id, userId);
                return null;
                }

                logger.LogInformation("Task with ID: {TaskId} retrieved for User ID: {UserId}", id, userId);
            return task;
            }

            public TaskItem UpdateTask(int id, int userId, UpdateTaskDTO task)
            {
                

                var taskItem = context.TaskItems.FirstOrDefault(x => x.Id == id && x.UserId == userId);

                if (taskItem == null)
                {

                logger.LogWarning("Task with ID: {TaskId} not found for User ID: {UserId}", id, userId);
                return null;

                
                }

                taskItem.Title = task.Title;
                taskItem.Description = task.Description;
                taskItem.Category = (TaskCategory)task.Category;
                taskItem.Status = (TaskStat)task.Status;

            if (task.Title == null || task.Description == null || task.Category == null || task.Status == null)
            {
                logger.LogWarning("Invalid update data for Task ID: {TaskId} for User ID: {UserId}", id, userId);
                return null;
            }





                context.SaveChanges();

             logger.LogInformation("Task with ID: {TaskId} updated for User ID: {UserId}", id, userId);
            return taskItem;

            }






        public TaskItem PatchTask(int id, int userId, PatchTaskDTO task)
        {


            var taskItem = context.TaskItems.FirstOrDefault(x => x.Id == id && x.UserId == userId);

            if (taskItem == null)
            {
                logger.LogWarning("Task with ID: {TaskId} not found for User ID: {UserId}", id, userId);
                return null;
            }

            if (task.Title != null)
            {
                taskItem.Title = task.Title;
            }
            

            if (task.Description != null)
            {
                taskItem.Description = task.Description;
            }
   
            if (task.Category != null)
            {
                taskItem.Category =task.Category.Value;
            }

            if (task.status != null)
            {
                taskItem.Status = task.status.Value;
            }
      
            context.SaveChanges();

                logger.LogInformation("Task with ID: {TaskId} patched for User ID: {UserId}", id, userId);

            return taskItem;

        }


        public bool DeleteTask(int id , int userId)
            {
                
                var taskItem = context.TaskItems.FirstOrDefault(x => x.Id == id && x.UserId == userId);

            if (taskItem == null)
            {
                logger.LogWarning("Task with ID: {TaskId} not found for User ID: {UserId}", id, userId);
                return false;

            }

                

            try
            {
                context.Remove(taskItem);
                context.SaveChanges();
            }
            catch (Exception ex)
            {

                logger.LogError(ex, "Error deleting task with ID: {TaskId} for User ID: {UserId}", id, userId);

                throw new Exception($"Error deleting task with ID: {id} for User ID: {userId}", ex);

            }


                logger.LogInformation("Task with ID: {TaskId} deleted for User ID: {UserId}", id, userId);

            return true;


        }




        }
    }


