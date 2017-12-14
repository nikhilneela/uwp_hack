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
        Path _outerCirclePath;
        Path _innerCirclePath;
        Path _upperArcPath;
        Path _lowerArcPath;
        Grid _container;
        double _margin = 2;
        double _thickness = 8;
        Point _center;

        private const double RADIANS = Math.PI / 180;
        public SimpleControl()
        {
            this.DefaultStyleKey = typeof(SimpleControl);
            this.SizeChanged += SimpleControl_SizeChanged;
        }

        private void SimpleControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            drawRing();
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _outerCirclePath = GetTemplateChild("OuterCircle") as Path;
            _innerCirclePath = GetTemplateChild("InnerCircle") as Path;
            _upperArcPath = GetTemplateChild("UpperArc") as Path;
            _lowerArcPath = GetTemplateChild("LowerArc") as Path;
            _container = GetTemplateChild("EyeDropperContainer") as Grid;
            drawRing();
            
        }

        

        private void drawRing()
        {
            _center = new Point(_container.Width / 2, _container.Height / 2);

            double outerCircleRadius = (_container.Width - _margin) / 2;
            double innerCircleRadius = (_container.Width - _margin - _thickness) / 2;

            var oc = new EllipseGeometry();
            oc.Center = new Point(_center.X, _center.Y);
            oc.RadiusX = outerCircleRadius;
            oc.RadiusY = outerCircleRadius;


            var ic = new EllipseGeometry();
            ic.Center = new Point(_center.X, _center.Y);
            ic.RadiusX = innerCircleRadius;
            ic.RadiusY = innerCircleRadius;


            _outerCirclePath.Stroke = new SolidColorBrush(Colors.Black);
            _outerCirclePath.Data = oc;

            _innerCirclePath.Stroke = new SolidColorBrush(Colors.Black);
            _innerCirclePath.Data = ic;


            var la = GetCircleSegment(_center, innerCircleRadius, 90, SweepDirection.Counterclockwise);
            _lowerArcPath.Stroke = new SolidColorBrush(Colors.Blue);
            _lowerArcPath.StrokeThickness = 4;
            _lowerArcPath.Data = la;


            var ua = GetCircleSegment(_center, innerCircleRadius, 90, SweepDirection.Clockwise);
            _upperArcPath.Stroke = new SolidColorBrush(Colors.Green);
            _upperArcPath.StrokeThickness = 4;
            _upperArcPath.Data = ua;


        }

        public PathGeometry GetCircleSegment(Point centerPoint, double radius, double angle, SweepDirection direction)
        {
            var path = new Path();
            var pathGeometry = new PathGeometry();

            var circleStart = new Point(5, 50);

            var arcSegment = new ArcSegment
            {
                IsLargeArc = false,
                
                Point = ScaleUnitCirclePoint(centerPoint, angle, radius),
                Size = new Size(radius, radius),
                SweepDirection = direction
            };

            var pathFigure = new PathFigure
            {
                StartPoint = circleStart,
                IsClosed = false
            };

            pathFigure.Segments.Add(arcSegment);
            pathGeometry.Figures.Add(pathFigure);
            return pathGeometry;
        }

        private static Point ScaleUnitCirclePoint(Point origin, double angle, double radius)
        {
            return new Point(origin.X + Math.Sin(RADIANS * angle) * radius, origin.Y - Math.Cos(RADIANS * angle) * radius);
        }
    }
}
