using IT_maraphon.Domain.Models;
using MediatR;

namespace IT_maraphon.Application.Commands;

public class CreateCanvasCommand : IRequest<string>
{
    public IReadOnlyCollection<FigureDTO> Figures { get; set; }
}