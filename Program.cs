var builder = WebApplication.CreateBuilder(args);

// Servisleri ekle
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Middleware'ler s�ras�yla:

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

// Controller route'lar�n� e�le
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Oyun}/{action=Index}/{id?}");

app.Run();

