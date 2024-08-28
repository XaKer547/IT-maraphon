using System.Drawing;

namespace IT_maraphon.DataAccess.Data.Entities.Abstract;

public abstract class Figure
{
    protected abstract Pen Pen { get; }
    public abstract void Draw(Graphics graphics);
}