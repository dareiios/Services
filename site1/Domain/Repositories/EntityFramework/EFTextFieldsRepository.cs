using Microsoft.EntityFrameworkCore;
using site1.Domain.Entities;
using site1.Domain.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace site1.Domain.Repositories.EntityFramework
{
    public class EFTextFieldsRepository: ITextFieldsRepository
    {
        private readonly AppDbContext context;

        //через внедрение зависимостей в конструкторе связываем обьект с параметром=
        public EFTextFieldsRepository(AppDbContext context)
        {
            this.context = context;
        }

        public IQueryable<TextField> GetTextFields()//select * from textfields -но не запустила
        {
            return context.TextFields;
        }

        public TextField GetTextFieldById(Guid id)
        {
            return context.TextFields.FirstOrDefault(x => x.Id == id);
        }

        public TextField GetTextFieldByCodeWord(string codeWord)
        {
            return context.TextFields.FirstOrDefault(x => x.CodeWord == codeWord);
        }

        public void SaveTextField(TextField entity)
        {
            //если айди=по умолчанию=>это новая запись и айди нет=>это новая запись(добавить)
            if (entity.Id == default)
                //вручную создать экземпляр entity, присвоить ему нужные свойства
                //и вручную отредактировать состояние объекта в системе отслеживания изменений
                context.Entry(entity).State = EntityState.Added;
            else //айди уже есть=>обьект изменен
                context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void DeleteTextField(Guid id)
        {
            //создаем новый фековый обьект у котрого айди совпадает с переданным
            context.TextFields.Remove(new TextField() { Id = id });
            context.SaveChanges();
        }
    }
}
