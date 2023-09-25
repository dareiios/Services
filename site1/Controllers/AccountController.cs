using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using site1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace site1.Controllers
{
    [Authorize] //В этом случае доступ к методам имеют только те пользователи, которые залогинились в приложении. 
    public class AccountController : Controller
    {
        //для управления пользователями используется не контекст данных, а специальный класс - UserManager
        private readonly UserManager<IdentityUser> userManager;
        //позволяет аутентифицировать пользователя и устанавливать или удалять его куки
        private readonly SignInManager<IdentityUser> signInManager;

        //через внедрение зависимостей передаем данные чтобы оперировать пользователями в бд
        public AccountController(UserManager<IdentityUser> userMgr, SignInManager<IdentityUser> signinMgr)
        {
            userManager = userMgr;
            signInManager = signinMgr;
        }

        //Атрибут AllowAnonymous позволяет открыть доступ для анонимных пользователей.
        //Обычно он применяется к методам контроллера, который уже защищен атрибутом Authorize.
        [AllowAnonymous] //нужно быть анонимным пользователем, чтобы зарегаться на сайте
        // Login принимают строковый аргумент returnUrl. Когда пользователь запрашивает URL-адрес с ограниченным доступом,
        // он перенаправляется по адресу /Account/Login со строкой запроса, содержащей адрес страницы с ограниченным доступом.
        //С помощью параметра ReturnUrl мы сможем перенаправить пользователя, успешно прошедшего аутентификацию, на страницу, с которой он пришел. 
        public IActionResult Login(string returnUrl)
        { 
            ViewBag.returnUrl = returnUrl;
            return View(new LoginViewModel());
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = await userManager.FindByNameAsync(model.UserName);//найти пользователя с теми данными что ввел в форму
                if(user != null)
                {
                    await signInManager.SignOutAsync();//выход
                    //патаемся войти по паролю с формы
                    //Этот метод берет роль из бд и смотрит роль админ или нет
                    Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                    if(result.Succeeded)
                    {
                        return Redirect(returnUrl ?? "/");//возвращаем пользователя на место откуда от пытался зайти если не было такой стр, то идет на главную
                    }
                }
                ModelState.AddModelError(nameof(LoginViewModel.UserName), "Неверный логин или пароль");
            }
            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
