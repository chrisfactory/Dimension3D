using System;
using System.Windows;
using System.Windows.Media.Media3D;

namespace Dimension3D.Core
{
    public class DimensionModelVisual3D : DimensionVisual3D
    {
        private static Type _typeofThis = typeof(DimensionModelVisual3D);
        static DimensionModelVisual3D()
        {
            DefaultStyleKeyProperty.OverrideMetadata(_typeofThis, new FrameworkPropertyMetadata(_typeofThis));
        }

        public DimensionModelVisual3D()
        {

        }
    }
}
