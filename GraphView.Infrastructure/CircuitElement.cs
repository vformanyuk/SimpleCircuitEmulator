using System.Windows;
using GraphView.Framework;
using GraphView.Framework.Interfaces;

namespace GraphView.Infrastructure
{
    public class CircuitElement : DiagramNode
    {
        public DependencyObject View { get; private set; }
    }
}
