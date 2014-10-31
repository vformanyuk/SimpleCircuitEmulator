using System.Windows;
using GraphView.Infrastructure.Interfaces;

namespace GraphView.Infrastructure
{
    public abstract class ToolbarElement
    {
        public abstract DependencyObject Element { get; }

        public abstract ICircuitElement CreateElement();

        public virtual void BeginDrag()
        {
            DragDrop.DoDragDrop(Element, CreateElement(), DragDropEffects.Copy);
        }
    }
}
