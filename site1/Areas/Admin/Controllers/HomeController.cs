using Microsoft.AspNetCore.Mvc;
using site1.Domain.Repositories;

namespace site1.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly DataManager dataManager;
        public HomeController(DataManager dm)
        {
            dataManager = dm;
        }

        public IActionResult Index()
        {
            return View(dataManager.ServiceItems.GetServiceItems());
        }
    }
}