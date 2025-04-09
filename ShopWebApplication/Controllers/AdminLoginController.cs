using Microsoft.AspNetCore.Mvc;
using ShopWebApplication.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace ShopWebApplication.Controllers;

public class AdminLoginController : Controller
{
    private readonly ApplicationDbContext _context;

    public AdminLoginController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index(string? returnUrl = null)
    {
        ViewBag.ReturnUrl = returnUrl;
        return View();
    }
    [HttpPost]
    public IActionResult Index(AdminLoginViewModel model, string? returnUrl = null) // ✅【修改】接收 returnUrl
    {
        if (ModelState.IsValid)
        {
            var user = _context.Admins.FirstOrDefault(a => a.UserId == model.Username || a.Email == model.Username);

            if (user != null && VerifyPassword(model.Password, user.Password))
            {
                // 登入成功，設定認證 Cookie
                if (user.Name != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()), // 🔹 加入 UserId
                        new Claim(ClaimTypes.Name, user.Name),
                        new Claim(ClaimTypes.Email, user.Email)
                    };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    // 設定登入時的 Cookie
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                }

                // ✅【修改】登入成功後，導向 ReturnUrl，否則回首頁
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                return RedirectToAction("Index", "Home"); // 登入成功後跳轉到主頁
            }
            else
            {
                ModelState.AddModelError("", "用戶名或密碼錯誤");
            }
        }
        ViewBag.ReturnUrl = returnUrl; // ✅【新增】確保登入失敗時 returnUrl 仍然存在
        return View(model);
    }

    // 密碼驗證方法
    private bool VerifyPassword(string password, string storedHash)
    {
        // 使用 BCrypt 驗證密碼
        return BCrypt.Net.BCrypt.Verify(password, storedHash);
    }
    // 登出處理
    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        // 清除認證 Cookie，讓用戶登出
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        // 登出後重定向到登入頁面
        return RedirectToAction("Index", "Home");
    }
}