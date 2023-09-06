using System;
using System.Windows;

namespace Dimension3D.Core
{
    public abstract class DimensionElement3D : FrameworkElement
    {
        public static Type _typeofThis = typeof(DimensionElement3D);
        static DimensionElement3D()
        {
            DefaultStyleKeyProperty.OverrideMetadata(_typeofThis, new FrameworkPropertyMetadata(_typeofThis));
        }
    }
}
