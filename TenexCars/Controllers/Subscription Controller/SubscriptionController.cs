using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TenexCars.Controllers.Subscriber_Controller;
using TenexCars.DataAccess.Models;
using TenexCars.DataAccess.Repositories.Interfaces;
using TenexCars.Models.ViewModels;

namespace TenexCars.Controllers.Subscription_Controller
{
    public class SubscriptionController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IOperatorRepository _operatorRepository;
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly ILogger<SubscriberController> _logger;

        public SubscriptionController(UserManager<AppUser> userManager, IOperatorRepository operatorRepository, ISubscriptionRepository subscriptionRepository,
                                      ILogger<SubscriberController> logger)
        {
            _userManager = userManager;
            _operatorRepository = operatorRepository;
            _subscriptionRepository = subscriptionRepository;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> OperatorSubscription(OperatorSubscriptionViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            Operator? existingOperator = null!;

            if (user.Type == "Main_Operator")
            {
                existingOperator = await _operatorRepository.GetOperatorByUserId(user.Id);
            }
            else if (user.Type == "Operator_Team_Member")
            {
                var operatorMember = await _operatorRepository.GetOperatorMemberByUserId(user.Id);
                existingOperator = await _operatorRepository.GetOperatorById(operatorMember!.OperatorId!);
                ViewBag.CompanyName = existingOperator!.CompanyName;
            }
            else
            {
                return BadRequest("Invalid user type.");
            }

            if (existingOperator == null)
            {
                _logger.LogInformation("Operator ID is required.");
                return BadRequest();
            }

            var subscription = await _subscriptionRepository.GetSubscriptionForOperator(existingOperator.Id);
            if (subscription == null)
            {
                TempData["error"] = "Operator does not have an existing subscription";
                return View(new List<OperatorSubscriptionViewModel> { model });
            }

            var subscriptions = await _subscriptionRepository.GetAllSubscriptionsForOperator(existingOperator.Id);
            if (subscriptions == null || !subscriptions.Any())
            {
                _logger.LogInformation("No subscriptions found for the given operator.");
                return NotFound();
            }

            var subscriptionForOperator = subscriptions.Select(sub => new OperatorSubscriptionViewModel
            {
                Id = sub.SubscriberId!,
                Customer = $"{sub.Subscriber!.FirstName} {sub.Subscriber.LastName}",
                VehicleRequest = $"{sub.Vehicle!.Make} {sub.Vehicle.Model}",
                RequestDate = sub.RequestDate,
                TermStart = sub.TermStart,
                TermEnd = sub.TermEnd,
                PickupLocation = sub.PickUpLocation,
                VehicleInfo = sub.Vehicle.VIN!,
                DriversLicenseUrlFront = sub.DLUrlFront,
                DriversLicenseUrlBack = sub.DLUrlBack,
                SubscriptionStatus = sub.SubscriptionStatus,
                VehicleColor = sub.Vehicle.Color,
                Email = sub.Subscriber.Email
            }).ToList();


            return View(subscriptionForOperator);
        }
    }
}
