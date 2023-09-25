using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using site1.Domain;
using site1.Domain.Repositories;
using site1.Domain.Repositories.Abstract;
using site1.Domain.Repositories.EntityFramework;
using site1.Service;

namespace site1
{
    //настраивает службы и контейнер запросов приложения
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration) => Configuration = configuration;

        //для настройки, добавления служб
        public void ConfigureServices(IServiceCollection services)
        {
            //подкл config из appseting.json. связываем проджект(из файла appsetings) и конфиг.cs
            Configuration.Bind("Project", new Config());//1-строка из файла конфиг. 2-класс конфиг

            //подкл нужный функционал в качестве сервисов. внедрение зависимостей. связываем интерфейс с его реализацией
            //если захотим поменять систему базы даннхы, то просто меняем тут значение вместо EF...
            //transient-в рамках одного запроса может быть создано сколько угодно объектов репозиториев. при каждом обращении
            //к датаменеджеру будет создан объект и не один
            services.AddTransient<ITextFieldsRepository, EFTextFieldsRepository>();
            services.AddTransient<IServiceItemsRepository, EFServiceItemsRepository>();
            services.AddTransient<DataManager>();

            //подключение контекста к бд через AddDbContext(созданный контекст) и через свойства конфиг указываем строку подключения
            services.AddDbContext<AppDbContext>(x => x.UseSqlServer(Config.ConnectionString));

            //настраиваем identity систему, жесткие требования для подтверждения пользователяя
            //Метод AddIdentity() позволяет установить некоторую начальную конфигурацию.
            //Здесь мы указываем тип пользователя и тип роли, которые будут использоваться системой Identity.
            services.AddIdentity<IdentityUser, IdentityRole>(opts =>
            {
                opts.User.RequireUniqueEmail = true;
                opts.Password.RequiredLength = 6;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireDigit = false;
                //Метод AddEntityFrameworkStores() устанавливает тип хранилища, которое будет применяться в Identity для хранения данных.
                //В качестве типа хранилища здесь указывается класс контекста данных.
            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            //настраиваем authentication cookie
            // аутентификация с помощью куки
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "myCompanyAuth";
                options.Cookie.HttpOnly = true; //недоступно для клиента
                options.LoginPath = "/account/login"; //куда отправлять пользователя, чтобы он залогинился. контролер чтобы польз вввел данные и был доступ к роли адм
                options.AccessDeniedPath = "/account/accessdenied";
                options.SlidingExpiration = true;
            });

            //настройка политики авторизации для Admin area
            //политика-дополнительные условия для авторизации...
            services.AddAuthorization(x =>
            {
                //добавление политики с названием админарея и пользователь должен быть под ролью админ
                x.AddPolicy("AdminArea", policy => { policy.RequireRole("admin"); });
            });


            //добавление поддержки контр и представлений(MVC)
            services.AddControllersWithViews(x =>
            {
                //примени к этой области(admin) эту политику(adminarea). выше написано что входит в политику(роль админ)
                x.Conventions.Add(new AdminAreaAuthorization("Admin", "AdminArea")); //1-area, 2-policy. adminarea-69строка
            })
                //выставляем совместимость с асп нет кор 3 версии
                .SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Latest).AddSessionStateTempDataProvider();
        }



        //для создания конвейера обработки запросов. устанавливает, как приложение будет обрабатывать запрос.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //middlewere-функционал. надо подключать в правильном порядке(т.е все подключения регистрации, стат файлы и др)

            //если находимся в процессе сброки кода(дебаг), нужно знать какие ошибки возникают(подробную инфу)
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //подкл поддержки стат файлов (css,js...)
            app.UseStaticFiles();

            //подкл маршрутизации
            app.UseRouting();

            //подключение аутентификации и авторизации
            app.UseCookiePolicy();
            app.UseAuthentication();//чтобы использовать Identity
            app.UseAuthorization();


            //исп маршрутищации(ендпоинты); устанавливаем адреса, которые будут обрабатываться
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("admin", "{area:exists}/{controller=Home}/{action=Index}/{id?}");//такой сегмент должен существовать
                //регистрируем наш маршрут, назв-дефолт. когда ничего не передается кроме url основного сайта(site.com), то исп хоум и индекс
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}


//миграция-позволяет мониторить отслеживать бд. т.е если в услугах появится новое словйтсво(цена), то исп миграции
//не надо вручную обращат ься к бд и создавать там колонку. мы просто создадим миграцию и изменения автоматически добавятся в бд