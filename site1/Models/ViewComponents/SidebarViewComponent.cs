using Microsoft.AspNetCore.Mvc;
using site1.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace site1.Models.ViewComponents
{
    public class SidebarViewComponent : ViewComponent
    {
        private readonly DataManager dataManager;

        public SidebarViewComponent( DataManager dm)
        {
            dataManager = dm;
        }

        public Task<IViewComponentResult> InvokeAsync()
        {
            //task-возвр тип в асинх методе
            return Task.FromResult((IViewComponentResult)View("Default", dataManager.ServiceItems.GetServiceItems()));
        }
    }
}
