using System;
using System.Windows;
using System.Windows.Media.Media3D;

namespace Dimension3D.Core
{
    public class DimensionGeometryArc : DimensionGeometryModel3D
    {
        private static Type _typeofThis = typeof(DimensionGeometryArc); 
        public static readonly DependencyProperty LocationProperty;
        public static readonly DependencyProperty StartAngleProperty;
        public static readonly DependencyProperty EndAngleProperty;
        public static readonly DependencyProperty RadiusProperty;
        public static readonly DependencyProperty VertexCountProperty;
        public static readonly DependencyProperty SizeProperty;
        static DimensionGeometryArc()
        {
            LocationProperty = DependencyProperty.Register(nameof(Location), typeof(Point3D), _typeofThis, new FrameworkPropertyMetadata<DimensionGeometryArc>(PropertyChangedCallback));
            StartAngleProperty = DependencyProperty.Register(nameof(StartAngle), typeof(double), _typeofThis, new FrameworkPropertyMetadata<DimensionGeometryArc>(0.0, PropertyChangedCallback));
            EndAngleProperty = DependencyProperty.Register(nameof(EndAngle), typeof(double), _typeofThis, new FrameworkPropertyMetadata<DimensionGeometryArc>(360.0, PropertyChangedCallback));
            RadiusProperty = DependencyProperty.Register(nameof(Radius), typeof(double), _typeofThis, new FrameworkPropertyMetadata<DimensionGeometryArc>(PropertyChangedCallback));
            VertexCountProperty = DependencyProperty.Register(nameof(VertexCount), typeof(int), _typeofThis, new FrameworkPropertyMetadata<DimensionGeometryArc>(60, PropertyChangedCallback));
            SizeProperty = DependencyProperty.Register(nameof(Width), typeof(double), _typeofThis, new FrameworkPropertyMetadata<DimensionGeometryArc>(10.0, PropertyChangedCallback));
        }

        public Point3D Location { get => (Point3D)GetValue(LocationProperty); set => SetValue(LocationProperty, value); }
        public double StartAngle { get => (double)GetValue(StartAngleProperty); set => SetValue(StartAngleProperty, value); }
        public double EndAngle { get => (double)GetValue(EndAngleProperty); set => SetValue(EndAngleProperty, value); }
        public double Radius { get => (double)GetValue(RadiusProperty); set => SetValue(RadiusProperty, value); }
        public int VertexCount { get => (int)GetValue(VertexCountProperty); set => SetValue(VertexCountProperty, value); }
        public double Size { get => (double)GetValue(SizeProperty); set => SetValue(SizeProperty, value); }





        private static void PropertyChangedCallback(DimensionGeometryArc d, DependencyPropertyChangedEventArgs e) => d.InvalidateGeometry();

        protected override MeshGeometry3D ProvideMesh()
        {

            var mesh = new MeshGeometry3D();
            MeshBuilder.CreateArc(mesh, Location, StartAngle, EndAngle, Radius, Size, VertexCount);

            return mesh;

        }
    }
}
