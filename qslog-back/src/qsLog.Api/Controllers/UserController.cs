using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using qsLibPack.Domain.ValueObjects;
using qsLog.Applications.Models;
using qsLog.Applications.Services.Interfaces;
using qsLog.Presentetion.Models;
using System.Linq.Expressions;
using System.Linq;

namespace qsLog.Presentetion.Controllers
{
    //[Authorize(Roles = "Administrator")]
    [Route("api/[controller]")]
    public class UserController: ApiController
    {
        readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserModel model)
        {
            var id = await _userService.Create(model);
            if (id == Guid.Empty)
                return NoContent();
            
            return CreatedAtRoute("GetById", new {id}, id);
        }

        [AllowAnonymous]
        [HttpGet("admin")]
        public async Task<IActionResult> ValidateAdminUser()
        {
            await _userService.CreateAdminUser();
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] UserModel model, Guid id)
        {
            await _userService.Update(model, id);
            return NoContent();
        }

        [HttpGet("{id}", Name="GetById")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var model = await _userService.GetByID(id);
            if (model != null)
            {
                return Ok(model);
            }

            return NotFound("Usuario nao encontrado");
        }

        [HttpGet()]
        public IActionResult List(string search)
        {
            var model =  _userService.List(search);
            return Ok(model.OrderBy(x => x.Name));
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(Guid id)
        {
            await _userService.Remove(id);
            return NoContent();
        }
    }
}