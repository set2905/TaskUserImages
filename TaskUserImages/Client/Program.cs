using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using TaskUserImages.Client;
using Refit;
using TaskUserImages.Client.API;
using Polly;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("TaskUserImages.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
    .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

string apiUrl = $"{builder.HostEnvironment.BaseAddress}api";
builder.Services
    .AddRefitClient<IImageFriendsAPI>().ConfigureHttpClient(c =>
        {
            c.BaseAddress = new Uri(apiUrl);
        })
    .AddHttpMessageHandler(sp => sp.GetRequiredService<BaseAddressAuthorizationMessageHandler>())
    .AddTransientHttpErrorPolicy(b => b.WaitAndRetryAsync(new[]
    {
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(5),
                    TimeSpan.FromSeconds(10),
    }));

// Supply HttpClient instances that include access tokens when making requests to the server project
//builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("TaskUserImages.ServerAPI"));

builder.Services.AddApiAuthorization();
builder.Services.AddMudServices();

await builder.Build().RunAsync();
