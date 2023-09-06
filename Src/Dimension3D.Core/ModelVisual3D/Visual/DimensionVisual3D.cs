using System;
using System.Windows;

namespace Dimension3D.Core
{
    public abstract class DimensionVisual3D : DimensionElement3D
    {
        public static Type _typeofThis = typeof(DimensionVisual3D);
        static DimensionVisual3D()
        {
            DefaultStyleKeyProperty.OverrideMetadata(_typeofThis, new FrameworkPropertyMetadata(_typeofThis));
        }
    }
}
