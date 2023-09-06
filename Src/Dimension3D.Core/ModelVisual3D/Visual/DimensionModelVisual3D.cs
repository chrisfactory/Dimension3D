using System;
using System.Windows;

namespace Dimension3D.Core
{
    public class DimensionModelVisual3D : DimensionVisual3D
    {
        public static Type _typeofThis = typeof(DimensionModelVisual3D);
        static DimensionModelVisual3D()
        {
            DefaultStyleKeyProperty.OverrideMetadata(_typeofThis, new FrameworkPropertyMetadata(_typeofThis));
        }
    }
}
