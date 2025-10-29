
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.G02.Persistence;
using Store.G02.Persistence.Data.Contexts;
using Store.G02.Shared.ErrorModels;
using Store.G05.Domain.Contracts;
using Store.G05.Services;
using Store.G05.Services.Abstractions;
using Store.G05.Services.Mapping.Products;
using Store.G05.Web.Extensions;
using Store.G05.Web.Middlewares;
using System.Threading.Tasks; 

namespace Store.G05.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.RegisterAllServices(builder.Configuration);

            var app = builder.Build();

            await app.ConfigureMiddlewaresAsync();

            app.Run();
        }
    }
}
