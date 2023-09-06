using System;
using System.Windows;

namespace Dimension3D.Core
{
    public abstract class DimensionGeometryModel3D : DimensionModel3D
    {
        private static Type _typeofThis = typeof(DimensionGeometryModel3D);
        static DimensionGeometryModel3D()
        {
            DefaultStyleKeyProperty.OverrideMetadata(_typeofThis, new FrameworkPropertyMetadata(_typeofThis));
        }
    }
}
