using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Task_Manager.enums;

namespace Task_Manager.DTOs
{
    public class CreateTaskDTO
    {
        [Required]
        [Description("Title of the task")]
        public string Title { get; set; } 

        [Required]
        [Description("Description of the task")]
        public string Description { get; set; } 
            [Required]
        [Description("Category of the task ( Work, Personal , Study)")]
        public TaskCategory Category { get; set; } 

            public TaskStat Status { get; set; } = TaskStat.Pending;


    }
    }



