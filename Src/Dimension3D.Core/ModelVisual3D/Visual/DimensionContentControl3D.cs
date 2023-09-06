using System;
using System.Windows;

namespace Dimension3D.Core
{
    public class DimensionContentControl3D : DimensionVisual3D
    {
        public static Type _typeofThis = typeof(DimensionContentControl3D);
        static DimensionContentControl3D()
        {
            DefaultStyleKeyProperty.OverrideMetadata(_typeofThis, new FrameworkPropertyMetadata(_typeofThis));
        }
    }
}
