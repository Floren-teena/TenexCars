using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TenexCars.DataAccess.Models;
using TenexCars.DataAccess.Repositories.Implementations;
using TenexCars.DataAccess.Repositories.Interfaces;
using TenexCars.DataAccess.ViewModels;
using TenexCars.Interfaces;

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

    }
}
