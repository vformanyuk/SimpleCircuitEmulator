using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using GraphView.Framework.Interfaces;

namespace GraphView.Framework.Controls
{
    public class NodeContainerControl : BaseNodeControl
    {
        #region Static fields

        public static readonly DependencyProperty NodeProperty = DependencyProperty.Register(
            "Node", typeof (INode), typeof (NodeContainerControl));

        #endregion

        static NodeContainerControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NodeContainerControl),
                new FrameworkPropertyMetadata(typeof(NodeContainerControl)));
        }

        #region Constructors

        public NodeContainerControl(INode node)
        {
            SetBinding(XProperty, new Binding("X") { Source = node, Mode = BindingMode.TwoWay});
            SetBinding(YProperty, new Binding("Y") { Source = node, Mode = BindingMode.TwoWay });
            Node = node;
            DataContext = node;
        }

        #endregion

        #region Public properties

        public INode Node
        {
            get { return (INode) GetValue(NodeProperty); }
            set { SetValue(NodeProperty, value); }
        }

        #endregion
    }
}