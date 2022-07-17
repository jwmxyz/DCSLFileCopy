using CommandLine;
using CopyDirectory.ConsoleApp.Extension;
using CopyDirectory.Services;
using CopyDirectory.Services.Wrappers;
using CopyDirectory.Shared.Config;
using CopyDirectory.Shared.Utils;
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
                   .WithParsed(RunProgram)
                   .WithNotParsed(RunManually);
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services
                .AddSingleton<IDirectoryWrapper, DirectoryWrapper>()
                .AddSingleton<IPathWrapper, PathWrapper>()
                .AddSingleton<IFileWrapper, FileWrapper>()
                .AddSingleton<IMethodExecutionUtils, MethodExecutionUtils>()
                .AddSingleton<IMessageHandler, ConsoleMessageHandler>();
        }

        public static void RunManually(IEnumerable<Error> errors)
        {
            var cliOptions = new CLIOptions();
            bool validInput = false;
            while (!validInput)
            {
                Console.WriteLine("Input Source Directory Path:");
                string source = Console.ReadLine();
                try
                {
                    cliOptions.Source = new Uri(source);
                    validInput = true;
                }
                catch
                {
                    Console.Error.Write("Invalid source path..");
                }

            }
            validInput = false;
            while (!validInput)
            {
                Console.WriteLine("Input Destination Directory Path:");
                string source = Console.ReadLine();
                try
                {
                    cliOptions.Destination = new Uri(source);
                    validInput = true;
                }
                catch
                {
                    Console.Error.WriteLine("Invalid destination path..");
                }

            }
            RunProgram(cliOptions);
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
