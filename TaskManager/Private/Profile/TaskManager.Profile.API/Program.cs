using TaskManager.Common.Application.WebConfiguration;
using TaskManager.Profile.Application;
using TaskManager.Profile.Repository.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();



builder.Services.AddProfileApp(ConfigureCommonDbContext.ConfigureDb(builder, "profile"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.EnsureDbCreatedForDevelopment<UserProfileDbContext>(true);


app.Run();
