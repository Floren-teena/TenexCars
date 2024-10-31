using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TenexCars.DataAccess.Models;
using TenexCars.DataAccess.Repositories.Implementations;
using TenexCars.DataAccess.Repositories.Interfaces;
using TenexCars.DataAccess.ViewModels;
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

        public OperatorController(UserManager<AppUser> userManager, IOperatorRepository operatorRepository, ILogger<OperatorController> logger,
                                  IPhotoService photoService, IVehicleRepository vehicleRepository) 
        {
            _userManager = userManager;
            _operatorRepository = operatorRepository;
            _logger = logger;
            _photoService = photoService;
            _vehicleRepository = vehicleRepository;
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
                //var operatorMemberAdmin = loggedInUser is not null ? await _operatorRepository.GetOperatorMemberByUserId(loggedInUser.Id) : null;
            }
            else
            {
                return Unauthorized();
            }

            return View();
        }
    }
}
