using GraphView.Framework.Interfaces;
using GraphView.Framework.Routers;
using GraphView.Infrastructure.Interfaces;

namespace GraphView.Infrastructure.FrameworkDefaults
{
    public class ConductingConnection : Connection
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ConductingConnection"/> class.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        public ConductingConnection(IObservableConnector source, IObservableConnector destination)
            : this(source, destination, new DirectLineRouter())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConductingConnection"/> class.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="router">The router.</param>
        public ConductingConnection(IObservableConnector source, IObservableConnector destination, IRouter router)
            : base(source,destination,router)
        {
            StartPoint = source;
            EndPoint = destination;
            Router = router;

            source.OnVoltageChanged += InputVoltageChanged;
        }

        #endregion

        #region Private Methods

        private void InputVoltageChanged(object sender, double e)
        {
            ((IObservableConnector)EndPoint).Input(e);
        }

        #endregion
    }
}