using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using GraphView.Framework.Interfaces;

namespace GraphView.Framework.Controls
{
    public class ConnectionContainerControl : ContentControl
    {
        #region Static fields

        public static readonly DependencyProperty ConnectionProperty = DependencyProperty.Register(
            "Connection", typeof (IConnection), typeof (ConnectionContainerControl),
            new PropertyMetadata(default(IConnection)));

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes the <see cref="ConnectionContainerControl"/> class.
        /// </summary>
        static ConnectionContainerControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof (ConnectionContainerControl),
                new FrameworkPropertyMetadata(typeof (ConnectionContainerControl)));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionContainerControl"/> class.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="connection">The connection.</param>
        /// <param name="noneVirtual">If connection is virtual it should be excluded from hit test.</param>
        public ConnectionContainerControl(BaseNodeControl source, BaseNodeControl destination, IConnection connection, bool noneVirtual = true)
        {
            var xprop = DependencyPropertyDescriptor.FromProperty(BaseNodeControl.XProperty, typeof (ConnectorControl));
            if (xprop != null)
            {
                xprop.AddValueChanged(source, PositionChanged);
                xprop.AddValueChanged(destination, PositionChanged);
            }

            var yprop = DependencyPropertyDescriptor.FromProperty(BaseNodeControl.YProperty, typeof (ConnectorControl));
            if (yprop != null)
            {
                yprop.AddValueChanged(source, PositionChanged);
                yprop.AddValueChanged(destination, PositionChanged);
            }

            IsHitTestVisible = noneVirtual;

            m_source = source;
            m_destination = destination;
            Connection = connection;
            DataContext = connection;
            UpdateData();
        }

        #endregion

        #region Public properties

        public IConnection Connection
        {
            get { return (IConnection) GetValue(ConnectionProperty); }
            set { SetValue(ConnectionProperty, value); }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Positions the changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void PositionChanged(object sender, EventArgs e)
        {
            UpdateData();
        }

        /// <summary>
        /// Updates the data.
        /// </summary>
        private void UpdateData()
        {
            // TODO: change container default left and top from NaN to proper X and Y
            Connection.UpdateConnectionPoints(new Point(m_source.X, m_source.Y), new Point(m_destination.X, m_destination.Y));
        }

        #endregion

        #region Private fields

        private readonly BaseNodeControl m_destination;
        private readonly BaseNodeControl m_source;

        #endregion

        /// <summary>
        /// Gets the source connector.
        /// </summary>
        internal BaseNodeControl Source
        {
            get { return m_source; }
        }

        /// <summary>
        /// Gets the destination connector.
        /// </summary>
        internal BaseNodeControl Destination
        {
            get { return m_destination; }
        }
    }
}