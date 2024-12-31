using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TenexCars.DataAccess.Models;
using TenexCars.DataAccess.Repositories.Interfaces;
using TenexCars.Interfaces;
using TenexCars.Models;

namespace TenexCars.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signManager;
        private readonly ISubscriberRepository _subscriberRepository;
        private readonly IEmailService _emailService;

        public HomeController(ILogger<HomeController> logger, IVehicleRepository vehicleRepository, UserManager<AppUser> userManager, SignInManager<AppUser> signManager, ISubscriberRepository subscriberRepository, IEmailService emailService)
        {
            _logger = logger;
            _vehicleRepository = vehicleRepository;
            _userManager = userManager;
            _subscriberRepository = subscriberRepository;
            _emailService = emailService;
            _signManager = signManager;
        }

        public async Task<IActionResult> Index()
        {
            var topUniqueVehicles = await _vehicleRepository.GetTopUniqueVehiclesAsync();
            return View(topUniqueVehicles);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
