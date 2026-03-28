using Microsoft.AspNetCore.Identity.Data;
using Task_Manager.DTOs;

namespace Task_Manager.services
{
    public interface IAuthService
    {
        LoginResult Login(string email, string password);
     
        RegisterResult Register(string email, string password);
    }
}
