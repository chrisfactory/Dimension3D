using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;

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
            GeometryModelProperty = DependencyProperty.Register(nameof(GeometryModel), typeof(GeometryModel3D), _typeofThis, new FrameworkPropertyMetadata());
        }



        public Visual Visual { get => (Visual)GetValue(VisualProperty); set => SetValue(VisualProperty, value); } 
        public GeometryModel3D GeometryModel { get => (GeometryModel3D)GetValue(GeometryModelProperty); set => SetValue(GeometryModelProperty, value); }

        protected override void InvalidateModel()
        {
            throw new NotImplementedException();
        }
    }
}
