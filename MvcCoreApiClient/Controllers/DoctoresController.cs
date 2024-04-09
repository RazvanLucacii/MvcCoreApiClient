using Microsoft.AspNetCore.Mvc;
using MvcCoreApiClient.Services;
using System.Net.Http.Headers;

namespace MvcCoreApiClient.Controllers
{
    public class DoctoresController : Controller
    { 

        public IActionResult Index()
        {
            return View();
        }
    }
}
