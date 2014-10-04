using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace GraphView.Framework.Controls
{
    public class SelectionRect : Control
    {

        #region Constructors

        static SelectionRect()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof (SelectionRect),
                new FrameworkPropertyMetadata(typeof (SelectionRect)));
        }

        public SelectionRect(double left, double bottom)
        {
            Canvas.SetLeft(this, left);
            Canvas.SetBottom(this, bottom);
            Width = 0;
            Height = 0;
            SelectionStartPoint = new Point(left, bottom);
        }

        #endregion

        #region Public properties

        public Point SelectionStartPoint { get; private set; }

        #endregion
    }
}