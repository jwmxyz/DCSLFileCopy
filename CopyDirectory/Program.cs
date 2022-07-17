using CommandLine;
using CopyDirectory.ConsoleApp.Extension;
using CopyDirectory.Services;
using CopyDirectory.Services.Wrappers;
using CopyDirectory.Shared.Config;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CopyDirectory.ConsoleApp
{
    public static class Program
    {
        private static readonly IServiceCollection services = new ServiceCollection();

        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<CLIOptions>(args)
                   .WithParsed(RunProgram);
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services
                .AddSingleton<IDirectoryWrapper, DirectoryWrapper>()
                .AddSingleton<IPathWrapper, PathWrapper>()
                .AddSingleton<IMessageHandler, ConsoleMessageHandler>();
        }

        public static void RunProgram(CLIOptions opts)
        {
            if (!opts.ValidateObject(out ICollection<ValidationResult> lstvalidationResult))
            {
                foreach (var error in lstvalidationResult)
                {
                    Console.Error.WriteLine("Error: " + error.ErrorMessage);
                }
                Environment.Exit(1);
            }

            ConfigureServices(services);
            services
                .AddSingleton<FileCopier, FileCopier>()
            .BuildServiceProvider()
            .GetService<FileCopier>()
            .Execute(opts);
        }

        public static void PrintToConsole(string message)
        {
            Console.WriteLine(message);
        }
    }
}
