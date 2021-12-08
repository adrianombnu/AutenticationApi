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
        public IActionResult Cadastrar()
        {


        }


        [HttpPost, Route("login")]
        public IActionResult Login()
        {


        }

        

    }
}
