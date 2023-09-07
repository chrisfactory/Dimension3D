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
            TransformProperty = DependencyProperty.Register(nameof(Transform), typeof(Transform3D), _typeofThis, new FrameworkPropertyMetadata<DimensionElement3D>(TransformPropertyChangedCallback));
        }

        public Transform3D Transform { get => (Transform3D)GetValue(TransformProperty); set => SetValue(TransformProperty, value); }


        private static void TransformPropertyChangedCallback(DimensionElement3D d, DependencyPropertyChangedEventArgs e)
        {
            d.OnApplyTransform();
        }
        protected virtual void OnApplyTransform() { }
    }
}
