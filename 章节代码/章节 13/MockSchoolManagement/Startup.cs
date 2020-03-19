using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MockSchoolManagement.DataRepositories;

namespace MockSchoolManagement
{
    public class Startup
    {
        private IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
           // services.AddMvc(a => a.EnableEndpointRouting = false);
            services.AddControllersWithViews(a => a.EnableEndpointRouting = false).AddXmlSerializerFormatters();
            services.AddSingleton<IStudentRepository, MockStudentRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //���������Development serve Developer Exception Page
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //else�ṩ����Ӧ�ó���֧�ֵ��û��Ѻô���ҳ����ϵ��Ϣ
            else if (env.IsStaging() || env.IsProduction() || env.IsEnvironment("UAT"))
            {
                app.UseExceptionHandler("/Error");
            }
            //ʹ�ô���̬�ļ�֧�ֵ��м��������ʹ�ô����ն��м��
            app.UseStaticFiles();

            app.UseMvcWithDefaultRoute();

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }
    }
}