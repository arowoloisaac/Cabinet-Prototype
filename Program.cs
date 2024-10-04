
using Cabinet_Prototype.Configurations;
using Cabinet_Prototype.Data;
using Cabinet_Prototype.Models;
using Cabinet_Prototype.Services.UserService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Cabinet_Prototype
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services
                .AddDbContext<ApplicationDbContext>(options =>options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


            builder.Services.AddScoped<IUserService, UserService>();


            builder.Services.AddIdentity<User, Role>( options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.SignIn.RequireConfirmedEmail = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 8;
            }).AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddAuthorization( options =>
            {
                options.AddPolicy(ApplicationRoleName.Admin, new AuthorizationPolicyBuilder()
                    .RequireClaim(ClaimTypes.Role, ApplicationRoleName.Admin)
                    .RequireRole(ApplicationRoleName.Admin)
                    .RequireAuthenticatedUser()
                    .Build()
                );

                options.AddPolicy(ApplicationRoleName.Teacher, new AuthorizationPolicyBuilder()
                    .RequireClaim(ClaimTypes.Role, ApplicationRoleName.Teacher)
                    .RequireRole(ApplicationRoleName.Teacher)
                    .RequireAuthenticatedUser()
                    .Build()
                );

                options.AddPolicy(ApplicationRoleName.Student, new AuthorizationPolicyBuilder()
                    .RequireClaim(ClaimTypes.Role, ApplicationRoleName.Student)
                    .RequireRole(ApplicationRoleName.Student)
                    .RequireAuthenticatedUser()
                    .Build()
                );
            });

            var jwtSection = builder.Configuration.GetSection("JwtBearerToken");
            builder.Services.Configure<JwtBearerSetting>(jwtSection);

            var jwtConfig = jwtSection.Get<JwtBearerSetting>();
            var key = Encoding.ASCII.GetBytes(jwtConfig.SecretKey);

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer( options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = jwtConfig.Audience,
                    ValidIssuer = jwtConfig.Issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                };
            });
            
            var app = builder.Build();

            using var serviceScope = app.Services.CreateScope();
            var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
            context.Database.Migrate();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
