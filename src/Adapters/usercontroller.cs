using Microsoft.AspNetCore.Mvc;

namespace APIrest.Controllers
{
    [Route("user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IDatabaseConnection _databaseConnection;

        public UserController(IUserService userService, IDatabaseConnection databaseConnection)
        {
            _userService = userService;
            _databaseConnection = databaseConnection;
        }

        [HttpPost("signup")]
        public IActionResult SignUp(User user)
        {
            try
            {
                _userService.SignUp(user, _databaseConnection);
                return Ok("Usuário cadastrado com sucesso!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao cadastrar usuário: {ex.Message}");
            }
        }

        [HttpPost("login")]
        public IActionResult Login(User user)
        {
            try
            {
                _userService.Login(user, _databaseConnection);
                return Ok("Usuário logado com sucesso!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao cadastrar usuário: {ex.Message}");
            }
        }
    }
}
