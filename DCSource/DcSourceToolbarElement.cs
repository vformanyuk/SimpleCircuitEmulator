using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using GraphView.Infrastructure;
using GraphView.Infrastructure.Interfaces;

namespace DCSource
{
    public class DcSourceToolbarElement : IToolbarElement
    {
        private static DependencyObject s_view;

        public DcSourceToolbarElement()
        {
            if (s_view == null)
            {
                s_view = new TextBlock(new Bold(new Run("DC")));
            }
        }

        public DependencyObject View
        {
            get { return s_view; }
        }

        public CircuitElement CreateElement()
        {
            return new DcSourceViewModel();
        }
    }
}
