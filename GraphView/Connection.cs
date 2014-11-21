using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;
using GraphView.Framework.Interfaces;
using GraphView.Framework.Routers;
using GraphView.Infrastructure.Annotations;

namespace GraphView
{
    public class Connection : IConnection, INotifyPropertyChanged
    {
        #region Constructors

        public Connection(IConnectionPoint source, IConnectionPoint destination)
        {
            StartPoint = source;
            EndPoint = destination;
            Router = new DirectLineRouter();
        }

        public Connection(IConnectionPoint source, IConnectionPoint destination, IRouter router)
        {
            StartPoint = source;
            EndPoint = destination;
            Router = router;
        }

        #endregion

        #region Public Methods

        public void UpdateConnectionPoints(Point startPoint, Point endPoint)
        {
            Data = new PointCollection(Router.CalculateGeometry(startPoint, endPoint));
            OnPropertyChanged("Data");
        }

        #endregion

        #region Private Methods

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region IConnection Members

        public IConnectionPoint StartPoint { get; private set; }
        public IConnectionPoint EndPoint { get; private set; }
        public IRouter Router { get; private set; }
        public PointCollection Data { get; private set; }

        public bool IsSelected { get; set; }

        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}