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

        // 此方法由运行时调用。使用此方法将服务添加到容器.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
        }

        // 此方法由运行时调用。使用此方法配置HTTP请求管道。
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // 默认HSTS值为30天。您可能需要为生产场景更改此设置.(开启HTTPS时启用) 
                //app.UseHsts();
            }
            //// 使用Https的协议TLS
            app.UseHttpsRedirection();
            //// 访问静态文件
            app.UseStaticFiles();
            //// 注册路由
            app.UseRouting();

            //// 身份认证 由于在ASP.NET Core 3.0中，app.UseMvc被app.UseRouting和app.UseEndpoints替代，所以app.UseAuthentication和app.UseAuthorization，要放在app.UseRouting、app.UseCors之后，并且在app.UseEndpoints之前
            app.UseAuthorization();
            //// 端点
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
