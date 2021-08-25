using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Core.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // �˷���������ʱ���á�ʹ�ô˷�����������ӵ�����.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
        }

        // �˷���������ʱ���á�ʹ�ô˷�������HTTP����ܵ���
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // Ĭ��HSTSֵΪ30�졣��������ҪΪ�����������Ĵ�����.(����HTTPSʱ����) 
                //app.UseHsts();
            }
            //// ʹ��Https��Э��TLS
            app.UseHttpsRedirection();
            //// ���ʾ�̬�ļ�
            app.UseStaticFiles();
            //// ע��·��
            app.UseRouting();

            //// �����֤ ������ASP.NET Core 3.0�У�app.UseMvc��app.UseRouting��app.UseEndpoints���������app.UseAuthentication��app.UseAuthorization��Ҫ����app.UseRouting��app.UseCors֮�󣬲�����app.UseEndpoints֮ǰ
            app.UseAuthorization();
            //// �˵�
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
