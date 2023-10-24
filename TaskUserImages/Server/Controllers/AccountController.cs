using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Services.Interfaces;
using TaskUserImages.Server.Areas.Identity.Pages.Account;
using TaskUserImages.Server.Data;

namespace TaskUserImages.Server.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserService _userService;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly ILogger<RegisterModel> _logger;
        public AccountController(
            UserManager<ApplicationUser> userManager,
            IUserService userService,
            IUserStore<ApplicationUser> userStore,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger)
        {
            _userManager = userManager;
            _userService=userService;
            _userStore = userStore;
            _signInManager = signInManager;
            _logger = logger;

        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterModel.InputModel model)
        {
            var regModel = new RegisterModel(_userManager, _userService, _userStore, _signInManager, _logger);
            regModel.Input = model;
           
            return await regModel.OnPostAsync();
        }
    }
}
