// AccountController.cs


using CoblentzContext.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoblentzContext.Controllers
{
    //  Controller for account management
    public class AccountController : Controller
    {
        // User manager
        private UserManager<User> userManager;

        // Sign in manager
        private SignInManager<User> signInManager;

        // Constructor
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            // Initialize user manager
            this.userManager = userManager;
            // Initialize sign in manager
            this.signInManager = signInManager;
        }

        // GET: /Account/Login
        [HttpGet]
        public IActionResult Login(string returnUrl = "")
        {
            // Create login view model
            LoginViewModel model = new LoginViewModel();
            // Set return URL
            model.ReturnURL = returnUrl;
            // Return the view model
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            
            if (ModelState.IsValid)
            {
                // Check if the user is valid
                var result = await signInManager.PasswordSignInAsync(
                    model.UserName, model.Password, isPersistent: model.RememberMe, lockoutOnFailure: false);

                // If the user is valid
                if (result.Succeeded)
                {
                    // Show welcome message
                    TempData["ShowWelcome"] = true;

                    // If the user is valid
                    if (!string.IsNullOrEmpty(model.ReturnURL))
                    {
                        // Redirect to the return URL
                        return Redirect(model.ReturnURL);
                    }
                    else
                    {
                        // Redirect to the home page
                        return RedirectToAction("Index", "Home");
                    }
                }
                // Add error message
                ModelState.AddModelError("", "Invalid username/password.");
            }
            // Return the view model
            return View(model);
        }

        // GET: /Account/Register
        [HttpGet]
        public IActionResult Register()
        {
            // Return the view model
            return View(new RegisterViewModel());
        }

        // POST: /Account/Register
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            // Check if the model is valid
            if (ModelState.IsValid)
            {
                // Create user
                User user = new User { UserName = model.UserName };
                // Create user
                IdentityResult result = await userManager.CreateAsync(user, model.Password);
                // If the user is valid
                if (result.Succeeded)
                {
                    // Show welcome message
                    // Show welcome message
                    TempData["ShowWelcome"] = true;
                    // Sign in user
                    await signInManager.SignInAsync(user, isPersistent: false);
                    // Redirect to home page
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    // Add error message
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            // Return the view model
            return View(model);
        }

        // POST: /Account/Logout
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            // Sign out user
            await signInManager.SignOutAsync();
            // Redirect to home page
            return RedirectToAction("Index", "Home");
        }

        // GET: /Account/AccessDenied
        public IActionResult AccessDenied()
        {
            // Return the view model
            return View();
        }
    }
}
