using System;
using System.Windows;
using System.Windows.Media.Media3D;

namespace Dimension3D.Core
{
    public abstract class DimensionGeometryProvider : DimensionGeometryModel3D
    {
        private static Type _typeofThis = typeof(DimensionGeometryProvider);
        public static readonly DependencyProperty MaterialProperty;
        public static readonly DependencyProperty BackMaterialProperty;

        static DimensionGeometryProvider()
        {
            BackMaterialProperty = DependencyProperty.Register(nameof(BackMaterial), typeof(Material), _typeofThis, new FrameworkPropertyMetadata<DimensionGeometryProvider>(OnMaterialsPropertyChangedCallback));
            MaterialProperty = DependencyProperty.Register(nameof(Material), typeof(Material), _typeofThis, new FrameworkPropertyMetadata<DimensionGeometryProvider>(OnMaterialsPropertyChangedCallback));

        }



        protected DimensionGeometryProvider()
        {
            InvalidateGeometry();
        }

        public Material Material { get => (Material)GetValue(MaterialProperty); set => SetValue(MaterialProperty, value); }
        public Material BackMaterial { get => (Material)GetValue(BackMaterialProperty); set => SetValue(BackMaterialProperty, value); }

        protected void InvalidateGeometry()
        {
            this.Model.Geometry = ProvideMesh();
        }

        protected abstract MeshGeometry3D ProvideMesh();

        private static void OnMaterialsPropertyChangedCallback(DimensionGeometryProvider d, DependencyPropertyChangedEventArgs e)
        {
            if (e.Property == MaterialProperty)
                d.Model.Material = e.NewValue as Material;
            if (e.Property == BackMaterialProperty)
                d.Model.BackMaterial = e.NewValue as Material;
        }
    }
}
