using Angel.DataAccess;
using Angel.Service;
using Microsoft.Extensions.Caching.Memory;
using NuGet.Packaging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddMemoryCache();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.AddControllers(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true).AddNewtonsoftJson();

//AddSingleton
builder.Services.AddTransient<IDatabase, MysqlService>();
builder.Services.AddSingleton<IDataService, BLLService>();
builder.Services.AddSingleton<IApplicationSettingService, ApplicationSettingInMemoryService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<ICacheService, DataCache>();
builder.Services.AddInitializationHostServiceSetup();

//缓存
//HttpContext.Current.Application["key"] = Angel.Utils.XmlHelper.GetXmlCaches();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();
app.UseRouting();
app.UseSession();//添加会话中间              
app.UseCookiePolicy(); // 使用cookie
app.UseAuthorization();
app.UseFileServer();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}"
    );

app.Run();
