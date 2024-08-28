using IT_maraphon.Domain.Services;

namespace IT_maraphon.API.Services;

public class CanvasProviderConfiguration(IWebHostEnvironment webHostEnvironment) : ICanvasProviderConfiguration
{
    private readonly IWebHostEnvironment webHostEnvironment = webHostEnvironment;
    public string CanvasRootDirectory => webHostEnvironment.WebRootPath;
}
