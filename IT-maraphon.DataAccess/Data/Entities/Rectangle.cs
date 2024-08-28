using IT_maraphon.DataAccess.Data.Entities.Abstract;
using System.Drawing;

namespace IT_maraphon.DataAccess.Data.Entities;

public class Rectangle : Figure
{
    public Point TopLeft { get; set; }
    public Point BottomRight { get; set; }
    protected override Pen Pen => new(Color.Black);

    public override void Draw(Graphics graphics)
    {
        var width = BottomRight.X - TopLeft.X;

        var height = TopLeft.Y - BottomRight.Y;

        graphics.DrawRectangle(Pen, TopLeft.X, TopLeft.Y, width, height);
    }
}