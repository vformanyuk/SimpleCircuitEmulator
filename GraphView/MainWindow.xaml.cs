using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GraphView.Framework;
using GraphView.Framework.Interfaces;
using GraphView.Infrastructure;
using GraphView.Infrastructure.Interfaces;

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
            _plugins = new ObservableCollection<IGraphViewElementPlugin>();

            var catalog = new DirectoryCatalog(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            var container = new CompositionContainer(catalog);
            container.Compose(new CompositionBatch());
            var elements = container.GetExports<IGraphViewElementPlugin>();

            foreach (var element in elements)
            {
                _plugins.Add(element.Value);
            }

            DataContext = this;
        }

        public IDiagram Diagram
        {
            get { return _digram; }
        }

        public ObservableCollection<IGraphViewElementPlugin> Plugins
        {
            get { return _plugins; }
        }

        public IGraphViewElementPlugin SelectedItem { get; set; }

        private readonly IDiagram _digram;
        private readonly ObservableCollection<IGraphViewElementPlugin> _plugins;
        private Point _previousPosition;

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            
        }

        private void ToolbarMouseMove(object sender, MouseEventArgs e)
        {
            var listbox = (ListBox) sender;
            var position = e.GetPosition(listbox);

            if (Mouse.LeftButton == MouseButtonState.Pressed &&
                (Math.Abs(position.X - _previousPosition.X) >= SystemParameters.MinimumHorizontalDragDistance ||
                 Math.Abs(position.Y - _previousPosition.Y) >= SystemParameters.MinimumVerticalDragDistance))
            {
                var dataPack = new DataObject(typeof (CircuitElement), SelectedItem.GetCircuitElement());
                DragDrop.DoDragDrop(listbox, dataPack, DragDropEffects.Copy);
            }

            _previousPosition = position;
        }

        private void DiagramDrop(object sender, DragEventArgs e)
        {
            var dataPack = e.Data as DataObject;
            if (dataPack == null) return;

            var element = dataPack.GetData(typeof(CircuitElement)) as CircuitElement;
            if (element == null) return;

            var diagramPosition = e.GetPosition(sender as IInputElement);
            element.X = diagramPosition.X;
            element.Y = diagramPosition.Y;

            Diagram.ChildNodes.Add(element);
        }
    }
}
