using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GraphView.Framework;
using GraphView.Framework.Interfaces;

namespace GraphView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            _digram = new Diagram(new ConnectionFactory());

            for (int i = 0; i < 5; i++)
            {
                _digram.ChildNodes.Add(new CustomNode {Name = "Node #" + i, X = 10*i, Y = 10*i});
            }

            DataContext = this;
        }

        public IDiagram Diagram
        {
            get { return _digram; }
        }

        private readonly IDiagram _digram;

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
