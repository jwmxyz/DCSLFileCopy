using CommandLine;
using FileCopy.Config;
using FileCopy.Wrappers;
using Microsoft.Extensions.DependencyInjection;

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
            ConfigureServices(services);
            services
                .AddSingleton<FileCopier, FileCopier>()
                .BuildServiceProvider()
                .GetService<FileCopier>()
                .Execute(opts);
        }
    }
}
