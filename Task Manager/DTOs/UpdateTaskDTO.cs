using System.ComponentModel.DataAnnotations;
using Task_Manager.enums;

namespace Task_Manager.DTOs
{
    public class UpdateTaskDTO
    {

        [Required]
            public string Title { get; set; }

        [Required]
            public string Description { get; set; }

        [Required]
            public TaskCategory Category { get; set; }

        [Required]
            public TaskStat Status { get; set; }


        }
    }

