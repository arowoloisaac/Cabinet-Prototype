using Microsoft.OpenApi.Models;
using System.Runtime.CompilerServices;

namespace mimistore.Utility.SwaggerEXT
{
    /// <summary>
    /// 这个类就是扩展了swagger的相关配置
    /// </summary>
    public static class CustomSwaggerEXT
    {
        /// <summary>
        /// 配置swagger
        /// </summary>
        /// <param name="builder"></param>
        public static void AddSwaggerEXT(this WebApplicationBuilder builder)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(option =>
            { 
                var file = Path.Combine(AppContext.BaseDirectory, "Cabinet_Prototype.xml");
                option.IncludeXmlComments(file, true);
                option.OrderActionsBy(o => o.RelativePath);

                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "please enter token",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer"
                });


                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference()
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });
        }

        /// <summary>
        /// use swagger
        /// </summary>
        /// <param name="app"></param>
        public static void UseSwaggerEXT(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
    }
}
