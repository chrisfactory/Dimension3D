using System;
using System.Windows;
using System.Windows.Controls;

namespace Dimension3D.Core
{
    internal class VisualItems : ItemsControl
    {
        private static readonly Type _typeofThis = typeof(VisualItems);
        static VisualItems()
        {
            DefaultStyleKeyProperty.OverrideMetadata(_typeofThis, new FrameworkPropertyMetadata(_typeofThis));
        }
    }
}
