using System.Windows;
using GraphView.Framework.Interfaces;

namespace GraphView.Framework.Routers
{
    public class DirectLineRouter : IRouter
    {
        public Point[] CalculateGeometry(Point start, Point end)
        {
            return new[] { start, new Point((start.X + end.X) / 2, (start.Y + end.Y) / 2), end };
        }
    }
}
