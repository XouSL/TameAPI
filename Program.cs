using TameAPI.Context;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();

builder.Services.AddMvc(mvc => { mvc.EnableEndpointRouting = false; });
builder.Services.AddSingleton<ApplicationContext>();

var app = builder.Build();



app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMvc(routes =>
{
    routes.MapRoute(
        name: "default",
        template: "{controler = App}/{action =Index}/{id?}");
    routes.MapRoute("NotFound", "{*url}",
        new { controller = "App", action = "RedirectToMain" });

});

app.MapControllers();

app.Run();
