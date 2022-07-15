using CommandLine;
using FileCopy.Config;
using FileCopy.Helpers;
using FileCopy.Wrappers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FileCopy
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
                .AddSingleton<IPathWrapper, PathWrapper>();
        }

        public static void RunProgram(CLIOptions opts)
        {
            ICollection<ValidationResult> lstvalidationResult;
            if (!opts.Validate(out lstvalidationResult))
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
    }
}
