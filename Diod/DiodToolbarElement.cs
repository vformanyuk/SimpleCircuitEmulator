using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using GraphView.Infrastructure;
using GraphView.Infrastructure.Interfaces;

namespace Diod
{
    public class DiodToolbarElement : IToolbarElement
    {
        private static readonly DependencyObject s_view = new TextBlock(new Bold(new Run("Diod")));

        public DependencyObject View
        {
            get { return s_view; }
        }

        public CircuitElement CreateElement()
        {
            return new DiodViewModel();
        }
    }
}
