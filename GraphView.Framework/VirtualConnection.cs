using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;
using GraphView.Framework.Interfaces;
using GraphView.Framework.Routers;

namespace GraphView.Framework
{
    internal class VirtualConnection : IConnection, INotifyPropertyChanged
    {
        internal VirtualConnection(IConnectionPoint source)
        {
            StartPoint = source;
            EndPoint = null;
            Router = new DirectLineRouter();
        }

        public IConnectionPoint StartPoint { get; private set; }
        public IConnectionPoint EndPoint { get; private set; }
        public IRouter Router { get; private set; }
        public bool IsSelected { get; set; }

        public PointCollection Data { get; private set; }
        public void UpdateConnectionPoints(Point startPoint, Point endPoint)
        {
            Data = new PointCollection(Router.CalculateGeometry(startPoint, endPoint));
            OnPropertyChanged("Data");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
