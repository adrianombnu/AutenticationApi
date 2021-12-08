using AutenticationApi.DTOs;
using AutenticationApi.Entidades;
using AutenticationApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

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

        [HttpPost, AllowAnonymous]
        public IActionResult Cadastrar([FromBody] NewUserDTO userDTO)
        {
            return Created("", _userService.Create(
                new User
                {
                    Role = userDTO.Role,
                    UserName = userDTO.Username,
                    Password = userDTO.Password
                }));
        }

        [HttpPost, AllowAnonymous, Route("login")]
        public IActionResult Login([FromBody] UserLoginDTO loginDTO)
        {
            return Ok(_userService.Login(loginDTO.Username, loginDTO.Password));

        }

        [HttpGet, Route("autenticado"), Authorize]
        public string Autorizado() => $"autenticado {User.Identity.Name }";

        [HttpGet, Authorize(Roles = "admin"), Route("admin")]
        public string Admin() => $"autenticado admin";

        [HttpGet, Authorize(Roles = "funcionario"), Route("funcionario")]
        public string Funcionario() => $"autenticado funcionario";

        [HttpGet, Authorize(Roles = "admin, funcionario"), Route("both")]
        public string Both() => $"autenticado funcionario, admin";


        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_userService.Get());

        }

        [HttpGet, Route("{id}/login")]
        public IActionResult Get(Guid id)
        {
            return Ok(_userService.Get(id));

        }



    }
}
