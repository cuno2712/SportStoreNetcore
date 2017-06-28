using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using WebApplication7.Models.ViewModels;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebApplication7.Models;

namespace WebApplication7.Controllers
{

    [Authorize]
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> userManager;
        private SignInManager<ApplicationUser> signInManager;
        private readonly ILogger _logger;
        private readonly string _externalCookieScheme;
        public AccountController(UserManager<ApplicationUser> useMng, SignInManager<ApplicationUser> signMng, IOptions<IdentityCookieOptions> identityCookieOptions, ILoggerFactory loggerFactory)
        {
            userManager = useMng;
            signInManager = signMng;
            _externalCookieScheme = identityCookieOptions.Value.ExternalCookieAuthenticationScheme;
            _logger = loggerFactory.CreateLogger<AccountController>();

        }
        [AllowAnonymous]
        public ViewResult Login(string returnUrl)
        {
            return View(new LoginModel
            {
                ReturnUrl = returnUrl
            });
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(loginModel.Name, loginModel.Password, loginModel.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _logger.LogInformation(1, "User logged in.");
                    return Redirect(loginModel?.ReturnUrl ?? "/Admin/Index");
                }               
                if (result.IsLockedOut)
                {
                    _logger.LogWarning(2, "User account locked out.");
                    return View("Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(loginModel);
                }
            }
            ModelState.AddModelError("", "Invalid name or password");
            return View(loginModel);
        }
        public async Task<RedirectResult> Logout(string returlUrl="/")
        {
            await signInManager.SignOutAsync();
            return Redirect(returlUrl);
        }
    }
}