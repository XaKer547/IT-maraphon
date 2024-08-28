using Hangfire.Storage;
using IT_maraphon.API.Documentation.RequestExamples;
using IT_maraphon.Application.Commands;
using IT_maraphon.Domain.Models.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;
using System.Drawing;

namespace IT_maraphon.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CanvasController(IMediator mediator, IStorageConnection storage, IWebHostEnvironment webHost) : ControllerBase
    {
        private readonly IMediator mediator = mediator;
        private readonly IStorageConnection storage = storage;
        private readonly IWebHostEnvironment webHost = webHost;

        [HttpPost("start-processing")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(Guid))]
        [SwaggerRequestExample(typeof(CreateCanvasCommand), typeof(CreateCanvasCommandExamples))]
        public async Task<IActionResult> CreateCanvas(CreateCanvasCommand command)
        {
            var jobId = await mediator.Send(command);
            return Ok(jobId);
        }

        [HttpGet("check-process")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(CanvasJobStatuses))]
        public async Task<IActionResult> CreateCanvas([FromQuery] Guid processId)
        {
            var stateData = storage.GetStateData(processId.ToString());

            if (stateData == null)
                return NotFound();

            var status = Enum.Parse<CanvasJobStatuses>(stateData.Name);

            return Ok(status);
        }

        [HttpGet("process-scheme")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        [Produces("image/jpeg")]
        public async Task<IActionResult> GetCanvas([FromQuery] Guid processId)
        {
            var file = webHost.WebRootFileProvider.GetFileInfo(processId.ToString() + ".jpg");

            if (!file.Exists)
                return NotFound();

            var bitmap = new Bitmap(Image.FromFile(file.PhysicalPath), new Size(500, 500));

            var stream = new MemoryStream();

            bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);

            return File(stream.ToArray(), "image/jpeg");
        }
    }
}