using System;
using System.Windows;
using System.Windows.Media.Media3D;

namespace Dimension3D.Core
{
    public class DimensionGeometryPlan : DimensionGeometryModel3D
    {
        private static Type _typeofThis = typeof(DimensionGeometryPlan);
        public static readonly DependencyProperty SideProperty;
        public static readonly DependencyProperty LocationProperty;
        public static readonly DependencyProperty SizeProperty;

        static DimensionGeometryPlan()
        {
            SideProperty = DependencyProperty.Register(nameof(Side), typeof(PlanSides), _typeofThis, new FrameworkPropertyMetadata<DimensionGeometryPlan>(PlanSides.Front,PlanPropertyChangedCallback));
            LocationProperty = DependencyProperty.Register(nameof(Location), typeof(Point3D), _typeofThis, new FrameworkPropertyMetadata<DimensionGeometryPlan>(PlanPropertyChangedCallback));
            SizeProperty = DependencyProperty.Register(nameof(Size), typeof(Size), _typeofThis, new FrameworkPropertyMetadata<DimensionGeometryPlan>(new Size(1,1),PlanPropertyChangedCallback));
        }


        public PlanSides Side { get => (PlanSides)GetValue(SideProperty); set => SetValue(SideProperty, value); } 
        public Point3D Location { get => (Point3D)GetValue(LocationProperty); set => SetValue(LocationProperty, value); } 
        public Size Size { get => (Size)GetValue(SizeProperty); set => SetValue(SizeProperty, value); }

       
       


        private static void PlanPropertyChangedCallback(DimensionGeometryPlan d, DependencyPropertyChangedEventArgs e) => d.InvalidateGeometry();


        protected override MeshGeometry3D ProvideMesh()
        {
           
            var mesh = new MeshGeometry3D();
            MeshBuilder.CreatePlan(mesh, Location, Size, Side);
            return mesh;

        }
    }
}
