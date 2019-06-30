using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Web.Http.Routing;

namespace Admin.WebApi.App_Start.Swagger
{
    public class ClassifiedHttpControllerSelector : DefaultHttpControllerSelector
    {
        private const string AreaRouteVariableName = "area";
        private const string CategoryRouteVariableName = "category";
        private const string TheFixControllerFolderName = "Controllers";

        private readonly HttpConfiguration _configuration;
        private readonly Lazy<ILookup<string, Type>> _apiControllerTypes;

        private ILookup<string, Type> ApiControllerTypes;

        public ClassifiedHttpControllerSelector(HttpConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
            _apiControllerTypes = new Lazy<ILookup<string, Type>>(GetApiControllerTypes);
        }

        public override HttpControllerDescriptor SelectController(HttpRequestMessage request)
        {
            //return GetApiController(request);
            HttpControllerDescriptor des = null;
            string controllerName = this.GetControllerName(request);
            if (!string.IsNullOrWhiteSpace(controllerName))
            {
                var groups = this.ApiControllerTypes[controllerName.ToLower()];
                if (groups != null && groups.Any())
                {
                    string endString;
                    var routeDic = request.GetRouteData().Values;//存在controllerName的必定取到IHttpRouteData
                    if (routeDic.Count > 1)
                    {
                        StringBuilder sb = new StringBuilder();
                        foreach (var key in routeDic.Keys)
                        {
                            sb.Append(".");
                            sb.Append(routeDic[key]);
                            if (key.Equals(DefaultHttpControllerSelector.ControllerSuffix,
                                StringComparison.CurrentCultureIgnoreCase))
                            {
                                //如果是Control，则代表命名空间结束
                                break;
                            }
                        }
                        sb.Append(DefaultHttpControllerSelector.ControllerSuffix);
                        endString = sb.ToString();
                    }
                    else
                    {
                        endString = string.Format(".{0}{1}", controllerName,
                            DefaultHttpControllerSelector.ControllerSuffix);
                    }
                    // 取NameSpace节点数最少的类型
                    var type =
                        groups.Where(t => t.FullName.EndsWith(endString, StringComparison.CurrentCultureIgnoreCase))
                            .OrderBy(t => t.FullName.Count(s => s == '.'))
                            .FirstOrDefault();//默认返回命名空间节点数最少的第一
                    if (type != null)
                    {
                        des = new HttpControllerDescriptor(this._configuration, controllerName, type);
                    }
                }
            }
            if (des == null)
            {
                throw new HttpResponseException(request.CreateErrorResponse(HttpStatusCode.NotFound,
                    string.Format("No route providing a controller name was found to match request URI '{0}'",
                        request.RequestUri)));
            }
            return des;
        }


        private static string GetRouteValueByName(HttpRequestMessage request, string routeName)
        {
            IHttpRouteData data = request.GetRouteData();
            if (data.Values.ContainsKey(routeName))
            {
                return data.Values[routeName] as string;
            }
            return null;
        }

        private ILookup<string, Type> GetApiControllerTypes()
        {
            IAssembliesResolver assembliesResolver = this._configuration.Services.GetAssembliesResolver();
            return
                this._configuration.Services.GetHttpControllerTypeResolver()
                    .GetControllerTypes(assembliesResolver)
                    .ToLookup(
                        t =>
                            t.Name.ToLower()
                                .Substring(0, t.Name.Length - DefaultHttpControllerSelector.ControllerSuffix.Length),
                        t => t);
        }
    }
}