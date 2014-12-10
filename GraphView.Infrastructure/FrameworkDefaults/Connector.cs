using System.ComponentModel;
using System.Runtime.CompilerServices;
using GraphView.Framework.Interfaces;
using GraphView.Infrastructure.Annotations;

namespace GraphView.Infrastructure.FrameworkDefaults
{
    public class Connector : IConnectionPoint, INotifyPropertyChanged
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Connector"/> class.
        /// </summary>
        /// <param name="hostingElement">The hosting element.</param>
        /// <param name="type">The type.</param>
        public Connector(CircuitElement hostingElement, ConnectorType type)
        {
            Type = type;
            Element = hostingElement;
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Gets element that current connector attached to.
        /// </summary>
        public CircuitElement Element { get; private set; }
        /// <summary>
        /// Gets connector type.
        /// </summary>
        public ConnectorType Type { get; private set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Determines whether this instance can connect the specified connector.
        /// </summary>
        /// <param name="connector">The connector.</param>
        /// <returns></returns>
        public virtual bool CanConnect(Connector connector)
        {
            return Type == ConnectorType.Output && connector.Type == ConnectorType.Output;
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

        #region IConnectionPoint Members

        /// <summary>
        /// Gets or sets a value indicating whether this instance is connected.
        /// </summary>
        public virtual bool IsConnected
        {
            get { return m_isConnected; }
            set
            {
                if (m_isConnected == value) return;
                m_isConnected = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Determines whether this instance can connect the specified connection point.
        /// </summary>
        /// <param name="connectionPoint">The connection point.</param>
        /// <returns></returns>
        bool IConnectionPoint.CanConnect(IConnectionPoint connectionPoint)
        {
            return CanConnect(connectionPoint as Connector);
        }

        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Private fields

        private bool m_isConnected;

        #endregion
    }
}