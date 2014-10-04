using System.Windows;
using System.Windows.Data;
using GraphView.Framework.Converters;
using GraphView.Framework.Interfaces;

namespace GraphView.Framework.Controls
{
    public sealed class ConnectorControl : BaseNodeControl
    {
        #region Static fields

        public static readonly DependencyProperty ConnectionPointProperty = DependencyProperty.Register(
            "ConnectionPoint", typeof (IConnectionPoint), typeof (ConnectorControl));

        #endregion

        #region Constructors

        static ConnectorControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof (ConnectorControl),
                new FrameworkPropertyMetadata(typeof (ConnectorControl)));
        }

        public ConnectorControl()
        {
            Loaded += ConnectorControl_Loaded;
        }

        private void ConnectorControl_Loaded(object sender, RoutedEventArgs e)
        {
            var parentNode = this.GetParent<BaseNodeControl>();
            if (parentNode == null)
            {
                return;
            }

            var offset = TranslatePoint(new Point(0, 0), parentNode);
            m_currentControlOffest = new Point(offset.X + ActualWidth / 2, offset.Y + ActualHeight / 2);

            SetBinding(XProperty,
                new Binding("X")
                {
                    Source = parentNode, 
                    Converter = new OffsetConverter(),
                    ConverterParameter = m_currentControlOffest.X
                });

            SetBinding(YProperty,
                new Binding("Y")
                {
                    Source = parentNode,
                    Converter = new OffsetConverter(),
                    ConverterParameter = m_currentControlOffest.Y
                });
        }

        #endregion

        #region Public properties

        public IConnectionPoint ConnectionPoint
        {
            get { return (IConnectionPoint) GetValue(ConnectionPointProperty); }
            set { SetValue(ConnectionPointProperty, value); }
        }

        #endregion

        private Point m_currentControlOffest;
    }
}