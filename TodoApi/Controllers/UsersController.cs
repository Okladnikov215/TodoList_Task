using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Auth;
using TodoApi.Models.Users;
using TodoApi.Models;
using TodoApi.Users;
using System.Security.Cryptography;

namespace TodoApi.Controllers
{
    [Route("v1/users")]
    public class AuthController : Controller
    {
        private UserContext repository;
        private IAuthenticator authenicator;

        public AuthController(UserContext repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException(nameof(repository));
            }

            this.repository = repository;
            this.authenicator = new Authenticator(repository);
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserRegistrationInfo uInfo)
        {
            if (uInfo == null)
            {
                return this.BadRequest("NoteBuildInfo");
            }
            var hashedPassword = HashPassword(uInfo.Password);
            var user = new User
            {
                Id = Guid.NewGuid(),
                Login = uInfo.Login,
                PasswordHash = hashedPassword,
                RegisteredAt = DateTime.Now
            };
            repository.Users.Add(user);
            await repository.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(Guid id)
        {
            var user = await repository.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        private string HashPassword(string password)
        {
            using (var md5 = MD5.Create())
            {
                var passwordBytes = Encoding.UTF8.GetBytes(password);
                var hashBytes = md5.ComputeHash(passwordBytes);
                var hash = BitConverter.ToString(hashBytes);
                return hash;
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Authenicate([FromBody] UserRegistrationInfo uInfo)
        {
            var userSession = await this.authenicator.AuthenticateAsync(uInfo.Login, uInfo.Password, CancellationToken.None);
            return this.Ok(userSession);
        }


    }
}
