using Microsoft.AspNetCore.Mvc;

namespace Task_Manager.DTOs
{
    public class LoginResult
    {
        public bool Success { get; set; }
        public int? UserId { get; set; }

        public string? Email { get; set; }
        public string? Error { get; set; }
        
      public  string? Token { get; set; }

        
      
    }
}
