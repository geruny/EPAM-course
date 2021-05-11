using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyLib;
using NLog;
using NLog.Extensions.Logging;
using System;

namespace ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var logger = LogManager.GetCurrentClassLogger();
            try
            {
                var config = new ConfigurationBuilder()
                    .SetBasePath(System.IO.Directory.GetCurrentDirectory()) //From NuGet Package Microsoft.Extensions.Configuration.Json | for what this line??
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true) //And this one
                    .Build();

                var servicesProvider = BuildDi(config);
                using (servicesProvider as IDisposable)
                {
                    servicesProvider.GetRequiredService<Converter>();

                    PrintL("Input a string with number");
                    var numStr = Console.ReadLine();
                    PrintL();

                    // var num = Converter.StringToInt("-2147483648");
                    // var num = Converter.StringToInt(" ");
                    var num = Converter.StringToInt(numStr);

                    PrintL();
                    PrintL("Result of parsing: " + num);
                }
            }
            catch (Exception ex)
            {
                PrintL("--------------");
                logger.Error(ex, "The program finished with an error");
                PrintL();

                throw;
            }
            finally
            {
                LogManager.Shutdown();
            }
        }

        private static void PrintL(string str = "") => Console.WriteLine(str);

        private static IServiceProvider BuildDi(IConfiguration config)
        {
            return new ServiceCollection()
                .AddTransient<Converter>()
                .AddLogging(loggingBuilder =>
                {
                    loggingBuilder.ClearProviders();
                    loggingBuilder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                    loggingBuilder.AddNLog("NLog.config");
                })
                .BuildServiceProvider();
        }
    }
}
