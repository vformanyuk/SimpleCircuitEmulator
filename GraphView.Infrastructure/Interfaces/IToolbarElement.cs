using System;
using System.Windows;
namespace GraphView.Infrastructure.Interfaces
{
    public interface IToolbarElement
    {
        DependencyObject View { get; }

        [Obsolete]
        CircuitElement CreateElement();
    }
}
