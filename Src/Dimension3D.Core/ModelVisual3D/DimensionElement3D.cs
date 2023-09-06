using System;
using System.Windows;
using System.Windows.Media.Media3D;

namespace Dimension3D.Core
{
    public abstract class DimensionElement3D : FrameworkElement
    {
        private static Type _typeofThis = typeof(DimensionElement3D);
        public static readonly DependencyProperty TransformProperty;
        static DimensionElement3D()
        {
            DefaultStyleKeyProperty.OverrideMetadata(_typeofThis, new FrameworkPropertyMetadata(_typeofThis));
            TransformProperty = DependencyProperty.Register(nameof(Transform), typeof(int), _typeofThis, new PropertyMetadata(null));

        }
        public Transform3D Transform { get => (Transform3D)GetValue(TransformProperty); set => SetValue(TransformProperty, value); }
    }
}
