using CookingCourseAPI.Repositories;
using CookingCourseAPI.Services.Interfaces;
using CookingCourseAPI.Services;
using CookingCourseAPI.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Security.Claims;
using CookingCourseAPI.Repositories.Interfaces;

namespace CookingCourseAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            // ================= CORS ====================
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            // ============= JWT Authentication ============
            var jwtSettings = builder.Configuration.GetSection("Jwt");
            var jwtKey = jwtSettings["Key"];
            var jwtIssuer = jwtSettings["Issuer"];

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
                    RoleClaimType = ClaimTypes.Role
                };
            });

            builder.Services.AddAuthorization();

            // ============ DI cho Repository và Service ============
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped<IBlogReportService, BlogReportService>();
            builder.Services.AddScoped<IBlogReportRepository, BlogReportRepository>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<ICommentService, CommentService>();
            builder.Services.AddScoped<IBlogService, BlogService>();
            builder.Services.AddScoped<IBlogRepository, BlogRepository>();
            builder.Services.AddScoped<ICommentRepository, CommentRepository>();
            builder.Services.AddScoped<ICommentReportRepository, CommentReportRepository>();
            builder.Services.AddScoped<ICourseService, CourseService>();
            builder.Services.AddScoped<ICourseRepository, CourseRepository>();
            builder.Services.AddScoped<ICourseVideoRepository, CourseVideoRepository>();
            builder.Services.AddScoped<ICourseVideoService, CourseVideoService>();
            builder.Services.AddScoped<ICourseVideoRepository, CourseVideoRepository>();
            builder.Services.AddScoped<INewsRepository, NewsRepository>();
            builder.Services.AddScoped<INewsService, NewsService>();
            builder.Services.AddScoped<IStatisticsRepository, StatisticsRepository>();
            builder.Services.AddScoped<IStatisticsService, StatisticsService>();

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
                options.JsonSerializerOptions.WriteIndented = true; // Tùy chọn hiển thị dễ đọc
            });
            // ============ Swagger có hỗ trợ JWT ============
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CookingCourse API", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Nhập token dạng: Bearer {your token}"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });

            // ============ Kết nối DbContext ============
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            var app = builder.Build();

            // ============ Middleware ============
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStaticFiles();
            app.UseCors("AllowAll");
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            // Trang chủ trả về index.html nếu có
            app.MapGet("/", async context =>
            {
                await context.Response.SendFileAsync(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "index.html"));
            });

            app.Run();
        }
    }
}
