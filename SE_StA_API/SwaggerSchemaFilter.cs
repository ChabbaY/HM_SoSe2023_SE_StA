using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.Runtime.Serialization;

namespace SE_StA_API
{
    public class SwaggerSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (schema?.Properties == null)
            {
                return;
            }

            var ignoreDataMemberProperties = context.Type.GetProperties()
                .Where(t => t.GetCustomAttribute<IgnoreDataMemberAttribute>() != null);

            foreach (var property in ignoreDataMemberProperties)
            {
                var propertyToHide = schema.Properties.Keys
                    .SingleOrDefault(x => x.ToLower() == property.Name.ToLower());

                if (propertyToHide != null)
                {
                    schema.Properties.Remove(propertyToHide);
                }
            }
        }
    }
}