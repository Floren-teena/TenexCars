using Microsoft.AspNetCore.Mvc;
using TenexCars.DataAccess.Repositories.Interfaces;
using TenexCars.Models.ViewModels;

namespace TenexCars.Controllers.Subscriber_Controller
{
    public class SubscriberController : Controller
    {
        private readonly IVehicleRepository _vehicleRepository;

        public SubscriberController(IVehicleRepository vehicleRepository) 
        {
            _vehicleRepository = vehicleRepository;
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
    }
}
