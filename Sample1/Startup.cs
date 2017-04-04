using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using BlobService.Core.Configuration;
using BlobService.MetaStore.EntityFrameworkCore.Configuration;
using BlobService.Storage.FileSystem.Configuration;

namespace Sample1
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            var builder = services.AddBlobService(opts =>
            {
                opts.MaxBlobSizeInMB = 100;
            })
            .AddEfMetaStores(opts =>
            {
                opts.ConnectionString = "Server=myServerAddress;Database=myDataBase;User Id=myUsername;Password = myPassword;";
            })
            .AddFileSystemStorageService(opts =>
            {
                opts.RootPath = @"C:\blobs";
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            app.UseBlobService();
        }
    }
}
