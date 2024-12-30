﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TenexCars.DataAccess.Enums;
using TenexCars.DataAccess.Models;
using TenexCars.DataAccess.Repositories.Implementations;
using TenexCars.DataAccess.Repositories.Interfaces;
using TenexCars.Interfaces;
using TenexCars.Models.ViewModels;

namespace TenexCars.Controllers.Subscriber_Controller
{
    public class SubscriberController : Controller
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly ILogger<SubscriberController> _logger;
        private readonly IOperatorRepository _operatorRepository;
        private readonly UserManager<AppUser> _manager;
        private readonly ISubscriberRepository _subscriberRepository;
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IVehicleRequestRepository _vehicleRequestRepository;
        private readonly IPhotoService _photoService;
        private readonly ICoSubscriberRepository _coSubscriberRepository;

        public SubscriberController(IVehicleRepository vehicleRepository, ILogger<SubscriberController> logger, IOperatorRepository operatorRepository,
                                    UserManager<AppUser> userManager, ISubscriberRepository subscriberRepository, ISubscriptionRepository subscriptionRepository,
                                    IVehicleRequestRepository vehicleRequestRepository, IPhotoService photoService, ICoSubscriberRepository coSubscriberRepository)
        {
            _vehicleRepository = vehicleRepository;
            _logger = logger;
            _operatorRepository = operatorRepository;
            _manager = userManager;
            _subscriberRepository = subscriberRepository;
            _subscriptionRepository = subscriptionRepository;
            _vehicleRequestRepository = vehicleRequestRepository;
            _photoService = photoService;
            _coSubscriberRepository = coSubscriberRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ReserveCar(string id)
        {
            var vehicle = await _vehicleRepository.GetVehicleById(id);

            var response = new ReserveCarViewModel
            {
                VehicleId = vehicle!.Id,
                ImageUrl = vehicle.ImageUrl,
            };

            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> ReserveCar(ReserveCarViewModel rcvm)
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Invalid input data!";
                return View(rcvm);
            }

            _logger.LogInformation("Reserving Car ...");
            try
            {
                var vehicle = await _vehicleRepository.GetVehicleById(rcvm.VehicleId!);

                var operatorEntity = vehicle is not null ? await _operatorRepository.GetOperatorById(vehicle.OperatorId!) : null;

                var newUser = new AppUser
                {
                    FirstName = rcvm.FirstName,
                    LastName = rcvm.LastName,
                    Email = rcvm.Email,
                    PhoneNumber = rcvm.PhoneNumber,
                    UserName = rcvm.Email,
                    //OperatorId = operatorEntity?.Id,
                    Type = "Main_Subscriber"

                };

                if (rcvm.Password != rcvm.ConfirmPassword)
                {
                    _logger.LogError("Password mismatch!");
                    TempData["error"] = "Password mismatch!";
                    return View(rcvm);
                }

                var result = await _manager.CreateAsync(newUser, rcvm.Password!);

                await _manager.AddToRoleAsync(newUser, "Main_Subscriber");

                if (result.Succeeded)
                {
                    var subscriber = new Subscriber
                    {
                        FirstName = newUser.FirstName!,
                        LastName = newUser.LastName!,
                        PhoneNumber = newUser.PhoneNumber,
                        Email = newUser.Email!,
                        AppUser = newUser,
                        AppUserId = newUser.Id,
                        HomeAddress = rcvm.HomeAddress,
                        City = rcvm.City,
                        Country = rcvm.Country,
                        State = rcvm.State,
                        ZipCode = rcvm.ZipCode
                    };

                    var newSubscriber = await _subscriberRepository.AddSubscriberAsync(subscriber);

                    var subscription = new Subscription
                    {
                        Subscriber = newSubscriber,
                        SubscriberId = newSubscriber.Id,
                        Vehicle = vehicle is not null ? vehicle : null,
                        VehicleId = vehicle is not null ? vehicle.Id : "",
                        OperatorId = operatorEntity is not null ? operatorEntity.Id : "",
                        Operator = operatorEntity,
                        SubscriptionStatus = SubscriptionStatus.DLNeeded

                    };

                    await _subscriptionRepository.AddSubscriptionAsync(subscription);

                    var newRequestLog = new VehicleRequest
                    {
                        Subscriber = newSubscriber,
                        SubscriberId = newSubscriber.Id,
                        Vehicle = vehicle is not null ? vehicle : null,
                        VehicleId = vehicle is not null ? vehicle.Id : "",
                    };

                    await _vehicleRequestRepository.AddVehicleRequestLog(newRequestLog);

                    _logger.LogInformation("Reservation Successful!");
                    TempData["success"] = "Reservation Successful! Please login to complete reservation";
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    _logger.LogError("Ensure you filled all fields accurately!");
                    TempData["error"] = "Ensure you filled all fields accurately!";
                    return View(rcvm);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "Something went wrong when reserving car");
                TempData["error"] = "Something went wrong when reserving car. Contact Support!";

                return View(rcvm);
            }
        }

