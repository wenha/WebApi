using System.Web.Http;
using WebActivatorEx;
using Admin.WebApi;
using Swashbuckle.Application;
using System;
using System.Web.Http.Description;
using Admin.WebApi.App_Start.Swagger;
using System.IO;
using System.Linq;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace Admin.WebApi
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            ConfigureSwaggerUi();
        }

        private static void ConfigureSwaggerUi()
        {
            //访问地址: http://localhost:11008//apis/index
            GlobalConfiguration.Configuration.EnableSwagger(c =>
            {
                c.MultipleApiVersions(ResolveAreasSupportByRouteConstraint, (vc) =>
                {
                    vc.Version("v1", "前台接口 API", true).Description("前台接口");

                    vc.Version("Admin", "后台接口 API").Description("后台接口");
                });

                //增加token至请求头部
                c.ApiKey("Authorization").Description("token 唯一值").In("header").Name("token");
                
                //加载运行目录下的所有注释文档
                var pathXml = string.Format("{0}/bin/", AppDomain.CurrentDomain.BaseDirectory);
                var fileXml = Directory.GetFiles(pathXml).Where(o => Path.GetExtension(o).ToLower() == ".xml");
                foreach (var file in fileXml)
                {
                    c.IncludeXmlComments(file);
                }

                //增加Swagger区域选择
                c.DocumentFilter<SwaggerAreasSupportDocumentFilter>();

                //增加上传文件过滤操作
                //c.OperationFilter<AddUploadOperationFilter>();

                //显示开发者信息
                c.ShowDeveloperInfo();
            })
                .EnableSwaggerUi("apis/{*assetPath}", c =>
                {
                    c.InjectStylesheet(typeof(SwaggerConfig).Assembly, "Admin.WebApi.Content.Swagger.theme-flattop.css");
                    c.InjectJavaScript(typeof(SwaggerConfig).Assembly, "Admin.WebApi.Content.Swagger.translator.js");

                    c.EnableApiKeySupport("Authorization", "header");
                });
        }

        private static bool ResolveAreasSupportByRouteConstraint(ApiDescription apiDescription, string targetApiVersion)
        {
            if (targetApiVersion == "v1")
            {
                return apiDescription.Route.RouteTemplate.StartsWith("api/{controller}");
            }
            var routeTemplateStart = "api/" + targetApiVersion;
            return apiDescription.Route.RouteTemplate.StartsWith(routeTemplateStart);
        }
    }
}
