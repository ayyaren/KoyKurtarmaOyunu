var builder = WebApplication.CreateBuilder(args);

// Servisleri ekle
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Middleware'ler sýrasýyla:

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// Routing aktif et
app.UseRouting();

// Yetkilendirme aktif et (varsa)
app.UseAuthorization();

// Controller route'larýný eþle
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Oyun}/{action=Index}/{id?}");

app.Run();

