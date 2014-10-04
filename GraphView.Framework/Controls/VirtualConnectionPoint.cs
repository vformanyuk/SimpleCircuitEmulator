using System.Windows;
using System.Windows.Controls;
using GraphView.Framework.Interfaces;

namespace GraphView.Framework.Controls
{
    internal sealed class VirtualConnectionPoint : BaseNodeControl
    {
        #region Constructors

        static VirtualConnectionPoint()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof (VirtualConnectionPoint),
                new FrameworkPropertyMetadata(typeof (VirtualConnectionPoint)));

            XProperty.OverrideMetadata(typeof (VirtualConnectionPoint),
                new PropertyMetadata(
                    (o, a) => Canvas.SetLeft((UIElement) o, ((double) a.NewValue) - Constants.VirtualPointXOffset)));
            YProperty.OverrideMetadata(typeof (VirtualConnectionPoint),
                new PropertyMetadata(
                    (o, a) => Canvas.SetTop((UIElement) o, ((double) a.NewValue) - Constants.VirtualPointYOffset)));
        }

        public VirtualConnectionPoint(ConnectorControl sourceConnectionPoint)
        {
            SourceConnectionPoint = sourceConnectionPoint.ConnectionPoint;
            SourceConnectorControl = sourceConnectionPoint;
        }

        #endregion

        #region Public properties

        public IConnectionPoint SourceConnectionPoint { get; private set; }

        public ConnectorControl SourceConnectorControl { get; private set; }

        #endregion
    }
}