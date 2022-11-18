using EasyCodeAcademy.Web;

Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder =>
{
    webBuilder.UseStartup<Startup>();
    webBuilder.UseKestrel(options =>
    {
        options.Limits.MaxRequestBodySize = 409715200;
    });
}).Build().Run();