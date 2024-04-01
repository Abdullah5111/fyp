using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Jobverse.Models;

namespace Jobverse.Controllers
{
    public class NotificationController : Controller
    {
        public IActionResult Notification()
        {
            return View();
        }
        public IActionResult JobApplication()
        {
            return View();
        }
    }

}
