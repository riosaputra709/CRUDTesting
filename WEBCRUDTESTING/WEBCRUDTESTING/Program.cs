var builder = WebApplication.CreateBuilder(args);

// Menambahkan HttpClient ke DI container
builder.Services.AddHttpClient();

// Menambahkan session ke DI container
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Sesuaikan waktu session
    options.Cookie.HttpOnly = true; // Mengatur agar cookie hanya dapat diakses oleh server
    options.Cookie.IsEssential = true; // Menjadikan session penting
});

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

// Menambahkan UseSession untuk middleware session
app.UseSession();  // Harus dipanggil sebelum UseAuthorization

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
