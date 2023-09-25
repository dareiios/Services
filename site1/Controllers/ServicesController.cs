using Microsoft.AspNetCore.Mvc;
using site1.Domain.Repositories;
using System;

namespace site1.Controllers
{
    public class ServicesController : Controller
    {
        private readonly DataManager dataManager;

        public ServicesController(DataManager dataManager)
        {
            this.dataManager = dataManager;
        }

        public IActionResult Index(Guid id)
        {
            if (id != default) //елси передали айди конкретной услуги. в индекс вью-стр 18(а)
            {
                return View("Show", dataManager.ServiceItems.GetServiceItemById(id));
            }
            //не передаем айди, хотим посмотреть все услуги, ане какую то конкрутную. то есть наши услуги
            ViewBag.TextField = dataManager.TextFields.GetTextFieldByCodeWord("PageServices");//положили во вьюбэг pageservices. а в представлении вытащим
            return View(dataManager.ServiceItems.GetServiceItems());//список всех услуг
        }

    }
}
