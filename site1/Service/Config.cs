using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace site1.Service
{
    public class Config
    {
        public static string ConnectionString { get; set; }
        public static string CompanyName { get; set; }
        public static string CompanyPhone { get; set; }
        public static string CompanyPhoneShort { get; set; }
        public static string CompanyEmail { get; set; }

    }
}

//Доменные модели это такие классы которые будут проицироваться на соответствующие таблицы в бд