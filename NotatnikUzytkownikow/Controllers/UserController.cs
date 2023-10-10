using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotatnikUzytkownikow.Dtos;
using NotatnikUzytkownikow.Interfaces;
using NotatnikUzytkownikow.Requests;
using PdfSharpCore.Pdf;
using PdfSharpCore;
using System.Data;
using System.Security.Claims;
using TheArtOfDev.HtmlRenderer.PdfSharp;

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
        ///     Add user
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
        ///     return all users
        /// </summary>
        [HttpGet("GetAllUsers")]
        [ProducesResponseType(typeof(List<FoundUserDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetAllUsers()
        {
            return Ok(await _userService.GetAllUsers());
        }

        /// <summary>
        ///     Update user
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
        ///     Delete user
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
        ///     Returning the user's Id based on his personal data
        /// </summary>
        [HttpGet("GetUserId")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetUserId([FromQuery] CreateUserRequest request)
        {          
            return Ok(await _userService.GetUserId(request));
        }

        /// <summary>
        ///     Generating a report
        /// </summary>
        [HttpGet("GenerateReport")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GenerateReport()
        {
            byte[] pdfBytes = await _userService.GenerateReport();
            DateTime currentDateTime = DateTime.Now;
            string formattedDateTime = currentDateTime.ToString("yyyy-MM-dd HH:mm:ss");
            string filename = formattedDateTime + ".pdf";

            return File(pdfBytes, "application/pdf", filename);
        }


    }
}
