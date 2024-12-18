using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TenexCars.DataAccess.Models;
using TenexCars.DataAccess.Repositories.Implementations;
using TenexCars.DataAccess.Repositories.Interfaces;
using TenexCars.DataAccess.ViewModels;
using TenexCars.DTOs;
using TenexCars.Interfaces;
using TenexCars.Models.ViewModels;

namespace TenexCars.Controllers.Operator_Controller
{
    public class OperatorController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IOperatorRepository _operatorRepository;
        private readonly ILogger<OperatorController> _logger;
        private readonly IPhotoService _photoService;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IEmailService _emailService;
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly ISubscriberRepository _subscriberRepository;

        public OperatorController(UserManager<AppUser> userManager, IOperatorRepository operatorRepository, ILogger<OperatorController> logger,
                                  IPhotoService photoService, IVehicleRepository vehicleRepository, IEmailService emailService,
                                  ISubscriptionRepository subscriptionRepository, ISubscriberRepository subscriberRepository) 
        {
            _userManager = userManager;
            _operatorRepository = operatorRepository;
            _logger = logger;
            _photoService = photoService;
            _vehicleRepository = vehicleRepository;
            _emailService = emailService;
            _subscriptionRepository = subscriptionRepository;
            _subscriberRepository = subscriberRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CreateVehicle()
        {
            var user = await _userManager.GetUserAsync(User);

            var operatorUser = user is not null ? await _operatorRepository.GetOperatorByUserId(user.Id) : null;

            var viewModel = new CreateVehicleViewModel
            {
                OperatorId = operatorUser is not null ? operatorUser.Id : null,
                OperatorLogoUrl = operatorUser is not null ? operatorUser.CompanyLogo : null,
                CarDealerName = operatorUser is not null ? operatorUser.BusinessName : null

            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateVehicle(CreateVehicleViewModel vehicleViewModel)
        {
            _logger.LogInformation($"Creating Vehicle ...");
            if (!ModelState.IsValid)
            {
                return View(vehicleViewModel);
            }
            try
            {
                var user = await _userManager.GetUserAsync(User);

                var operatorUser = user is not null ? await _operatorRepository.GetOperatorByUserId(user.Id) : null;

                var vehicleImage = await _photoService.AddPhotoAsync(vehicleViewModel.ImageUrl!);

                var newVehicle = new Vehicle
                {
                    Operator = operatorUser,
                    OperatorId = operatorUser!.Id,
                    Make = vehicleViewModel.Make,
                    Model = vehicleViewModel.Model,
                    PlateNumber = vehicleViewModel.PlateNumber,
                    CarDescription = vehicleViewModel.CarDescription,
                    ChasisNumber = vehicleViewModel.ChasisNumber,
                    SeatNumbers = vehicleViewModel.SeatNumbers,
                    Mileage = vehicleViewModel.Mileage,
                    TrunkSize = vehicleViewModel.TrunkSize,
                    Color = vehicleViewModel.Color,
                    VIN = vehicleViewModel.VIN,
                    PickupAddress = vehicleViewModel.PickupAddress,
                    City = vehicleViewModel.City,
                    ZIP = vehicleViewModel.ZIP,
                    CarDealerName = operatorUser.BusinessName,
                    FinacialStartDate = vehicleViewModel.FinacialStartDate,
                    FinacialEndDate = vehicleViewModel.FinacialEndDate,
                    CarWarrantyOverview = vehicleViewModel.CarWarrantyOverview,
                    Torque = vehicleViewModel.Torque,
                    TransmissionType = vehicleViewModel.TransmissionType,
                    Horsepower = vehicleViewModel.Horsepower,
                    TurningDiameter = vehicleViewModel.TurningDiameter,
                    CurbWeight = vehicleViewModel.CurbWeight,
                    DiscBrakes = vehicleViewModel.DiscBrakes,
                    TransmissionSpeed = vehicleViewModel.TransmissionSpeed,
                    DriveAxleRatio = vehicleViewModel.DriveAxleRatio,
                    StabilityControl = vehicleViewModel.StabilityControl,
                    RangeOfFullCharge = vehicleViewModel.RangeOfFullCharge,
                    CargoSpace = vehicleViewModel.CargoSpace,
                    TouchScreenDisplay = vehicleViewModel.TouchScreenDisplay,
                    DriverLumbarSupport = vehicleViewModel.DriverLumbarSupport,
                    NumberOfSpeakers = vehicleViewModel.NumberOfSpeakers,
                    Radio = vehicleViewModel.Radio,
                    AndroidAuto_AppleCarPlay = vehicleViewModel.AndroidAuto_AppleCarPlay,
                    TouchScreenNavSystem = vehicleViewModel.TouchScreenNavSystem,
                    ProjectorBeamLedHeadlight = vehicleViewModel.ProjectorBeamLedHeadlight,
                    RemoteKeylessEntry = vehicleViewModel.RemoteKeylessEntry,
                    StandardLowTirePressureWarning = vehicleViewModel.StandardLowTirePressureWarning,
                    BluetoothSystem = vehicleViewModel.BluetoothSystem,
                    WheelDrive = vehicleViewModel.WheelDrive,
                    EngineType = vehicleViewModel.EngineType,
                    State = vehicleViewModel.State,
                    DcFastCharging = vehicleViewModel.DcFastCharging,
                    HomeCharger = vehicleViewModel.HomeCharger,
                    SeatHeater = vehicleViewModel.SeatHeater,
                    Cartype = vehicleViewModel.Cartype,
                    ReservationFee = vehicleViewModel.ReservationFee,
                    TotalCostOfCar = vehicleViewModel.TotalCostOfCar,
                    ActivationFee = vehicleViewModel.ActivationFee,
                    MonthlyCost = vehicleViewModel.MonthlyCost,
                    MarketValue = vehicleViewModel.MarketValue,
                    DecrementMarketValue = vehicleViewModel.DecrementMarketValue,
                    ImageUrl = vehicleImage.Url.ToString()
                };

                await _vehicleRepository.AddVehicleAsync(newVehicle);
                _logger.LogInformation("Vehicle added successfully");
                TempData["success"] = "Vehicle added successfully";
                return RedirectToAction("OperatorInventoryPage");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "Something went wrong");
                TempData["error"] = ex.Message;
                return View(vehicleViewModel);

            }

        }

        [HttpGet]
        public async Task<IActionResult> CarDetails(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var vehicle = await _vehicleRepository.GetVehicleById(id);

            if (vehicle == null)
            {
                return NotFound();
            }
            var carDetailsViewModel = new CarDetailsViewModel
            {
                Id = vehicle.Id,
                Make = vehicle.Make,
                Model = vehicle.Model,
                MarketValue = vehicle.MarketValue.ToString(),
                ImageUrl = vehicle.ImageUrl,
                Color = vehicle.Color,
                SeatNumbers = vehicle.SeatNumbers,
                TrunkSize = vehicle.TrunkSize,
                DcFastCharging = vehicle.DcFastCharging,
                HomeCharger = vehicle.HomeCharger,
                RangeOfFullCharge = vehicle.RangeOfFullCharge,
                CarDescription = vehicle.CarDescription,
                TouchScreenDisplay = vehicle.TouchScreenDisplay,
                WheelDrive = vehicle.WheelDrive,
                DriverLumbarSupport = vehicle.DriverLumbarSupport,
                SeatHeater = vehicle.SeatHeater,
                Radio = vehicle.Radio,
                AndroidAuto_AppleCarPlay = vehicle.AndroidAuto_AppleCarPlay,
                NumberOfSpeakers = vehicle.NumberOfSpeakers,
                TouchScreenNavSystem = vehicle.TouchScreenNavSystem,
                BluetoothSystem = vehicle.BluetoothSystem,
                ProjectorBeamLedHeadlight = vehicle.ProjectorBeamLedHeadlight,
                RemoteKeylessEntry = vehicle.RemoteKeylessEntry,
                StandardLowTirePressureWarning = vehicle.StandardLowTirePressureWarning,
                Torque = vehicle.Torque,
                Horsepower = vehicle.Horsepower,
                EngineType = vehicle.EngineType,
                DiscBrakes = vehicle.DiscBrakes,
                StabilityControl = vehicle.StabilityControl,
                TransmissionSpeed = vehicle.TransmissionSpeed,
                TurningDiameter = vehicle.TurningDiameter,
                CurbWeight = vehicle.CurbWeight,
                TransmissionType = vehicle.TransmissionType,
                DriveAxleRatio = vehicle.DriveAxleRatio,
                CarWarrantyOverview = vehicle.CarWarrantyOverview
            };
            return View(carDetailsViewModel);

        }

        [HttpGet]
        public async Task<IActionResult> OperatorDashboard(string operatorId)
        {
            var loggedInUser = await _userManager.GetUserAsync(User);

            if (loggedInUser == null)
            {
                return Unauthorized();
            }
            if (loggedInUser.Type == "Main_Operator")
            {
                var operatorUser = loggedInUser is not null ? await _operatorRepository.GetOperatorByUserId(loggedInUser.Id) : null;

                var totalVehicles = operatorUser is not null ? await _operatorRepository.GetTotalNumberOfCars(operatorUser.Id) : 0;
                var totalSubscribers = operatorUser is not null ? await _operatorRepository.GetTotalNumberOfSubscribers(operatorUser.Id) : 0;
                var totalReservedCars = operatorUser is not null ? await _operatorRepository.GetTotalNumberOfReservedCars(operatorUser.Id) : 0;
                var totalActiveCars = operatorUser is not null ? await _operatorRepository.GetTotalNumberOfActiveCars(operatorUser.Id) : 0;

                var viewModel = new OperatorDashboardViewModel
                {
                    OperatorId = operatorUser!.Id,
                    TotalNumberOfVehicles = totalVehicles,
                    TotalNumberOfSubscribers = totalSubscribers,
                    TotalNumberOfReservedCars = totalReservedCars,
                    TotalNumberOfActiveCars = totalActiveCars,
                    CurrentMonthStats = new List<int> { 500, 700, 1200, 1500, 2000, 2500, 3000, 3500, 4000, 4500 },
                    PastMonthStats = new List<int> { 300, 400, 500, 600, 700, 800, 900, 1000, 1100, 1200 },
                    OperatorLogo = operatorUser.CompanyLogo!
                };

                return View(viewModel);

            }
            else if (loggedInUser.Type == "Operator_Team_Member")
            {
                var operatorMemberAdmin = loggedInUser is not null ? await _operatorRepository.GetOperatorMemberByUserId(loggedInUser.Id) : null;
            }
            else
            {
                return Unauthorized();
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ManageOperatorMembers()
        {
            var user = await _userManager.GetUserAsync(User);
            var operatorUser = user is not null ? await _operatorRepository.GetOperatorByUserId(user.Id) : null;
            var model = new OperatorMemberViewModel
            {
                OperatorMembers = operatorUser is not null ? (await _operatorRepository.GetAllMembersForOperatorAsync(operatorUser.Id)).ToList() : null
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddOperatorMembers(OperatorMemberViewModel model)
        {
            var loggedInUser = await _userManager.GetUserAsync(User);
            var operatorUser = loggedInUser is not null ? await _operatorRepository.GetOperatorByUserId(loggedInUser.Id) : null;

            var user = new AppUser
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                //OperatorId = operatorUser?.Id,
                Type = model.Role == "Admin" ? "Main_Operator" : "Operator_Team_Member"
            };
            var result = await _userManager.CreateAsync(user);

            if (result.Succeeded)
            {
                if (model.Role == "Admin")
                {
                    await _userManager.AddToRoleAsync(user, "Main_Operator");

                    var newOperator = new Operator
                    {
                        FirstName = model.FirstName!,
                        LastName = model.LastName!,
                        Email = model.Email!,
                        AppUserId = user.Id
                    };

                    await _operatorRepository.AddOperatorAsync(newOperator);
                }
                else
                {
                    await _userManager.AddToRoleAsync(user, "Operator_Team_Member");

                    var newOperatorMember = new OperatorMember
                    {
                        FirstName = model.FirstName!,
                        LastName = model.LastName!,
                        Email = model.Email!,
                        AppUserId = user.Id,
                        Role = model.Role,
                        OperatorId = operatorUser!.Id,
                        Operator = operatorUser
                    };
                    await _operatorRepository.AddOperatorMemberAsync(newOperatorMember);
                }

                // Generate password reset token
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var encodedToken = Uri.EscapeDataString(token);
                var resetPasswordUrl = Url.Action("SetNewPassword", "Account", new { userId = user.Id, token = encodedToken }, protocol: HttpContext.Request.Scheme);

                var emailBody = $"<div style=\"color: black;\">" +
                                $"Hello {model.FirstName},<br><br>" +
                                $"Welcome to Tenex! We are excited to have you join us as an Operator {model.Role}. " +
                                "To get started, please set your password by clicking the link below:" +
                                "<br><br>" +
                                $"<a href=\"{resetPasswordUrl}\" style=\"color: blue; text-decoration: none;\">Set Password Link</a><br><br>" +
                                "This link will take you to a secure page where you can create your password and complete your account setup. " +
                                "<br><br>" +
                                "Welcome aboard, we look forward to working with you!" +
                                "<br><br>" +
                                "Best regards,<br>" +
                                "The Tenex Team" +
                                "</div>";

                var emailContent = new EmailDto
                {
                    To = user.Email,
                    Subject = "Welcome to Tenex! Set Your Password to Get Started",
                    Body = emailBody
                };
                await _emailService.SendOperatorSetPasswordEmailAsync(emailContent);

                return RedirectToAction("ManageOperatorMembers");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    _logger.LogError($"Error: {error.Code} - {error.Description}");
                }

                ModelState.AddModelError(string.Empty, "Failed to create user. Please check the errors and try again.");
            }
            model.OperatorMembers = (await _operatorRepository.GetAllOperatorMembersAsync()).ToList();
            return View("ManageOperatorMembers", model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteOperatorMember(string email)
        {
            await _operatorRepository.DeleteOperatorMemberAsync(email);
            return RedirectToAction("ManageOperatorMembers");
        }

        [HttpGet]
        public async Task<IActionResult> OperatorInventoryPage(OperatorInventoryViewModel model)
        {
            var loggedInUser = await _userManager.GetUserAsync(User);
            if (loggedInUser == null)
            {
                return Unauthorized();
            }

            Operator? existingOperator = null;

            if (loggedInUser.Type == "Main_Operator")
            {
                existingOperator = await _operatorRepository.GetOperatorByUserId(loggedInUser.Id);
                ViewBag.CompanyName = existingOperator!.CompanyName;
            }
            else if (loggedInUser.Type == "Operator_Team_Member")
            {
                var operatorMember = await _operatorRepository.GetOperatorMemberByUserId(loggedInUser.Id);
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

            //var operatorUser = loggedInUser is not null ? await _operatorRepository.GetOperatorByUserId(loggedInUser.Id) : null;
            var vehicles = existingOperator is not null ? await _vehicleRepository.GetVehiclesByOperator(existingOperator.Id) : null;
            if (vehicles == null)
            {
                TempData["error"] = "Operator does not have existing Vehicles, proceed to add new vehicles";
                return View(new List<OperatorInventoryViewModel> { model });
            }

            var viewModelList = new List<OperatorInventoryViewModel>();

            foreach (var vehicle in vehicles)
            {
                // Find the subscription related to the current vehicle
                var subscription = await _subscriptionRepository.GetSubscriptionForVehicle(vehicle.Id);

                var subscriber = subscription is not null ? await _subscriberRepository.GetSubscriberByIdAsync(subscription.SubscriberId!) : null;

                var viewModel = new OperatorInventoryViewModel
                {
                    VehicleId = vehicle.Id,
                    VehicleName = $"{vehicle.Make} {vehicle.Model}",
                    Status = subscription is not null ? subscription.SubscriptionStatus : "",
                    TrackerStatus = subscription is not null ? "Active" : "Inactive",
                    SubscriberId = subscriber is not null ? subscriber.Id : null,
                    SubscriberName = subscriber is not null ? $"{subscriber.FirstName} {subscriber.LastName}" : ""
                };

                viewModelList.Add(viewModel);
            }

            // Pass the view model list to the view
            return View(viewModelList);
        }
    }
}
