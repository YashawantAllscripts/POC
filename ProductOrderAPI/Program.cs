using InterfaceLibrary;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using ProductOrderAPI;
using RepositoryLibrary;
using ServicesLibrary;
using UtilitiesLibrary;

public class Program
{
    /// <summary>
    /// Application entry point
    /// </summary>
    /// <param name="args">
    /// Application arguments
    /// </param>
    public static void Main(string[] args)
    {
       CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args)=>
        Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder => {  webBuilder.UseStartup<Startup>(); });
}
