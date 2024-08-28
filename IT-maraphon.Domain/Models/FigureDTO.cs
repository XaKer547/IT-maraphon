using IT_maraphon.Domain.Models.Enums;

namespace IT_maraphon.Domain.Models;

public class FigureDTO
{
    public FigureTypes FigureType { get; set; }
    public IDictionary<string, object> Parameters { get; set; }
}