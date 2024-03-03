var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
} else
{
    app.UseExceptionHandler("Errors/ShowError");
}

app.UseHsts();
app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();
app.MapControllers();

app.Run();
