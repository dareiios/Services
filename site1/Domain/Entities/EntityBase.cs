using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

//этот базовый класс не будет участвовать в доменной моедли
namespace site1.Domain.Entities
{
    public abstract class EntityBase
    {
        //конструктор защищенный-при создании каждого обьекта DateAdded будет равна настоящему временми(чтобы при создании обьекта класса уже
        //было какое то значение)
        protected EntityBase() => DateAdded = DateTime.UtcNow;


        //required-обязательный
        [Required]
        public Guid Id { get; set; }

        //display-то что будет отображаться на стр вмето названия свойств

        [Display(Name ="Название(заголовок)")]
        public virtual string Title { get; set; }

        [Display(Name = "краткое описание")]
        public virtual string Subtitle { get; set; }

        [Display(Name = "Полное описание")]
        public virtual string Text { get; set; }

        [Display(Name = "Титульная картинка")]
        public virtual string TitleImagePath { get; set; }

        [Display(Name = "SEO метатег Title")]
        public virtual string MetaTitle { get; set; }

        [Display(Name = "SEO метатег Description")]
        public virtual string MetaDescription { get; set; }

        [Display(Name = "SEO метатег KeyWords")]
        public virtual string MetaKeyWords { get; set; }

        //указывает что при заполнениее будет заполняться как время, а не просто текст
        [DataType(DataType.Time)]
        public DateTime DateAdded { get; set; }
    }
}
