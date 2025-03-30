using DigitalWallet.Application.DTO;
using DigitalWallet.Application.Interfaces;
using DigitalWallet.Application.Models.Responses;
using DigitalWallet.Persistance.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DigitalWallet.Presentation.Controller
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserController(IUserService userService, IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("ListReceivers")]
        public async Task<IActionResult> ListReceivers()
        {
            var userIdStr = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!Guid.TryParse(userIdStr, out var userId))
                return StatusCode(401, new { message = "Token'dan kullanıcı bilgisi alınamadı" });

            var result = await _userService.GetReceiversAsync(userId);
            return Ok(result);
        }
    }
}
