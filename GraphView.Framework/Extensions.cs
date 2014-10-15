using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using GraphView.Framework.Interfaces;

namespace GraphView.Framework
{
    public static class Extensions
    {
        #region Public Methods

        public static IList<T> AreaHitTest<T>(this FrameworkElement element, Point startPoint, double width, double height)
            where T : class
        {
            var result = new List<T>();

            var hitTestParams = new GeometryHitTestParameters(new RectangleGeometry(new Rect(startPoint, new Size(width, height))));
            var resultCallback = new HitTestResultCallback(t => HitTestResultBehavior.Continue);
            var filterCallback = new HitTestFilterCallback(t =>
            {
                result.Add(GetParent<T>(t as FrameworkElement));
                return HitTestFilterBehavior.Continue;
            });

            VisualTreeHelper.HitTest(element, filterCallback, resultCallback, hitTestParams);

            return result.Where(c => c != null).Distinct().ToList();
        }

        public static T AreaHitTest<T>(this FrameworkElement element, Point center, double radius)
            where T : class
        {
            T hitTestResult = null;
            var hitTestParams = new GeometryHitTestParameters(new EllipseGeometry(center, radius, radius));
            var resultCallback = new HitTestResultCallback(t => HitTestResultBehavior.Continue);
            var filterCallback = new HitTestFilterCallback(t =>
            {
                hitTestResult = GetParent<T>(t as FrameworkElement);
                return HitTestFilterBehavior.Continue;
            });

            VisualTreeHelper.HitTest(element, filterCallback, resultCallback, hitTestParams);

            return hitTestResult;
        }

        public static T GetParent<T>(this FrameworkElement element) where T : class
        {
            if (element == null)
            {
                return null;
            }

            var parent = (element.Parent ?? element.TemplatedParent) as FrameworkElement;
            while (parent != null)
            {
                var testParent = parent as T;
                if (testParent != null)
                {
                    return testParent;
                }

                parent = (parent.Parent ?? parent.TemplatedParent) as FrameworkElement;
            }

            return null;
        }

        public static T HitTest<T>(this FrameworkElement element, Point position) where T : class
        {
            var hittest = element.InputHitTest(position);
            if (hittest == null) return null;

            return GetParent<T>(hittest as FrameworkElement);
        }

        #endregion
    }
}