using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;
using GraphView.Framework.Interfaces;
using GraphView.Framework.Routers;
using GraphView.Infrastructure.Annotations;

namespace GraphView.Infrastructure.FrameworkDefaults
{
    public class Connection : IConnection, INotifyPropertyChanged
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ConductingConnection"/> class.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        public Connection(IConnectionPoint source, IConnectionPoint destination)
            : this(source, destination, new DirectLineRouter())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConductingConnection"/> class.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="router">The router.</param>
        public Connection(IConnectionPoint source, IConnectionPoint destination, IRouter router)
        {
            StartPoint = source;
            EndPoint = destination;
            Router = router;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Updates the connection points.
        /// Used by container control to mainatin data consistency between view(control) and view model(this type)
        /// </summary>
        /// <param name="startPoint">The start point.</param>
        /// <param name="endPoint">The end point.</param>
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

        /// <summary>
        /// Gets the start point.
        /// </summary>
        public IConnectionPoint StartPoint { get; protected set; }
        /// <summary>
        /// Gets the end point.
        /// </summary>
        public IConnectionPoint EndPoint { get; protected set; }
        /// <summary>
        /// Gets the router which calculates points for connection.
        /// </summary>
        public IRouter Router { get; protected set; }
        /// <summary>
        /// Gets the points collection which is used to render the connection.
        /// </summary>
        public PointCollection Data { get; protected set; }

        public bool IsSelected { get; set; }

        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}