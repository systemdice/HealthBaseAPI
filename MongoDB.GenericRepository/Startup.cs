using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MongoDB.GenericRepository.Context;
using MongoDB.GenericRepository.Interfaces;
using MongoDB.GenericRepository.Model;
using MongoDB.GenericRepository.Persistence;
using MongoDB.GenericRepository.Repository;
using MongoDB.GenericRepository.UoW;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;
using System.Linq;

namespace MongoDB.GenericRepository
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.Configure<FormOptions>(o => {
            //    o.ValueLengthLimit = int.MaxValue;
            //    o.MultipartBodyLengthLimit = int.MaxValue;
            //    o.MemoryBufferThreshold = int.MaxValue;
            //});
            //services.AddCors(); // Make sure you call this previous to AddMvc
            //services.AddCors(c =>
            //{
            //    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());

            //});

            //services.AddCors(c =>
            //{
            //    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod());

            //});
            //services.AddCors(c =>
            //{
            //    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyMethod());

            //});
            // Add Cors
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOriginDev",
                          builder =>
                          {
                          builder.WithOrigins
                    (

                      "http://localhost:4200",
                      "http://localhost:4201",
                      "http://45.34.15.113:9002",
                      "http://45.34.15.113:9007", //Health+nuasakala
                      "http://45.34.15.113:9008",
                      "http://45.34.15.113:9009", //healthdemo
                      "http://45.34.15.113:9011",
                      "http://45.34.15.113:9012",
                      "http://45.34.15.113:9013",
                      "http://45.34.15.113:9014",
                      "http://45.34.15.113:9015",
                      "http://45.34.15.113:9016",
                      "http://45.34.15.113:9017",
                      "http://45.34.15.113:9018",
                      "http://45.34.15.113:9019" // PROD URL
                          //PC1
                          //"https://productmdm-web.ausmpc01.pcf.dell.com",
                          //S3B
                          //"https://productmdm-web.ausmsc01.pcf.dell.com"
                         )
                         .AllowCredentials()
                         .AllowAnyMethod()
                         .AllowAnyHeader();
                          });
            });


            //services.AddMvc().AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());//Change the json result to Pascal cast property;



            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddControllers()
    .AddNewtonsoftJson();
            //    services.AddMvc()
            //.AddJsonOptions(options => options.SerializerSettings.ContractResolver
            // = new DefaultContractResolver());
            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));
            //services.AddMvc().AddJsonOptions(options => options.SerializerSettings.ContractResolver
            //= new CamelCasePropertyNamesContractResolver());
            //services.AddMvc().AddNewtonsoftJson(o =>
            //{
            //    o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            //});
            services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.PropertyNamingPolicy = null;
        }
        );
            //services.AddControllers().AddNewtonsoftJson(options =>
            //{
            //    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            //});
            services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ContractResolver = new DefaultContractResolver();
    });
            
            // Configure the persistence in another layer
            MongoDbPersistence.Configure();
            //services.AddSwaggerGen(s =>
            //{
            //    s.SwaggerDoc("v1", new Info
            //    {
            //        Version = "v1",
            //        Title = "SystemDICE API",//"MongoDB Repository Pattern and Unit of Work - Example",
            //        Description = "SystemDICE Swagger box",
            //        Contact = new Contact { Name = "SystemDICE", Email = "debasmitsamal@gmail.com", Url = "http://www.systemdice.com" }

            //    });
            //});
            //services.AddSwaggerGen();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            });
            RegisterServices(services);

            services.AddMvc(option => option.EnableEndpointRouting = false);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

           
            //        app.UseCors(
            //    options => options.WithOrigins("http://localhost:4200").AllowAnyMethod()
            //);

            //app.UseCors(options => options.AllowAnyOrigin());
            //app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader());
            //app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod());
            //app.UseCors(options => options.AllowAnyOrigin().allo);
            app.UseCors("AllowSpecificOriginDev");
            //app.UseStaticFiles();
            //app.UseStaticFiles(new StaticFileOptions()
            //{
            //    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
            //    RequestPath = new PathString("/Resources")
            //});
            app.UseHttpsRedirection();
            app.UseMvc();
            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync("Before1 Invoke from 2nd app.Use()\n");
            //    await next();
            //    //await context.Response.WriteAsync("After Invoke from 2nd app.Use()\n");
            //});
            //app.UseCors(MyAllowSpecificOrigins);
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            //app.UseSwagger();
            //app.UseSwagger(c =>
            //{
            //    c.RouteTemplate = "/swagger/{documentName}/swagger.json";
            //});
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            //app.UseSwaggerUI(c =>
            //{
            //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            //});
            //app.UseSwagger(c =>
            //{
            //    c.RouteTemplate = "swagger/{documentName}/swagger.json";
            //});
            //app.UseSwaggerUI(s =>
            //{
            //    s.SwaggerEndpoint("/swagger/v1/swagger.json", "SysDICE Repository Pattern plus Unit of Work API v2.0");
            //});
            app.UseSwagger(c =>
            {
                c.RouteTemplate = "/swagger/{documentName}/swagger.json";
            });
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));

            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync("Before Invoke from 2nd app.Use()\n");
            //    await next();
            //    //await context.Response.WriteAsync("After Invoke from 2nd app.Use()\n");
            //});
            loggerFactory.AddFile("Logs/myapp-{Date}.txt");

            
        }

        private void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IMongoContext, MongoContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IUserRegistrationRepository, UserRegistrationRepository>();
            services.AddScoped<IDeptInvestigationRepository, DeptInvestigationRepository>();
            services.AddScoped<IUnitsCategoryRepository, UnitsCategoryRepository>();
            services.AddScoped<ITestsCategoryRepository, TestsCategoryRepository>();
            services.AddScoped<ICategoryMasterRepository, CategoryMasterRepository>();
            services.AddScoped<IPatientDetailsRepository, PatientDetailsRepository>();
            services.AddScoped<IReferralMasterRepository, ReferralMasterRepository>();
            services.AddScoped<ITestHistoryRepository, TestHistoryRepository>();
            services.AddScoped<IAppointmentDetailRepository, AppointmentDetailRepository>();
            services.AddScoped<IAvailabilityRepository, AvailabilityRepository>();
            services.AddScoped<INewModifyCaseRepository, NewModifyCaseRepository>();
            services.AddScoped<IAddQuestionRepository, AddQuestionRepository>();
            services.AddScoped<IFarmacyDeliveryToPatientRepository, FarmacyDeliveryToPatientRepository>();
            services.AddScoped<IInventoryMasterRepository, InventoryMasterRepository>();
            services.AddScoped<IExpensesRepository, ExpensesRepository>();
            services.AddScoped<IReportRepository, ReportRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IPaymentHistoryRepository, PaymentHistoryRepository>();
            services.AddScoped<ILabTestMasterRepository, LabTestMasterRepository>();
            services.AddScoped<ILabTestIndividualRepository, LabTestIndividualRepository>();
            services.AddScoped<IUserDetailsRepository, UserDetailsRepository>();
            services.AddScoped<ITimesheetRepository, TimesheetRepository>();
            services.AddScoped<IFollowupRepository, FollowupRepository>();
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddScoped<ICompanyMasterRepository, CompanyMasterRepository>();
            services.AddScoped<IBedManagementRepository, BedManagementRepository>();
            services.AddScoped<IVaccinationsRepository, VaccinationsRepository>();
            services.AddScoped<IPrintBillRepository, PrintBillRepository>();
            services.AddScoped<ILeaveMangementRepository, LeaveMangementRepository>();
            services.AddScoped<IGroupTestsRepository, GroupTestsRepository>();

        }
    }
}
