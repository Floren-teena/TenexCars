using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TenexCars.DataAccess.Enums;
using TenexCars.DataAccess.Models;
using TenexCars.DataAccess.Repositories.Interfaces;
using TenexCars.DTOs;

namespace TenexCars.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ILogger<AccountController> _logger;
        private readonly ISubscriberRepository _subscriberRepository;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ILogger<AccountController> logger, 
                                ISubscriberRepository subscriberRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _subscriberRepository = subscriberRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDto loginVm)
        {
            if (!ModelState.IsValid)
            {
                return View(loginVm);
            }

            var user = await _userManager.FindByEmailAsync(loginVm.Username!);
            if (user == null)
            {
                _logger.LogWarning("User not found: {Email}", loginVm.Username);
                TempData["Error"] = "Invalid credentials";
                return View(loginVm);
            }

            _logger.LogInformation("User found: {Email}", loginVm.Username);

            var result = await _signInManager.PasswordSignInAsync(user, loginVm.Password!, loginVm.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                var roles = await _userManager.GetRolesAsync(user);
                _logger.LogInformation("User roles: {Roles}", string.Join(", ", roles));

                if (roles.Contains("Main_Subscriber"))
                {
                    var subscriber = await _subscriberRepository.GetSubscriberByUserId(user.Id);

                    /*var subscription = subscriber is not null ? await _subscriptionRepository.GetSubscriptionBySubcriber(subscriber.Id) : null;
                    if (subscription is not null && subscription.SubscriptionStatus == SubscriptionStatus.DLNeeded)
                    {
                        _logger.LogInformation("Redirecting to Complete Reservation Page ...");
                        return RedirectToAction("CompleteReservation", "Subscriber");
                    }*/
                    _logger.LogInformation("Redirecting to Subscriber page");
                    return RedirectToAction("Profile", "Subscriber");
                }
                else if (roles.Contains("Main_Operator"))
                {
                    _logger.LogInformation("Redirecting to Operator page");
                    return RedirectToAction("CreateVehicle", "Operator"); //change back to dashboard when i create it
                }
                else if (roles.Contains("Operator_Team_Member"))
                {
                    _logger.LogInformation("Redirecting to Operator page");
                    return RedirectToAction("OperatorSubscription", "Subscription");
                }
                else if (roles.Contains("Tenex_Admin"))
                {
                    _logger.LogInformation("Redirecting to Admin page");
                    return RedirectToAction("Admin", "Admin");
                }
                else
                {
                    _logger.LogWarning("User does not have the required role");
                    TempData["Error"] = "You do not have permission to access this resource.";
                    await _signInManager.SignOutAsync();
                    return View(loginVm);
                }
            }

            _logger.LogWarning("Invalid login attempt for user: {Email}", loginVm.Username);
            TempData["Error"] = "Invalid credentials";
            return View(loginVm);
        }
    }
}
