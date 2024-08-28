using IT_maraphon.Domain.Models;
using System.Drawing;

namespace IT_maraphon.Domain.Services;

public interface ICanvasService
{
    Task<Bitmap> CreateCanvas(IReadOnlyCollection<FigureDTO> figures);
}