using System;
using System.Windows;
using System.Windows.Media.Media3D;

namespace Dimension3D.Core
{
    public sealed class DimensionGeometryModel3D : DimensionModel3D
    {
        private static Type _typeofThis = typeof(DimensionGeometryModel3D);
        public static readonly DependencyProperty GeometryProperty;
        static DimensionGeometryModel3D()
        {
            DefaultStyleKeyProperty.OverrideMetadata(_typeofThis, new FrameworkPropertyMetadata(_typeofThis));
            GeometryProperty = DependencyProperty.Register(nameof(Geometry), typeof(GeometryModel3D), _typeofThis, new FrameworkPropertyMetadata());
        }



        public GeometryModel3D Geometry { get => (GeometryModel3D)GetValue(GeometryProperty); set => SetValue(GeometryProperty, value); }



    }
}
