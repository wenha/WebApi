using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Description;

namespace Admin.WebApi.Swagger
{
    public class SwaggerHttpHeaderFilter : IOperationFilter
    {
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            if (operation.parameters == null)
            {
                operation.parameters = new List<Parameter>();
            }
        }
    }
}