using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

// The Templated Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234235

namespace SimpleCustomControl
{
    public sealed class SimpleControl : Control
    {
        Path _ring;
        public SimpleControl()
        {
            this.DefaultStyleKey = typeof(SimpleControl);
        }

        protected override void OnApplyTemplate()
        {
            _ring = this.GetTemplateChild("Ring") as Path;
            drawRing();
            base.OnApplyTemplate();
        }

        private void drawRing()
        {


           
            _ring.Fill = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 204, 204, 255));
            _ring.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
            _ring.StrokeThickness = 1;

            var geometryGroup1 = new GeometryGroup();
            var rectangleGeometry1 = new RectangleGeometry();
            rectangleGeometry1.Rect = new Rect(50, 5, 100, 10);
            var rectangleGeometry2 = new RectangleGeometry();
            rectangleGeometry2.Rect = new Rect(5, 5, 95, 180);
            geometryGroup1.Children.Add(rectangleGeometry1);
            geometryGroup1.Children.Add(rectangleGeometry2);

            var ellipseGeometry1 = new EllipseGeometry();
            ellipseGeometry1.Center = new Point(100, 100);
            ellipseGeometry1.RadiusX = 20;
            ellipseGeometry1.RadiusY = 30;
            geometryGroup1.Children.Add(ellipseGeometry1);

            var pathGeometry1 = new PathGeometry();
            var pathFigureCollection1 = new PathFigureCollection();
            var pathFigure1 = new PathFigure();
            pathFigure1.IsClosed = true;
            pathFigure1.StartPoint = new Windows.Foundation.Point(50, 50);
            pathFigureCollection1.Add(pathFigure1);
            pathGeometry1.Figures = pathFigureCollection1;

            var pathSegmentCollection1 = new PathSegmentCollection();
            var pathSegment1 = new BezierSegment();
            pathSegment1.Point1 = new Point(75, 300);
            pathSegment1.Point2 = new Point(125, 100);
            pathSegment1.Point3 = new Point(150, 50);
            pathSegmentCollection1.Add(pathSegment1);

            var pathSegment2 = new BezierSegment();
            pathSegment2.Point1 = new Point(125, 300);
            pathSegment2.Point2 = new Point(75, 100);
            pathSegment2.Point3 = new Point(50, 50);
            pathSegmentCollection1.Add(pathSegment2);
            pathFigure1.Segments = pathSegmentCollection1;

            geometryGroup1.Children.Add(pathGeometry1);
            //_ring.Data = geometryGroup1;

            // When you create a XAML element in code, you have to add
            // it to the XAML visual tree. This example assumes you have
            // a panel named 'layoutRoot' in your XAML file, like this:
            // <Grid x:Name="layoutRoot>
            //layoutRoot.Children.Add(_ring);

        }
    }
}
