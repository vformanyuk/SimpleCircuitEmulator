using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using GraphView.Framework.Interfaces;
using GraphView.Infrastructure.Annotations;
using GraphView.Infrastructure.Interfaces;

namespace GraphView.Infrastructure.FrameworkDefaults
{
    public class Connector : IObservableConnector, INotifyPropertyChanged
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Connector"/> class.
        /// </summary>
        public Connector()
        {
        }

        public Connector(Func<Connector, bool> canConnect)
        {
            m_canConnect = canConnect;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Determines whether this instance can connect the specified connector.
        /// </summary>
        /// <param name="connector">The connector.</param>
        /// <returns></returns>
        public virtual bool CanConnect(Connector connector)
        {
            return m_canConnect == null || m_canConnect.Invoke(connector);
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
        private readonly Func<Connector, bool> m_canConnect;

        #endregion

        public void Input(double voltage)
        {
            var @event = OnVoltageChanged;
            if (@event != null)
            {
                @event(this, voltage);
            }
        }

        public event EventHandler<double> OnVoltageChanged;
    }
}