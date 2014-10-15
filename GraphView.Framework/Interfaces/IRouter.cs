using System.Windows;

namespace GraphView.Framework.Interfaces
{
    public interface IRouter
    {
        Point[] CalculateGeometry(Point start, Point end);
    }
}
