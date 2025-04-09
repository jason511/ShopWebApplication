using Microsoft.AspNetCore.Mvc;

namespace ShopWebApplication.Controllers;

public class OrderController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}