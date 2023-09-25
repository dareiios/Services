using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace site1.Service
{
    public class AdminAreaAuthorization: IControllerModelConvention
    {
        private readonly string area;
        private readonly string policy;

        public AdminAreaAuthorization(string area, string policy)
        {
            this.area = area;
            this.policy = policy;
        }

        public void Apply(ControllerModel controller)
        {
            //для контролера проверяем атрибуты. если присутсвтует атрибут арея, то добавляем в филтр для контроллера в авторайсфилтр
            //т е отправляем пользоввателя на авторизацию
            if(controller.Attributes.Any(x=>
                    x is AreaAttribute && (x as AreaAttribute).RouteValue.Equals(area, StringComparison.OrdinalIgnoreCase)) 
                || controller.RouteValues.Any(r=>
                    r.Key.Equals("area", StringComparison.OrdinalIgnoreCase) && r.Value.Equals(area, StringComparison.OrdinalIgnoreCase)))
            {
                controller.Filters.Add(new AuthorizeFilter(policy));
            }

        }
    }
}
