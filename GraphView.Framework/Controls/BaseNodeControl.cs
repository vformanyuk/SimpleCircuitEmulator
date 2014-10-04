using System.Windows;
using System.Windows.Controls;

namespace GraphView.Framework.Controls
{
    public class BaseNodeControl : ContentControl
    {
        #region Static fields

        public static readonly DependencyProperty XProperty = DependencyProperty.Register(
            "X", typeof (double), typeof (NodeContainerControl));

        public static readonly DependencyProperty YProperty = DependencyProperty.Register(
            "Y", typeof (double), typeof (NodeContainerControl));

        #endregion

        #region Public properties

        public double Y
        {
            get { return (double) GetValue(YProperty); }
            set { SetValue(YProperty, value); }
        }

        public double X
        {
            get { return (double) GetValue(XProperty); }
            set { SetValue(XProperty, value); }
        }

        #endregion
    }
}