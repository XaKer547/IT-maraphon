using MediatR;

namespace IT_maraphon.Application.Queries;

public class GetCanvasJobStatusQuery(string jobId) : IRequest
{
    public string JobId { get; } = jobId;
}