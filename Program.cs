
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
using Cabinet_Prototype.Services.Initialization;
using Cabinet_Prototype.Services.SharedService;
using Cabinet_Prototype.Services.TokenService;
using Microsoft.OpenApi.Models;
using Cabinet_Prototype.Services.AdminService;
using Cabinet_Prototype.Services.Initialization.ConfigUser;
using Cabinet_Prototype.Services.Initialization.PasswordGenerator;
using Cabinet_Prototype.Services.FacultyService;
using Cabinet_Prototype.Services.DirectionService;
using Cabinet_Prototype.Services.GroupService;
using Cabinet_Prototype.Services.CourseService;
using Cabinet_Prototype.Services.EmailService;

namespace Cabinet_Prototype
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            //https://github.com/juldhais/DateOnlyWebApiExample/blob/master/Program.cs
            builder.Services.AddControllers().AddJsonOptions(opt => {
                opt.JsonSerializerOptions.Converters.Add(new DateonlyConfiguration());

            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
                option.EnableAnnotations();
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });

            builder.Services
                .AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


            builder.Services.AddScoped<IUserPermission, UserPermission>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ISharedService, SharedService>();
            builder.Services.AddScoped<ITokenGenerator, TokenGenerator>();
            builder.Services.AddScoped<IAdminService, AdminService>();
            builder.Services.AddScoped<IPasswordGen, PasswordGen>();
            builder.Services.AddScoped<IEmailService, EmailService>();
            builder.Services.AddScoped<IFacultyService, FacultyService>();
            builder.Services.AddScoped<IDirectionService, DirectionService>();
            builder.Services.AddScoped<IGroupService, GroupService>();
            builder.Services.AddScoped<ICourseService, CourseService>();


            builder.Services.AddIdentity<User, Role>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.SignIn.RequireConfirmedEmail = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 8;
            }).AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddAuthorization(options =>
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
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = jwtConfig.Audience,
                    ValidIssuer = jwtConfig.Issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    NameClaimType = ClaimTypes.NameIdentifier,
                    RoleClaimType = ClaimTypes.Role
                };
            });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend",
                    policy => policy.WithOrigins("http://localhost:5173") // ����ǰ�˵�ַ
                                    .AllowAnyHeader()
                                    .AllowAnyMethod());
            });

            var app = builder.Build();

            app.UseCors("AllowFrontend");

            app.MapControllers();

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

            app.UseAuthentication();
            app.UseAuthorization();

            await app.ConfigureRole();
            await app.ConfigureAdmin();

            app.MapControllers();

            app.Run();
        }
    }
}
