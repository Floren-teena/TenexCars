using Microsoft.AspNetCore.Mvc;

namespace TenexCars.Controllers.Subscriber_Controller
{
    public class SubscriberController : Controller
    {
        public SubscriberController() 
        {

        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
