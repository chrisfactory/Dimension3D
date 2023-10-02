using System;
using System.Windows;
using System.Windows.Media.Media3D;

namespace Dimension3D.Core
{
    public class DimensionGeometryRegularPolygon : DimensionGeometryModel3D
    {
        private static Type _typeofThis = typeof(DimensionGeometryRegularPolygon);
        public static readonly DependencyProperty SideProperty;
        public static readonly DependencyProperty LocationProperty;
        public static readonly DependencyProperty RadiusProperty;
        public static readonly DependencyProperty SidesProperty;
        static DimensionGeometryRegularPolygon()
        {
            SideProperty = DependencyProperty.Register(nameof(Side), typeof(PlanSides), _typeofThis, new FrameworkPropertyMetadata<DimensionGeometryRegularPolygon>(PlanSides.Front, PropertyChangedCallback));
            LocationProperty = DependencyProperty.Register(nameof(Location), typeof(Point3D), _typeofThis, new FrameworkPropertyMetadata<DimensionGeometryRegularPolygon>(PropertyChangedCallback));
            RadiusProperty = DependencyProperty.Register(nameof(Radius), typeof(double), _typeofThis, new FrameworkPropertyMetadata<DimensionGeometryRegularPolygon>(0.5,PropertyChangedCallback));
            SidesProperty = DependencyProperty.Register(nameof(Sides), typeof(int), _typeofThis, new FrameworkPropertyMetadata<DimensionGeometryRegularPolygon>(5, PropertyChangedCallback));
        }

        public PlanSides Side { get => (PlanSides)GetValue(SideProperty); set => SetValue(SideProperty, value); }
        public Point3D Location { get => (Point3D)GetValue(LocationProperty); set => SetValue(LocationProperty, value); }
        public double Radius { get => (double)GetValue(RadiusProperty); set => SetValue(RadiusProperty, value); }
        public int Sides { get => (int)GetValue(SidesProperty); set => SetValue(SidesProperty, value); }

        private static void PropertyChangedCallback(DimensionGeometryRegularPolygon d, DependencyPropertyChangedEventArgs e) => d.InvalidateGeometry();


        protected override MeshGeometry3D ProvideMesh()
        { 
            var mesh = new MeshGeometry3D();
            MeshBuilder.CreateRegularPolygon(mesh, Location, Sides, Radius, Side);
            return mesh;

        }
    }
}
