using AutenticationApi.DTOs;
using AutenticationApi.Entidades;
using AutenticationApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace AutenticationApi.Controllers
{
    [ApiController, Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public IActionResult Cadastrar([FromBody] NewUserDTO userDTO)
        {
            return Created("", _userService.Create(
                new User
                {
                    Role = userDTO.Role,
                    UserName = userDTO.Username,
                    Password = userDTO.Passoword
                }));
        }

        [HttpPost, Route("login")]
        public IActionResult Login([FromBody] UserLoginDTO loginDTO)
        {
            return Ok(_userService.Login(loginDTO.Username, loginDTO.Password));

        }

        

    }
}
