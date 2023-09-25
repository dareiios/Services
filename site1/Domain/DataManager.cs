using site1.Domain.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace site1.Domain.Repositories
{
    //обслуживающмй класс-помощник, в к централизована управление репозиториями. вместо того чтобы отделбно
    //передавать каждый репозиторий для упр текстом и услугами, мы будем передавать в контроллер датаменеджер и обращаться к его свойствам
    public class DataManager
    {
        public ITextFieldsRepository TextFields { get; set; }
        public IServiceItemsRepository ServiceItems { get; set; }
        public DataManager(ITextFieldsRepository textFieldsRepository, IServiceItemsRepository serviceItemsRepository)
        {
            TextFields = textFieldsRepository;
            ServiceItems = serviceItemsRepository;
        }
    }
}
