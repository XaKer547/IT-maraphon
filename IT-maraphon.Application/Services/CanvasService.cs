using AutoMapper;
using IT_maraphon.DataAccess.Data.Entities;
using IT_maraphon.DataAccess.Data.Entities.Abstract;
using IT_maraphon.Domain.Models;
using IT_maraphon.Domain.Models.Enums;
using IT_maraphon.Domain.Services;
using Newtonsoft.Json;
using System.Drawing;

namespace IT_maraphon.Application.Services
{
    public class CanvasService(IMapper mapper) : ICanvasService
    {
        private readonly IMapper mapper = mapper;

        public async Task<Bitmap> CreateCanvas(IReadOnlyCollection<FigureDTO> figures)
        {
            const int ImageSize = 1000;

            var image = new Bitmap(ImageSize, ImageSize);

            var graphics = Graphics.FromImage(image);

            graphics.FillRectangle(Brushes.White, new RectangleF(0, 0, ImageSize, ImageSize));

            foreach (var figure in figures)
            {
                var model = ConvertDtoToModel(figure);

                model.Draw(graphics);
            }

            return image;
        }

        private Figure ConvertDtoToModel(FigureDTO dto)
        {
            return dto.FigureType switch
            {
                FigureTypes.Circle => DictionaryToObject<Circle>(dto.Parameters),
                FigureTypes.Rectangle => DictionaryToObject<DataAccess.Data.Entities.Rectangle>(dto.Parameters),
                _ => throw new NotImplementedException()
            };
        }

        static T DictionaryToObject<T>(IDictionary<string, object> dictionary) where T : new()
        {
            var json = JsonConvert.SerializeObject(dictionary);

            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}