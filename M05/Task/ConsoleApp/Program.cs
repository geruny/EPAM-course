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
        private static void Main(string[] args)
        {
            var logger = LogManager.GetCurrentClassLogger();
            try
            {
                var servicesProvider = BuildDi();
                using (servicesProvider as IDisposable)
                {
                    servicesProvider.GetRequiredService<Converter>();

                    PrintL("Input a string with number");
                    var numStr = Console.ReadLine();
                    PrintL();

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

        private static IServiceProvider BuildDi()
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
