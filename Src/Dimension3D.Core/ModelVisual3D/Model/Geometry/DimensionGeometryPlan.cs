using System;
using System.Windows;
using System.Windows.Media.Media3D;

namespace Dimension3D.Core
{
    public class DimensionGeometryPlan : DimensionGeometryModel3D
    {
        private static Type _typeofThis = typeof(DimensionGeometryPlan);
        public static readonly DependencyProperty SideProperty;

        static DimensionGeometryPlan()
        {
            SideProperty = DependencyProperty.Register(nameof(Side), typeof(PlanSides), _typeofThis, new FrameworkPropertyMetadata<DimensionGeometryPlan>(PlanPropertyChangedCallback));
        }


        public PlanSides Side { get => (PlanSides)GetValue(SideProperty); set => SetValue(SideProperty, value); }



        private static void PlanPropertyChangedCallback(DimensionGeometryPlan d, DependencyPropertyChangedEventArgs e) => d.InvalidateGeometry();


        protected override MeshGeometry3D ProvideMesh()
        {
            var center = new Point3D();
            var size = new Size3D(1, 1, 1);
            var mesh = new MeshGeometry3D();
            MeshBuilder.CreatePlan(mesh, center, size, Side);
            return mesh;

        }
    }
}
