using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotatnikUzytkownikow.Interfaces;
using NotatnikUzytkownikow.Requests;
using System.Data;
using System.Security.Claims;

namespace NotatnikUzytkownikow.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService  _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        /// <summary>
        ///     Dodanie użytownika
        /// </summary>
        [HttpPost("CreateUser")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> CreateUser(CreateUserRequest request)
        {
            await _userService.CreateUser(request);
            return NoContent();
        }

        /// <summary>
        ///     Usunięcie użytownika
        /// </summary>
        [HttpPost("DeleteUser")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteUser(Guid id)
        {
            await _userService.DeleteUser(id);
            return NoContent();
        }


    }
}
