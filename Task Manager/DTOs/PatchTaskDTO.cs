using System.ComponentModel.DataAnnotations;
using Task_Manager.enums;

namespace Task_Manager.DTOs
{
    public class PatchTaskDTO
    {
       
        public string?  Title { get; set; }    

        public string? Description { get; set; }
        public TaskCategory? Category { get; set; }

        public TaskStat? status { get; set; } 


    }
}
