using Microsoft.Extensions.FileProviders;

namespace IT_maraphon.API.Helpers
{
    public static class ApplicationStaticFilesHelper
    {
        public static IHostApplicationBuilder ConfigureStaticFilesFolder(this IHostApplicationBuilder builder)
        {
            var directory = new DirectoryInfo(Path.Combine(builder.Environment.ContentRootPath, "schemes"));

            if (!directory.Exists)
                directory.Create();

            builder.Environment.ContentRootPath = directory.FullName;
            builder.Environment.ContentRootFileProvider = new PhysicalFileProvider(directory.FullName);

            return builder;
        }
    }
}