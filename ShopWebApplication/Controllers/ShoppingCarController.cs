using Microsoft.AspNetCore.Mvc;

namespace ShopWebApplication.Controllers;

public class ShoppingCarController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}