        [HttpGet]
        public async Task<IActionResult> CompleteReservation()
        {
            var user = await _manager.GetUserAsync(User);
            var subscriber = user is not null ? await _subscriberRepository.GetSubscriberByUserId(user.Id) : null;

            var getExistingSubscription = subscriber is not null ? await _subscriptionRepository.GetSubscriptionBySubcriber(subscriber.Id) : null;
            var vehicle = getExistingSubscription is not null ? await _vehicleRepository.GetVehicleById(getExistingSubscription.VehicleId!) : null;

            var reserve = new CompleteReservationViewModel
            {
                AppUserId = user!.Id,
                FirstName = user.FirstName,
                VehicleName = vehicle is not null ? $"{vehicle.Make} {vehicle.Model}" : null,
                ReservationFee = vehicle is not null ? vehicle.ReservationFee.ToString() : null,
                MonthlyCost = vehicle is not null ? vehicle.MonthlyCost.ToString() : null,
                TotalCost = vehicle is not null ? vehicle.ReservationFee.ToString() : null
            };
            return View(reserve);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CompleteReservation(CompleteReservationViewModel completeReservation)
        {
            _logger.LogInformation("Completing Reservation ...");
            try
            {
                var user = await _manager.GetUserAsync(User);

                if (user != null)
                {
                    var age = _subscriberRepository.CalculateAge(completeReservation.DateOfBirth!.Value);

                    if (age < 16)
                    {
                        TempData["error"] = "You must be at least 16 years old to complete the reservation.";

                        // Reload vehicle and user information for the view model
                        var currentSubscriber = await _subscriberRepository.GetSubscriberByUserId(user.Id);
                        var currentSubscription = currentSubscriber != null ? await _subscriptionRepository.GetSubscriptionBySubcriber(currentSubscriber.Id) : null;
                        var reservedVehicle = currentSubscription != null ? await _vehicleRepository.GetVehicleById(currentSubscription.VehicleId!) : null;

                        completeReservation.FirstName = user.FirstName;
                        completeReservation.VehicleName = reservedVehicle != null ? $"{reservedVehicle.Make} {reservedVehicle.Model}" : null;
                        completeReservation.ReservationFee = reservedVehicle?.ReservationFee.ToString();
                        completeReservation.MonthlyCost = reservedVehicle?.MonthlyCost.ToString();
                        completeReservation.TotalCost = reservedVehicle?.ReservationFee.ToString();
                        return View(completeReservation);
                    }

                    var subscriber = await _subscriberRepository.GetSubscriberByUserId(user.Id);

                    if (subscriber == null)
                    {
                        _logger.LogError("User is not a subscriber");
                        TempData["error"] = "Error, User is not a subscriber";

                        return RedirectToAction("Login", "Account");
                    }

                    subscriber.DateOfBirth = completeReservation.DateOfBirth; //kind is unspecified

                    var updatedSubscriber = await _subscriberRepository.UpdateSubscriberAsync(subscriber);

                    var getExistingSubscription = await _subscriptionRepository.GetSubscriptionBySubcriber(updatedSubscriber.Id);
                    var vehicle = getExistingSubscription is not null ? await _vehicleRepository.GetVehicleById(getExistingSubscription.VehicleId!) : null;

                    var licenseFront = await _photoService.AddPhotoAsync(completeReservation.DriversLicenseFront!);
                    var licenseBack = await _photoService.AddPhotoAsync(completeReservation.DriversLicenseBack!);

                    getExistingSubscription!.DLUrlFront = licenseFront.Url.ToString();
                    getExistingSubscription.DLUrlBack = licenseBack.Url.ToString();
                    getExistingSubscription.TermStart = DateTime.UtcNow;



                    getExistingSubscription.TermEnd = completeReservation.SubscriptionDuration == SubscriptionDuration.SDSD_3Months ? DateTime.UtcNow.AddMonths(3)
                                                        : completeReservation.SubscriptionDuration == SubscriptionDuration.SDSD_6Months ? DateTime.UtcNow.AddMonths(6)
                                                        : DateTime.UtcNow.AddMonths(9);
                    getExistingSubscription.Duration = completeReservation.SubscriptionDuration == SubscriptionDuration.SDSD_3Months ? "3 months"
                                                        : completeReservation.SubscriptionDuration == SubscriptionDuration.SDSD_6Months ? "6 months"
                                                        : "9 months";
                    getExistingSubscription.PickUpDate = completeReservation.PickupDate == VehiclePickUpDate.VPSD_ASAP ? DateTime.UtcNow.AddDays(27)
                                                            : completeReservation.PickupDate == VehiclePickUpDate.VPSD_1 ? DateTime.UtcNow.AddMonths(1)
                                                            : DateTime.UtcNow.AddMonths(2);

                    getExistingSubscription.SubscriptionStatus = SubscriptionStatus.Awaiting;

                    var updatedSubscription = await _subscriptionRepository.UpdateSubscription(getExistingSubscription);

                    if (completeReservation.AdditionalDrivers == true && completeReservation.CoSubscribers != null)
                    {
                        foreach (var coSubscriberModel in completeReservation.CoSubscribers)
                        {
                            var checkUserExists = await _manager.FindByEmailAsync(coSubscriberModel.CoSubEmail!);

                            if (checkUserExists == null)
                            {
                                var newUser = new AppUser
                                {
                                    FirstName = coSubscriberModel.CoSubFirstName,
                                    LastName = coSubscriberModel.CoSubLastName,
                                    Email = coSubscriberModel.CoSubEmail,
                                    PhoneNumber = coSubscriberModel.CoSubPhone,
                                    UserName = coSubscriberModel.CoSubEmail,
                                    Type = "Co_Subscriber"
                                };

                                var addedUser = await _manager.CreateAsync(newUser);
                                await _manager.AddToRoleAsync(newUser, "Co_Subscriber");

                                if (addedUser.Succeeded)
                                {
                                    var addSubscriber = new Co_SubscriberInvitee
                                    {
                                        FirstName = newUser.FirstName!,
                                        LastName = newUser.LastName!,
                                        Email = newUser.Email!,
                                        SubscriberId = subscriber.Id,
                                        Subscriber = subscriber,
                                        Vehicle = vehicle,
                                        VehicleId = vehicle is not null ? vehicle.Id : null,
                                        AppUserId = newUser.Id
                                    };

                                    await _coSubscriberRepository.AddInvitee(addSubscriber);
                                }
                            }
                            else
                            {
                                var coSubscriberCheck = await _coSubscriberRepository.GetCoSubscriberByUserId(checkUserExists.Id);
                                if (coSubscriberCheck != null)
                                {
                                    coSubscriberCheck.SubscriberId = updatedSubscriber.Id;
                                    coSubscriberCheck.Vehicle = vehicle;
                                    coSubscriberCheck.VehicleId = vehicle is not null ? vehicle.Id : null;
                                    coSubscriberCheck.FirstName = checkUserExists.FirstName!;
                                    coSubscriberCheck.LastName = checkUserExists.LastName!;
                                    coSubscriberCheck.Email = checkUserExists.Email!;

                                    await _coSubscriberRepository.UpdateInvitee(coSubscriberCheck);
                                }
                                else
                                {
                                    var addCoSubscriber = new Co_SubscriberInvitee
                                    {
                                        FirstName = checkUserExists.FirstName!,
                                        LastName = checkUserExists.LastName!,
                                        Email = checkUserExists.Email!,
                                        SubscriberId = subscriber.Id,
                                        Subscriber = subscriber,
                                        Vehicle = vehicle,
                                        VehicleId = vehicle is not null ? vehicle.Id : null,
                                        AppUserId = checkUserExists.Id
                                    };

                                    await _coSubscriberRepository.AddInvitee(addCoSubscriber);
                                }
                            }
                        }
                    }

                    var payViewModel = new PayViewModel
                    {
                        Name = $"{subscriber.FirstName} {subscriber.LastName}",
                        Email = subscriber.Email,
                        VehicleId = vehicle!.Id,
                        SubscriberId = subscriber.Id,
                        OperatorId = vehicle.OperatorId,
                        Amount = vehicle.ReservationFee
                    };

                    vehicle.VehicleStatus = VehicleStatus.Reserved.ToString();
                    _vehicleRepository.UpdateVehicle(vehicle);

                    _logger.LogInformation("Proceed to Payment");
                    return RedirectToAction("Index", "Pay", payViewModel);
                }

                _logger.LogError("User may not exist");
                TempData["error"] = "User may not exist";
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "Something went wrong when reserving car");
                TempData["error"] = "Something went wrong when reserving car. Contact Support!";
                return View(completeReservation);
            }
        }

