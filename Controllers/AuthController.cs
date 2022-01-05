using Microsoft.AspNetCore.Mvc;
using MyEmotionsApi.Data.Interfaces;
using MyEmotionsApi.Entities;
using MyEmotionsApi.Services.Interfaces;
using MyEmotionsApi.ViewModels;
using System;

namespace MyEmotionsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserRepository _userRepository;

        public AuthController(IAuthService authService, IUserRepository userRepository)
        {
            _authService = authService;
            _userRepository = userRepository;
        }

        [HttpPost("login")]
        public ActionResult<AuthDataViewModel> Post([FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = _userRepository.GetSingle(u => u.Username == model.Username);
             
            if (user == null)
            {
                return BadRequest(new { username = "no user with this username" });
            }

            var passwordValid = _authService.VerifyPassword(model.Password, user.Password);

            if (!passwordValid)
            {
                return BadRequest(new { password = "invalid password" });
            }

            return _authService.GetAuthData(user.Id);
        }

        [HttpPost("register")]
        public ActionResult<AuthDataViewModel> Post([FromBody] RegisterViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var usernameUniq = _userRepository.IsValidUsername(model.Username);
            
            if (!usernameUniq) return BadRequest(new { username = "user with this email already exists" });

            var id = Guid.NewGuid().ToString();
            var user = new User
            {
                Id = id,
                Username = model.Username,
                Name = model.Name,
                Password = _authService.HashPassword(model.Password)
            };

            _userRepository.Add(user);
            _userRepository.Commit();

            return _authService.GetAuthData(user.Id);
        }
    }
}
