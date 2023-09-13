using System;
using System.Windows;
using System.Windows.Media;

namespace Dimension3D.Core
{
    public sealed class DimensionContentVisual : DimensionVisual3D
    {
        private static Type _typeofThis = typeof(DimensionContentVisual);
        public static readonly DependencyProperty VisualProperty;
        public static readonly DependencyProperty GeometryModelProperty;
        static DimensionContentVisual()
        {
            DefaultStyleKeyProperty.OverrideMetadata(_typeofThis, new FrameworkPropertyMetadata(_typeofThis));
            VisualProperty = DependencyProperty.Register(nameof(Visual), typeof(Visual), _typeofThis, new FrameworkPropertyMetadata());
            GeometryModelProperty = DependencyProperty.Register(nameof(GeometryModel), typeof(DimensionGeometryModel3D), _typeofThis, new FrameworkPropertyMetadata());
        }



        public Visual Visual { get => (Visual)GetValue(VisualProperty); set => SetValue(VisualProperty, value); } 
        public DimensionGeometryModel3D GeometryModel { get => (DimensionGeometryModel3D)GetValue(GeometryModelProperty); set => SetValue(GeometryModelProperty, value); }
         

    }
}
