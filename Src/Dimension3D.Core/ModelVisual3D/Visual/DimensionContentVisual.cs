using System;
using System.Windows;
using System.Windows.Media;

namespace Dimension3D.Core
{
    public class DimensionContentVisual : DimensionVisual3D
    {
        private static Type _typeofThis = typeof(DimensionContentVisual);
        public static readonly DependencyProperty VisualProperty;
        static DimensionContentVisual()
        {
            DefaultStyleKeyProperty.OverrideMetadata(_typeofThis, new FrameworkPropertyMetadata(_typeofThis));
            VisualProperty = DependencyProperty.Register(nameof(Visual), typeof(Visual), _typeofThis, new PropertyMetadata(null));
        }



        public Visual Visual { get => (Visual)GetValue(VisualProperty); set => SetValue(VisualProperty, value); } 
    }
}
