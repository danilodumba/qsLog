using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using qsLibPack.Domain.ValueObjects;
using qsLog.Applications.Services.Interfaces;
using qsLog.Presentetion.Controllers;
using qsLog.Presentetion.Models;

namespace qsLog.Api.Controllers
{
    [Route("api/[controller]")]
    public class ProfileController: ApiController
    {
        private IUserService _userService;
        public ProfileController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var user = await _userService.GetByID(this.UserID);

            var model = new {
                user.Email,
                user.Name,
                user.UserName
            };

            return Ok(model);
        }

        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordModel model)
        {
            await _userService.ChangePassoword(this.UserID, model.OldPassword, new PasswordVO(model.Password, model.ConfirmPassword));
            return NoContent();
        }
    }
}