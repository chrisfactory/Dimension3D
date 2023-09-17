using Dimension3D.Core.Tools;
using System;
using System.Windows;
using System.Windows.Media.Media3D;

namespace Dimension3D.Core
{
    public abstract class DimensionGeometryModel3D : FrameworkElement
    {
        private static Type _typeofThis = typeof(DimensionGeometryModel3D);
        public static readonly DependencyPropertyKey GeometryPropertyKey;
        public static readonly DependencyProperty GeometryProperty;
        static DimensionGeometryModel3D()
        {
            GeometryPropertyKey = DependencyProperty.RegisterReadOnly(nameof(Geometry), typeof(MeshGeometry3D), _typeofThis, new FrameworkPropertyMetadata());
            GeometryProperty = GeometryPropertyKey.DependencyProperty;
        }
        public DimensionGeometryModel3D()
        {
            InvalidateGeometry();
        }

        public MeshGeometry3D Geometry { get => (MeshGeometry3D)GetValue(GeometryProperty); private set => SetValue(GeometryPropertyKey, value); }





        protected virtual void InvalidateGeometry()
        {
            Geometry = ProvideMesh();
        }

        protected abstract MeshGeometry3D ProvideMesh();

    }


    public class GeometryProvider : DependencyObject
    {
        private static Type _typeofThis = typeof(DimensionGeometryModel3D);
        public static readonly DependencyProperty ProviderProperty;

        static GeometryProvider()
        {
            ProviderProperty = DependencyProperty.RegisterAttached("Provider", typeof(DimensionGeometryModel3D), _typeofThis, new FrameworkPropertyMetadata<GeometryModel3D>(ProviderPropertyChangedCallback));
        }



        public static DimensionGeometryModel3D GetProvider(GeometryModel3D obj) => (DimensionGeometryModel3D)obj.GetValue(ProviderProperty);
        public static void SetProvider(GeometryModel3D obj, DimensionGeometryModel3D value) => obj.SetValue(ProviderProperty, value);


        private static void ProviderPropertyChangedCallback(GeometryModel3D d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is DimensionGeometryModel3D geometry)
                d.SetBindingTo(GeometryModel3D.GeometryProperty, DimensionGeometryModel3D.GeometryProperty, geometry);
        }
    }
}
