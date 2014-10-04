using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using GraphView.Framework.Interfaces;
using Point = System.Windows.Point;

namespace GraphView.Framework.Controls
{
    public class ConnectionContainerControl : ContentControl
    {
        public static readonly DependencyProperty DataProperty = DependencyProperty.Register(
            "Data", typeof (PointCollection), typeof (ConnectionContainerControl), new PropertyMetadata(default(PointCollection)));

        public PointCollection Data
        {
            get { return (PointCollection) GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        #region Constructors

        static ConnectionContainerControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof (ConnectionContainerControl),
                new FrameworkPropertyMetadata(typeof (ConnectionContainerControl)));
        }

        public ConnectionContainerControl(BaseNodeControl source, BaseNodeControl destination, IConnection connection)
        {
            var xprop = DependencyPropertyDescriptor.FromProperty(BaseNodeControl.XProperty, typeof (ConnectorControl));
            if (xprop != null)
            {
                xprop.AddValueChanged(source, PositionChanged);
                xprop.AddValueChanged(destination, PositionChanged);
            }

            var yprop = DependencyPropertyDescriptor.FromProperty(BaseNodeControl.YProperty, typeof(ConnectorControl));
            if (yprop != null)
            {
                yprop.AddValueChanged(source, PositionChanged);
                yprop.AddValueChanged(destination, PositionChanged);
            }

            m_router = connection.Router;
            m_source = source;
            m_destination = destination;
            DataContext = connection;
            UpdateData();
        }

        private void PositionChanged(object sender, EventArgs e)
        {
            UpdateData();
        }

        private void UpdateData()
        {
            Data = new PointCollection(m_router.CalculateGeometry(new Point(m_source.X, m_source.Y),
                                                                  new Point(m_destination.X, m_destination.Y)));
        }

        #endregion

        private readonly IRouter m_router;
        private readonly BaseNodeControl m_source, m_destination;
    }
}