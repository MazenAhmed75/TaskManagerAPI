using Microsoft.EntityFrameworkCore.Storage;
using System.Diagnostics.Contracts;
using Task_Manager.enums ;

namespace Task_Manager.Models
{
    public class TaskItem
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public TaskCategory Category { get; set; }

        public TaskStat Status { get; set; } = TaskStat.Pending;

        public int UserId { get; set; }

        public User User { get; set; } // navgation property
         


    }
}
