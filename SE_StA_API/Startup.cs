using System.Reflection;
using System.Text;
using SE_StA_API.Authentication;
//using SE_StA_API.Email;
using SE_StA_API.Store;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace SE_StA_API {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddCors(options => {
                options.AddDefaultPolicy(
                    policy => {
                        policy.AllowAnyOrigin();
                    });
            });

            services.AddControllers();
            services.AddMvc();

            services.AddDbContext<ApplicationContext>(options => {
                options.UseNpgsql(Configuration["ConnectionStrings:DBConnection"]);
            });

            services.AddSwaggerGen(options => {
                options.SwaggerDoc("v0", new OpenApiInfo {
                    Version = "v0",
                    Title = "Softwareengineering II - Studienarbeit - API",
                    Description = "An ASP.NET API for a hotel booking system"
                });
                options.EnableAnnotations();

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
                options.SchemaFilter<SwaggerSchemaFilter>();

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme {
                            Reference = new OpenApiReference {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => {
                    options.TokenValidationParameters = new TokenValidationParameters() {
                        ClockSkew = TimeSpan.Zero,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = "apiWithAuthBackend",
                        ValidAudience = "apiWithAuthBackend",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("G6Gn&nCA$u8mna%4"))
                    };
                });

            services.AddIdentity<IdentityUser, IdentityRole>(options => {
                options.SignIn.RequireConfirmedAccount = true;
                options.User.RequireUniqueEmail = true;
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 12;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
            })
                .AddEntityFrameworkStores<ApplicationContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<TokenService, TokenService>();

            /*services.AddSingleton(new EmailConfiguration {
                From = "",
                FromDisplayName = "",
                SmtpServer = "",
                Port = 587,
                Username = "",
                Password = ""
            });
            services.AddScoped<IEmailSender, EmailSender>();*/
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationContext context) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseSwagger();
            app.UseSwaggerUI(options => {
                options.SwaggerEndpoint("/swagger/v0/swagger.json", "SE StA API V0");
            });

            app.UseCors(builder => {
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}