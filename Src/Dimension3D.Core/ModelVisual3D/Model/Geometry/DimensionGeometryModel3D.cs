using System;
using System.Windows;
using System.Windows.Media.Media3D;

namespace Dimension3D.Core
{
    public class DimensionGeometryModel3D : DimensionModel3D
    {
        private static Type _typeofThis = typeof(DimensionGeometryModel3D);
        private static readonly DependencyProperty ModelProperty;
        static DimensionGeometryModel3D()
        {
            DefaultStyleKeyProperty.OverrideMetadata(_typeofThis, new FrameworkPropertyMetadata(_typeofThis));
            ModelProperty = DependencyProperty.Register(nameof(Model), typeof(GeometryModel3D), _typeofThis, new FrameworkPropertyMetadata());
        }

        public DimensionGeometryModel3D()
        {
            Model = new GeometryModel3D();
        }

        internal GeometryModel3D Model { get => (GeometryModel3D)GetValue(ModelProperty); private set => SetValue(ModelProperty, value); }

        internal override Model3D? GetModel() => Model;

    }
}
