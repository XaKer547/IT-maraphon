using FluentValidation;
using Hangfire;
using Hangfire.Server;
using IT_maraphon.Application.Commands;
using IT_maraphon.Domain.Services;
using MediatR;

namespace IT_maraphon.Application.CommandHandlers
{
    public class CreateCanvasCommandHandler(IValidator<CreateCanvasCommand> validator,
                                            ICanvasService canvasService,
                                            ICanvasProviderConfiguration canvasProviderConfiguration,
                                            IBackgroundJobClient backgroundJobClient)
        : IRequestHandler<CreateCanvasCommand, string>
    {
        private readonly IValidator<CreateCanvasCommand> validator = validator;
        private readonly ICanvasService canvasService = canvasService;
        private readonly ICanvasProviderConfiguration canvasProviderConfiguration = canvasProviderConfiguration;
        private readonly IBackgroundJobClient backgroundJobClient = backgroundJobClient;
        public async Task<string> Handle(CreateCanvasCommand request, CancellationToken cancellationToken)
        {
            await validator.ValidateAndThrowAsync(request, cancellationToken);

            var jobId = backgroundJobClient.Enqueue(() => CreateCanvasWithDelay(request, null));

            return jobId;
        }

        public async Task CreateCanvasWithDelay(CreateCanvasCommand request, PerformContext context)
        {
            const int Delay = 5000;

            await Task.Delay(Delay);

            var canvas = await canvasService.CreateCanvas(request.Figures);

            var savePath = @"C:\Users\student\Desktop\schemes";

            var canvasName = $"{context.BackgroundJob.Id}.jpg";

            canvas.Save(Path.Combine(savePath, canvasName));
        }
    }
}