using System;
using System.Windows;
using System.Windows.Media.Media3D;

namespace Dimension3D.Core
{
    public class DimensionGeometryOctagon : DimensionGeometryModel3D
    {
        private static Type _typeofThis = typeof(DimensionGeometryOctagon);
        public static readonly DependencyProperty SideProperty;
        public static readonly DependencyProperty LocationProperty;
        public static readonly DependencyProperty SizeProperty;
        public static readonly DependencyProperty CornerSizeProperty;

        static DimensionGeometryOctagon()
        {
            SideProperty = DependencyProperty.Register(nameof(Side), typeof(PlanSides), _typeofThis, new FrameworkPropertyMetadata<DimensionGeometryOctagon>(PlanSides.Front, OctagonPropertyChangedCallback));
            LocationProperty = DependencyProperty.Register(nameof(Location), typeof(Point3D), _typeofThis, new FrameworkPropertyMetadata<DimensionGeometryOctagon>(OctagonPropertyChangedCallback));
            SizeProperty = DependencyProperty.Register(nameof(Size), typeof(Size), _typeofThis, new FrameworkPropertyMetadata<DimensionGeometryOctagon>(new Size(1,1),OctagonPropertyChangedCallback));
            CornerSizeProperty = DependencyProperty.Register(nameof(CornerSize), typeof(Size), _typeofThis, new FrameworkPropertyMetadata<DimensionGeometryOctagon>(new Size(0.2, 0.2), OctagonPropertyChangedCallback));
        }

        public PlanSides Side { get => (PlanSides)GetValue(SideProperty); set => SetValue(SideProperty, value); }
        public Point3D Location { get => (Point3D)GetValue(LocationProperty); set => SetValue(LocationProperty, value); } 
        public Size Size { get => (Size)GetValue(SizeProperty); set => SetValue(SizeProperty, value); } 
        public Size CornerSize { get => (Size)GetValue(CornerSizeProperty); set => SetValue(CornerSizeProperty, value); }

      
       




        private static void OctagonPropertyChangedCallback(DimensionGeometryOctagon d, DependencyPropertyChangedEventArgs e) => d.InvalidateGeometry();


        protected override MeshGeometry3D ProvideMesh()
        { 
            var mesh = new MeshGeometry3D();
            MeshBuilder.AddOctagonToMesh(mesh, Location, Size, CornerSize, Side);
            return mesh;

        }
    }
}
