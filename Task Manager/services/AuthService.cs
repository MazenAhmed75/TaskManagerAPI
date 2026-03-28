using BCrypt.Net;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Task_Manager.Data;
using Task_Manager.DTOs;
using Task_Manager.Models;


namespace Task_Manager.services
{
    public class AuthService : IAuthService 
    {
        private readonly ApplicationDbContext context;
        private readonly IConfiguration configuration;
       
        
        public  AuthService(ApplicationDbContext context , IConfiguration configuration)
        {
            this.context = context;

            this.configuration = configuration;
        }

      


        public RegisterResult Register(string email, string password)
        {
            bool emailExists = context.Users.Any(x=>x.Email==email);


            if (emailExists == false)
            {
                User newUser = new User();
                newUser.Email = email;
                newUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
                newUser.CreatedAt = DateTime.UtcNow;

                context.Users.Add(newUser);
                context.SaveChanges();

               return new RegisterResult
                {
                    Success = true,
                    UserId = newUser.Id,
                    Email = email,
                    Error = null
                };
                
               
            }
            else {

                return new RegisterResult
                {
                    Success = false,
                    Error = "Email already exists"
                };
              
            }
        }
        public LoginResult Login(string email, string password)
        {
            User user = context.Users.FirstOrDefault(e => e.Email == email);
           

            if (user != null)
            {
                if (BCrypt.Net.BCrypt.Verify(password , user.PasswordHash) ) 
                {

                    var token = CreateAccessToken(user);

                   
                    return new LoginResult
                    {
                        Success = true,
                        UserId = user.Id,
                        Email = email,
                        Error = null,
                        Token = token   

                    };
                    
                }
                else
                {
                    
                   
                    return new LoginResult
                    {
                        Success = false,
                        Error = "Invalid credentials"
                    };
                }
            }

             else {


                return new LoginResult
                {
                    Success = false,
                    Error = "Invalid credentials"
                };
            }


        }


          private  String CreateAccessToken(User user)

        {


            var uClaims = new List<Claim> {

             new Claim(ClaimTypes.NameIdentifier , user.Id.ToString()),
                new Claim(ClaimTypes.Email , user.Email)

            };

          

            var identity = new ClaimsIdentity(uClaims);







            String k = configuration["Jwt:Key"];

            var key = Encoding.UTF8.GetBytes(k);
            var skey= new SymmetricSecurityKey(key);
            
            var signedCrediential = new SigningCredentials(skey,SecurityAlgorithms.HmacSha256);


        


            var expires = DateTime.UtcNow.AddHours(1);

            var tokenDiscreptor = new SecurityTokenDescriptor
            {

                Subject = identity,
                Expires = expires,
                Issuer = configuration["Jwt:Issuer"],
                Audience = configuration["Jwt:Audience"],
                SigningCredentials = signedCrediential

            };


            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenJwt = tokenHandler.CreateToken(tokenDiscreptor);   
            var token = tokenHandler.WriteToken(tokenJwt);
            return token;   


        }





    }
}
