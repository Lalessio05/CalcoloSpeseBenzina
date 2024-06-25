using CalcoloSpeseBenzina.Components;
using Newtonsoft.Json;

namespace CalcoloSpeseBenzina
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Task a = new(CheckPrice);
            a.Start();
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();
            a.Wait();
            app.Run();
        }

        private static void CheckPrice()
        {

            if ((DateTime.Now - JsonConvert.DeserializeObject<PrezziBenzinaJsonFormat>(File.ReadAllText("prices.json"))!.Data ).TotalDays < 1)
                return;


            var (benzina, gasolio) = Crawler.CheckPrices();
            object a = new PrezziBenzinaJsonFormat
            {
                Benzina= benzina,
                Gasolio = gasolio,
                Data = DateTime.Now,
            };
            File.WriteAllText("prices.json", JsonConvert.SerializeObject(a));
        }
    }
}
