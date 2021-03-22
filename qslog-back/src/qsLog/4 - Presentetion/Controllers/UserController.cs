using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using qsLibPack.Domain.ValueObjects;
using qsLog.Applications.Models;
using qsLog.Applications.Services.Interfaces;
using qsLog.Presentetion.Models;

namespace qsLog.Presentetion.Controllers
{
    public class UserController: ApiController
    {
        readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserModel model)
        {
            await _userService.Create(model);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] UserModel model, Guid id)
        {
            await _userService.Update(model, id);
            return NoContent();
        }

        [HttpGet]
        public IActionResult ListAll()
        {
            return Ok(_userService.ListAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var model = await _userService.GetByID(id);
            if (model.IsValid())
            {
                return Ok(model);
            }

            return NotFound("Usuario nao encontrado");
        }

        [HttpGet("name")]
        public async Task<IActionResult> ListByName(string name)
        {
            var model = await _userService.GetByName(name);
            return Ok(model);
        }

        [HttpPut("{id}/change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordModel model, Guid id)
        {
            await _userService.ChangePassoword(id, model.Old, new PasswordVO(model.New, model.ConfirmNew));
            return NoContent();
        }

        [HttpPut("{id}/reset-password")]
        public async Task<IActionResult> ResetPassowrd(Guid id)
        {
            await _userService.ResetPassword(id);
            return NoContent();
        }


    }
}