using System;
using System.Windows;
using System.Windows.Media.Media3D;

namespace Dimension3D.Core
{
    public sealed class DimensionGeometry : DimensionGeometryProvider
    {
        private static Type _typeofThis = typeof(DimensionGeometry);
        public static readonly DependencyProperty GeometryProperty;
        static DimensionGeometry()
        {
            GeometryProperty = DependencyProperty.Register(nameof(Geometry), typeof(MeshGeometry3D), _typeofThis, new FrameworkPropertyMetadata<DimensionGeometry>(OnGeometryPropertyChangedCallback));
        }


        public MeshGeometry3D Geometry { get => (MeshGeometry3D)GetValue(GeometryProperty); set => SetValue(GeometryProperty, value); }



        protected override MeshGeometry3D ProvideMesh() => Geometry;
        private static void OnGeometryPropertyChangedCallback(DimensionGeometry d, DependencyPropertyChangedEventArgs e) => d.InvalidateGeometry();

    }
}