using Dimension3D.Core.Tools;
using System;
using System.Windows;
using System.Windows.Media.Media3D;

namespace Dimension3D.Core
{
    public class Dimension3D : DependencyObject
    {

        private static readonly Type _typeofThis = typeof(Dimension3D);
        public static readonly DependencyProperty DimensionModelVisualProperty;
        private static readonly DependencyProperty DataContextProperty; 
        static Dimension3D()
        {
            DimensionModelVisualProperty = DependencyProperty.RegisterAttached("DimensionModelVisual", typeof(DimensionModelVisual3D), _typeofThis, new PropertyMetadata(null, PropertyChangedCallback));
            DataContextProperty = DependencyProperty.RegisterAttached("DataContext", typeof(object), _typeofThis, new PropertyMetadata(null));
        }



        public static DimensionModelVisual3D? GetDimensionModelVisual(ModelVisual3D obj) => (DimensionModelVisual3D?)obj.GetValue(DimensionModelVisualProperty);
        public static void SetDimensionModelVisual(ModelVisual3D obj, DimensionModelVisual3D value) => obj.SetValue(DimensionModelVisualProperty, value);



        public static object GetDataContext(ModelVisual3D obj) => (object)obj.GetValue(DataContextProperty);
        public static void SetDataContext(ModelVisual3D obj, object value) => obj.SetValue(DataContextProperty, value);

  


        private static void PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ModelVisual3D modelVisual3D)
            {
                if (e.OldValue is DimensionModelVisual3D oldComposition) oldComposition.DetachRoot();

                if (e.NewValue is DimensionModelVisual3D newComposition)
                {
                    newComposition.AttachRoot(modelVisual3D);
                    modelVisual3D.SetBindingTo(Dimension3D.DataContextProperty, FrameworkElement.DataContextProperty, newComposition);
                } 
            }
        } 
    }
}
