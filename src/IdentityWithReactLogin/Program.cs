using IdentityWithReactLogin.Configurations;
using IdentityWithReactLogin.Context;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services
    .AddMvcCore()
    .AddNewtonsoftJson(opt =>
    {
        opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    });

//builder.Services.AddCors();
builder.Services.ConfigureIdentity(builder.Configuration);
builder.Services.ConfigureSwagger();
builder.Services.AddOptions();

var app = builder.Build();

app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSwaggerConfiguration();
//app.ConfigureInitialDatabase();

//MyIdentityDbContext dbcontext = app.Services.GetService<MyIdentityDbContext>();
//dbcontext.Database.EnsureCreated();

//using (var scope =
//  app.Services.CreateScope())

//using (var context = scope.ServiceProvider.GetService<MyIdentityDbContext>())
//    context.Database.EnsureCreated();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.Run();
