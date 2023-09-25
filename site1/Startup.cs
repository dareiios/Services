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
    //����������� ������ � ��������� �������� ����������
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration) => Configuration = configuration;

        //��� ���������, ���������� �����
        public void ConfigureServices(IServiceCollection services)
        {
            //����� config �� appseting.json. ��������� ��������(�� ����� appsetings) � ������.cs
            Configuration.Bind("Project", new Config());//1-������ �� ����� ������. 2-����� ������

            //����� ������ ���������� � �������� ��������. ��������� ������������. ��������� ��������� � ��� �����������
            //���� ������� �������� ������� ���� ������, �� ������ ������ ��� �������� ������ EF...
            //transient-� ������ ������ ������� ����� ���� ������� ������� ������ �������� ������������. ��� ������ ���������
            //� ������������� ����� ������ ������ � �� ����
            services.AddTransient<ITextFieldsRepository, EFTextFieldsRepository>();
            services.AddTransient<IServiceItemsRepository, EFServiceItemsRepository>();
            services.AddTransient<DataManager>();

            //����������� ��������� � �� ����� AddDbContext(��������� ��������) � ����� �������� ������ ��������� ������ �����������
            services.AddDbContext<AppDbContext>(x => x.UseSqlServer(Config.ConnectionString));

            //����������� identity �������, ������� ���������� ��� ������������� �������������
            //����� AddIdentity() ��������� ���������� ��������� ��������� ������������.
            //����� �� ��������� ��� ������������ � ��� ����, ������� ����� �������������� �������� Identity.
            services.AddIdentity<IdentityUser, IdentityRole>(opts =>
            {
                opts.User.RequireUniqueEmail = true;
                opts.Password.RequiredLength = 6;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireDigit = false;
                //����� AddEntityFrameworkStores() ������������� ��� ���������, ������� ����� ����������� � Identity ��� �������� ������.
                //� �������� ���� ��������� ����� ����������� ����� ��������� ������.
            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            //����������� authentication cookie
            // �������������� � ������� ����
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "myCompanyAuth";
                options.Cookie.HttpOnly = true; //���������� ��� �������
                options.LoginPath = "/account/login"; //���� ���������� ������������, ����� �� �����������. ��������� ����� ����� ����� ������ � ��� ������ � ���� ���
                options.AccessDeniedPath = "/account/accessdenied";
                options.SlidingExpiration = true;
            });

            //��������� �������� ����������� ��� Admin area
            //��������-�������������� ������� ��� �����������...
            services.AddAuthorization(x =>
            {
                //���������� �������� � ��������� ��������� � ������������ ������ ���� ��� ����� �����
                x.AddPolicy("AdminArea", policy => { policy.RequireRole("admin"); });
            });


            //���������� ��������� ����� � �������������(MVC)
            services.AddControllersWithViews(x =>
            {
                //������� � ���� �������(admin) ��� ��������(adminarea). ���� �������� ��� ������ � ��������(���� �����)
                x.Conventions.Add(new AdminAreaAuthorization("Admin", "AdminArea")); //1-area, 2-policy. adminarea-69������
            })
                //���������� ������������� � ��� ��� ��� 3 ������
                .SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Latest).AddSessionStateTempDataProvider();
        }



        //��� �������� ��������� ��������� ��������. �������������, ��� ���������� ����� ������������ ������.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //middlewere-����������. ���� ���������� � ���������� �������(�.� ��� ����������� �����������, ���� ����� � ��)

            //���� ��������� � �������� ������ ����(�����), ����� ����� ����� ������ ���������(��������� ����)
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //����� ��������� ���� ������ (css,js...)
            app.UseStaticFiles();

            //����� �������������
            app.UseRouting();

            //����������� �������������� � �����������
            app.UseCookiePolicy();
            app.UseAuthentication();//����� ������������ Identity
            app.UseAuthorization();


            //��� �������������(���������); ������������� ������, ������� ����� ��������������
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("admin", "{area:exists}/{controller=Home}/{action=Index}/{id?}");//����� ������� ������ ������������
                //������������ ��� �������, ����-������. ����� ������ �� ���������� ����� url ��������� �����(site.com), �� ��� ���� � ������
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}


//��������-��������� ���������� ����������� ��. �.� ���� � ������� �������� ����� ���������(����), �� ��� ��������
//�� ���� ������� ������� ��� � �� � ��������� ��� �������. �� ������ �������� �������� � ��������� ������������� ��������� � ��