using System.Net.Http.Headers;
using Polly;
using Polly.Extensions.Http;

namespace AspApp.Services;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddJsonPlaceholderService(this IServiceCollection services)
    {
        services.AddHttpClient<IJsonPlaceholderService, JsonPlaceholderService>(client =>
            {
                client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com");
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
            })
            .AddPolicyHandler(GetRetryPolicy())
            .AddPolicyHandler(GetCircuitBreakerPolicy());

        return services;
    }

    private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .WaitAndRetryAsync(3, retryAttempt => 
                TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
    }

    private static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));
    }
}