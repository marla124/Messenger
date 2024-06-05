using AutoMapper;
using Messenger.Auth.Dto;
using Messenger.Auth.Models;
using Messenger.Auth.Models.RequestModel;
using Messenger.Auth.Services;
using Microsoft.AspNetCore.Mvc;

namespace Messenger.Auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserService userService, IMapper mapper) : Controller
    {
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var project = mapper.Map<UserViewModel>(await userService.GetById(id, cancellationToken));
            if (project == null)
            {
                return NotFound();
            }
            return Ok(project);
        }

        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteById(Guid id, CancellationToken cancellationToken)
        {
            await userService.DeleteById(id, cancellationToken);
            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateUser(RegisterModel request, CancellationToken cancellationToken)
        {
            var dto = mapper.Map<UserDto>(request);

            await userService.RegisterUser(dto, cancellationToken);
            var user = await userService.GetByEmail(request.Email, cancellationToken);
            return Created($"users/{user.Id}", user);
        }

        [HttpPatch]
        [Route("[action]")]
        public async Task<IActionResult> UpdateUser(UpdateUserRequestViewModel request, CancellationToken cancellationToken)
        {
            var dto = mapper.Map<UserDto>(request);

            return Ok(mapper.Map<UserViewModel>(await userService.Update(dto, cancellationToken)));
        }
    }
}
