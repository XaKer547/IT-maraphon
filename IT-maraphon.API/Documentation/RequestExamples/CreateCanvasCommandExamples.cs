using IT_maraphon.Application.Commands;
using IT_maraphon.Domain.Models;
using IT_maraphon.Domain.Models.Enums;
using Swashbuckle.AspNetCore.Filters;
using System.Drawing;

namespace IT_maraphon.API.Documentation.RequestExamples;

public class CreateCanvasCommandExamples : IMultipleExamplesProvider<CreateCanvasCommand>
{
    public IEnumerable<SwaggerExample<CreateCanvasCommand>> GetExamples()
    {
        var circle = new FigureDTO()
        {
            FigureType = FigureTypes.Circle,
            Parameters = new Dictionary<string, object>
            {
                { "r", 50 },
                { "center", new Point(50, 50) },
            }
        };

        var rectangle = new FigureDTO()
        {
            FigureType = FigureTypes.Rectangle,
            Parameters = new Dictionary<string, object>
            {
                { "topLeft", new Point(0, 0) },
                { "bottomRight", new Point(45, -45) },
            }
        };

        yield return SwaggerExample.Create("Нарисовать круг", new CreateCanvasCommand()
        {
            Figures = [circle]
        });

        yield return SwaggerExample.Create("Нарисовать прямоугольник", new CreateCanvasCommand()
        {
            Figures = [rectangle]
        });

        yield return SwaggerExample.Create("Нарисовать круг и прямоугольник", new CreateCanvasCommand()
        {
            Figures = [circle, rectangle]
        });
    }
}