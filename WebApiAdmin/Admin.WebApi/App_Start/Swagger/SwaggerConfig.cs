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
            //���ʵ�ַ: http://localhost:11008//apis/index
            GlobalConfiguration.Configuration.EnableSwagger(c =>
            {
                c.MultipleApiVersions(ResolveAreasSupportByRouteConstraint, (vc) =>
                {
                    vc.Version("v1", "ǰ̨�ӿ� API", true).Description("ǰ̨�ӿ�");

                    vc.Version("Admin", "��̨�ӿ� API").Description("��̨�ӿ�");
                });

                //����token������ͷ��
                c.ApiKey("Authorization").Description("token Ψһֵ").In("header").Name("token");
                
                //��������Ŀ¼�µ�����ע���ĵ�
                var pathXml = string.Format("{0}/bin/", AppDomain.CurrentDomain.BaseDirectory);
                var fileXml = Directory.GetFiles(pathXml).Where(o => Path.GetExtension(o).ToLower() == ".xml");
                foreach (var file in fileXml)
                {
                    c.IncludeXmlComments(file);
                }

                //����Swagger����ѡ��
                c.DocumentFilter<SwaggerAreasSupportDocumentFilter>();

                //�����ϴ��ļ����˲���
                //c.OperationFilter<AddUploadOperationFilter>();

                //��ʾ��������Ϣ
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
