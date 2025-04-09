using Microsoft.AspNetCore.Mvc;
using ShopWebApplication.Models;

namespace ShopWebApplication.Controllers;

public class MemberRegisterController : Controller
{
    private readonly ApplicationDbContext _context;

    public MemberRegisterController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Index(MemberRegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            // 檢查用戶名是否已存在
            if (_context.Members.Any(m => m.UserId == model.UserId))
            {
                ModelState.AddModelError("UserId", "此用戶名已被註冊");
                return View(model);
            }

            // 檢查電子郵件是否已存在
            if (_context.Members.Any(m => m.Email == model.Email))
            {
                ModelState.AddModelError("Email", "此電子郵件已被註冊");
                return View(model);
            }

            // 確認密碼是否一致
            if (model.Password != model.ConfirmPassword)
            {
                ModelState.AddModelError("ConfirmPassword", "密碼不一致");
                return View(model);
            }

            // 密碼加密（使用 BCrypt）
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);

            // 創建新用戶
            var newUser = new Member
            {
                UserId = model.UserId,
                Email = model.Email,
                Password = hashedPassword,
                Name = model.Name,
            };

            _context.Members.Add(newUser);
            _context.SaveChanges();

            // 註冊成功後，跳轉到登入頁面
            return RedirectToAction("Index", "MemberLogin");
        }

        // 表單資料無效，重新顯示註冊頁面
        return View(model);
    }
}