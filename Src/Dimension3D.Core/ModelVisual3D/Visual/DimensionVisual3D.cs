using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Media3D;

namespace Dimension3D.Core
{
    public abstract class DimensionVisual3D : DimensionElement3D
    {
        private static Type _typeofThis = typeof(DimensionVisual3D);
        private static readonly DependencyProperty RootModelVisualProperty;
        private static readonly DependencyProperty ModelVisualProperty;
        private static readonly DependencyProperty ModelProperty;
        public static readonly DependencyProperty InputElementProperty;
        public static readonly DependencyProperty ItemsSourceProperty;
        public static readonly DependencyProperty SatelliteItemsSourceProperty;
        public static readonly DependencyProperty ItemTemplateSelectorProperty;
        public static readonly DependencyProperty ItemTemplateProperty;
        static DimensionVisual3D()
        { 
            RootModelVisualProperty = DependencyProperty.Register(nameof(RootModelVisual), typeof(ModelVisual3D), _typeofThis, new PropertyMetadata(null));
            ModelVisualProperty = DependencyProperty.Register(nameof(ModelVisual), typeof(ModelVisual3D), _typeofThis, new PropertyMetadata(null));
            ModelProperty = DependencyProperty.Register(nameof(Model), typeof(DimensionModel3D), _typeofThis, new PropertyMetadata(null));
            InputElementProperty = DependencyProperty.Register(nameof(InputElement), typeof(DimensionInputElement3D), _typeofThis, new PropertyMetadata(null));
            ItemsSourceProperty = DependencyProperty.Register(nameof(ItemsSource), typeof(IEnumerable), _typeofThis, new PropertyMetadata(null));
            SatelliteItemsSourceProperty = DependencyProperty.Register(nameof(SatelliteItemsSource), typeof(IEnumerable), _typeofThis, new PropertyMetadata(null));
            ItemTemplateSelectorProperty = DependencyProperty.Register(nameof(ItemTemplateSelector), typeof(DataTemplateSelector), _typeofThis, new PropertyMetadata(null));
            ItemTemplateProperty = DependencyProperty.Register(nameof(ItemTemplate), typeof(DataTemplate), _typeofThis, new PropertyMetadata(null));
        }

        private ModelVisual3D? RootModelVisual { get => (ModelVisual3D?)GetValue(RootModelVisualProperty); set => SetValue(RootModelVisualProperty, value); }
        private ModelVisual3D ModelVisual { get => (ModelVisual3D)GetValue(ModelVisualProperty); set => SetValue(ModelVisualProperty, value); }
        private DimensionModel3D Model { get => (DimensionModel3D)GetValue(ModelProperty); set => SetValue(ModelProperty, value); }
        public DimensionInputElement3D InputElement { get => (DimensionInputElement3D)GetValue(InputElementProperty); set => SetValue(InputElementProperty, value); }
        public IEnumerable ItemsSource { get => (IEnumerable)GetValue(ItemsSourceProperty); set => SetValue(ItemsSourceProperty, value); }
        public IEnumerable SatelliteItemsSource { get => (IEnumerable)GetValue(SatelliteItemsSourceProperty); set => SetValue(SatelliteItemsSourceProperty, value); }
        public DataTemplateSelector ItemTemplateSelector { get => (DataTemplateSelector)GetValue(ItemTemplateSelectorProperty); set => SetValue(ItemTemplateSelectorProperty, value); }
        public DataTemplate ItemTemplate { get => (DataTemplate)GetValue(ItemTemplateProperty); set => SetValue(ItemTemplateProperty, value); }




        internal void AttachRoot(ModelVisual3D modelVisual3D)
        {
            RootModelVisual = modelVisual3D;
        }

        internal void DetachRoot()
        {
            RootModelVisual = null;
        }



    }
}
