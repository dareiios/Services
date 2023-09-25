using Microsoft.EntityFrameworkCore;
using site1.Domain.Entities;
using site1.Domain.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace site1.Domain.Repositories.EntityFramework
{
    public class EFServiceItemsRepository: IServiceItemsRepository
    {
        private readonly AppDbContext context;

        public EFServiceItemsRepository(AppDbContext context)
        {
            this.context = context;
        }

       
        public void DeleteServiceItem(Guid id)
        {
            context.ServiceItems.Remove(new ServiceItem() { Id=id});
            context.SaveChanges();
        }

        public ServiceItem GetServiceItemById(Guid id)
        {
           return context.ServiceItems.FirstOrDefault(x => x.Id == id);
        }

        public IQueryable<ServiceItem> GetServiceItems()
        {
            return context.ServiceItems;
        }

        public void SaveServiceItem(ServiceItem entity)
        {
            if (entity.Id == default)
            {
                context.Entry(entity).State = EntityState.Added;
            }
            else context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
