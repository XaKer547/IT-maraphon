using IT_maraphon.DataAccess.Data.Entities.Abstract;
using System.Drawing;
using System.Text.Json.Serialization;

namespace IT_maraphon.DataAccess.Data.Entities;

public class Circle : Figure
{
    [JsonPropertyName("center")]
    public Point Center { get; set; }

    [JsonPropertyName("r")]
    public int R { get; set; }
    protected override Pen Pen => new(Color.Red);

    public override void Draw(Graphics graphics) => graphics.DrawEllipse(Pen, Center.X, Center.Y, R, R);
}