        [HttpGet]
        public IActionResult PostReservation()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var user = await _manager.GetUserAsync(User);
            var subscriber = _subscriberRepository.GetSubscriberById(user!.Id);

            if (subscriber == null)
            {
                return NotFound("Subscriber not found.");
            }

            var viewModel = new SubscriberProfileViewModel
            {
                Subscriber = subscriber,
                Subscriptions = _subscriberRepository.GetSubscriptionsBySubscriberId(subscriber.Id)
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Profile(string subscriptionId)
        {
            var result = _subscriptionRepository.CancelSubscription(subscriptionId);

            if (!result)
            {
                TempData["Failure"] = "Unable to cancel subscription";

                return RedirectToAction("Profile");
            }
            TempData["Success"] = "Cancel Subscription Successful";

            return RedirectToAction("Profile");
        }

        [HttpGet]
        public async Task<IActionResult> SubscriptionHistory()
        {
            var user = await _manager.GetUserAsync(User);
            var subscriber = _subscriberRepository.GetSubscriberById(user!.Id);

            if (subscriber == null)
            {
                return NotFound("Subscriber not found.");
            }

            var viewModel = new SubscriberProfileViewModel
            {
                Subscriber = subscriber,
                Subscriptions = _subscriberRepository.GetSubscriptionsBySubscriberId(subscriber.Id)
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> ActiveSubscription()
        {
            var user = await _manager.GetUserAsync(User);
            var subscriber = _subscriberRepository.GetSubscriberById(user!.Id);

            if (subscriber == null)
            {
                return NotFound("Subscriber not found.");
            }

            var viewModel = new SubscriberProfileViewModel
            {
                Subscriber = subscriber,
                Subscriptions = _subscriberRepository.GetSubscriptionsBySubscriberId(subscriber.Id)
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ActiveSubscription(string subscriptionId)
        {
            var result = _subscriptionRepository.CancelSubscription(subscriptionId);

            if (!result)
            {
                TempData["error"] = "Unable to cancel subscription.";
                return RedirectToAction("ActiveSubscription");
            }

            TempData["success"] = "Subscription cancelled successfully.";
            return RedirectToAction("ActiveSubscription");
        }
    }
}
