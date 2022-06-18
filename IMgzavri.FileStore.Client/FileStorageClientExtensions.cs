using System;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace IMgzavri.FileStore.Client
{
    public static class FileStorageClientExtensions
    {
        public static void AddClientForFileStorage(this IServiceCollection services, string name, string uri){
            services.AddHttpClient(name, client =>
            {
                client.BaseAddress = new Uri(uri);
                //client.DefaultRequestHeaders.Add("APIKey", Configuration["APIKey"]);
                //client.DefaultRequestHeaders.Add("X-Version", Configuration["X-VERSION"]);
            });

            var httpClientFactory = GetHttpClientService(services);

            services.TryAddSingleton<IFileStorageClient>(_ => new FileStorageClient(httpClientFactory, name));
        }

        private static IHttpClientFactory GetHttpClientService(IServiceCollection services)
        {
            var sp = services.BuildServiceProvider();
            return sp.GetService(typeof(IHttpClientFactory)) as IHttpClientFactory;
        }
    }
}
