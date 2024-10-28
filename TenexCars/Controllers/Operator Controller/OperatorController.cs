using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace TenexCars.Controllers.Operator_Controller
{
    public class OperatorController : Controller
    {
        public OperatorController() 
        {

        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CreateVehicle()
        {
            /*var user = await _userManager.GetUserAsync(User);

            var operatorUser = user is not null ? await _operatorRepository.GetOperatorByUserId(user.Id) : null;

            var viewModel = new CreateVehicleViewModel
            {
                OperatorId = operatorUser is not null ? operatorUser.Id : null,
                OperatorLogoUrl = operatorUser is not null ? operatorUser.CompanyLogo : null,
                CarDealerName = operatorUser is not null ? operatorUser.BusinessName : null

            };*/

            return View();
        }
    }
}
