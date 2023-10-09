using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotatnikUzytkownikow.Dtos;
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
        ///     Wyświetlenie wszystkich użytkowników
        /// </summary>
        [HttpGet("GetAllUsers")]
        [ProducesResponseType(typeof(List<FoundUserDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetAllUsers()
        {
            return Ok(await _userService.GetAllUsers());
        }

        /// <summary>
        ///     Update użytownika
        /// </summary>
        [HttpPut("UpdateUser")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateUser(UpdateUserRequest request)
        {
            await _userService.UpdateUser(request);
            return NoContent();
        }

        /// <summary>
        ///     Usunięcie użytownika
        /// </summary>
        [HttpDelete("DeleteUser/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteUser(Guid id)
        {
            await _userService.DeleteUser(id);
            return NoContent();
        }

        /// <summary>
        ///     Zwrócenie Id użytkownika na podstawie jego danych osobowych
        /// </summary>
        [HttpGet("GetUserId")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetUserId([FromQuery] CreateUserRequest request)
        {          
            return Ok(await _userService.GetUserId(request));
        }



    }
}
