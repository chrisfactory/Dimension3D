using System;
using System.ComponentModel;
using System.Windows;

namespace Dimension3D.Core
{
    [DefaultEvent("Click")]
    [Localizability(LocalizationCategory.Button)]
    public class DimensionButton3D : DimensionButtonBase3D
    {
        private static readonly Type _typeofThis = typeof(DimensionButton3D);
        static DimensionButton3D()
        {
            DefaultStyleKeyProperty.OverrideMetadata(_typeofThis, new FrameworkPropertyMetadata(_typeofThis));
        }

    }
}
