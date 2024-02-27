using BusinessLayer.InterFace;
using BusinessLayer.Sessions;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RepositoryLayer.InterFace;
using RepositoryLayer.Sessions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BookStore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddTransient<IUserBusiness, UserBusiness>();
            services.AddTransient<IUserRepo, UserRepo>();
            services.AddTransient<IBookRepo, BookRepo>();
            services.AddTransient<IBookBusiness, BookBusiness>();
            services.AddTransient<IAddressBusiness, AddressBusiness>();
            services.AddTransient<IAddressRepo, AddressRepo>();
            services.AddTransient<ICartBusiness, CartBusiness>();
            services.AddTransient<ICartRepo, CartRepo>();
            services.AddTransient<IOrderBusiness, OrderBusiness>();
            services.AddTransient<IOrderRepo, OrderRepo>();
            services.AddTransient<IWishListBusiness, WishListBusiness>();
            services.AddTransient<IWishListRepo, WishListRepo>();
            services.AddTransient<IReviewRepo, ReviewRepo>();
            services.AddTransient<IReviewBusiness, ReviewBusiness>();

            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "Jwt",
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
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                var Key = Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]);
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Key)
                };
            });

            services.AddCors(options =>
            {
                options.AddPolicy(
                name: "AllowOrigin",
              builder =>
              {
                  builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
              });
            });

            //services.AddMassTransit(x =>
            //{
            //    x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(config =>
            //    {
            //        config.UseHealthCheck(provider);
            //        config.Host(new Uri("rabbitmq://localhost"), h =>
            //        {
            //            h.Username("guest");
            //            h.Password("guest");
            //        });
            //    }));
            //});
            //services.AddMassTransitHostedService();


        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            
            app.UseRouting();
            app.UseCors("AllowOrigin");

            app.UseAuthentication();
            app.UseAuthorization();

            


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();

            // This middleware serves the Swagger documentation UI
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "BookStore API V1");
            });
        }
    }
}
