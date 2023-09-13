using Dimension3D.Core.Tools;
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
            BackMaterialProperty = DependencyProperty.Register(nameof(BackMaterial), typeof(Material), _typeofThis, new FrameworkPropertyMetadata());
            MaterialProperty = DependencyProperty.Register(nameof(Material), typeof(Material), _typeofThis, new FrameworkPropertyMetadata());

        }



        protected DimensionGeometryProvider()
        {
            InvalidateGeometry();
            Model.SetBindingTo(GeometryModel3D.MaterialProperty, DimensionGeometryProvider.MaterialProperty, this);
            Model.SetBindingTo(GeometryModel3D.BackMaterialProperty, DimensionGeometryProvider.BackMaterialProperty, this);
        }

        public Material Material { get => (Material)GetValue(MaterialProperty); set => SetValue(MaterialProperty, value); }
        public Material BackMaterial { get => (Material)GetValue(BackMaterialProperty); set => SetValue(BackMaterialProperty, value); }

        protected void InvalidateGeometry()
        {
            this.Model.Geometry = ProvideMesh();
        }

        protected abstract MeshGeometry3D ProvideMesh();
     

    }
}
