using Dimension3D.Core.Tools;
using System;
using System.Windows;
using System.Windows.Media.Media3D;

namespace Dimension3D.Core
{
    public class Dimension3D : DependencyObject
    {

        private static readonly Type _typeofThis = typeof(Dimension3D);
        public static readonly DependencyProperty Visual3DProperty;
        private static readonly DependencyProperty DataContextProperty;
        static Dimension3D()
        {
            Visual3DProperty = DependencyProperty.RegisterAttached("Visual3D", typeof(DimensionModelVisual3D), _typeofThis, new FrameworkPropertyMetadata<ModelVisual3D>(PropertyChangedCallback));
            DataContextProperty = DependencyProperty.RegisterAttached("DataContext", typeof(object), _typeofThis, new FrameworkPropertyMetadata());
        }



        public static DimensionModelVisual3D? GetVisual3D(ModelVisual3D obj) => (DimensionModelVisual3D?)obj.GetValue(Visual3DProperty);
        public static void SetVisual3D(ModelVisual3D obj, DimensionModelVisual3D value) => obj.SetValue(Visual3DProperty, value);



        public static object GetDataContext(ModelVisual3D obj) => (object)obj.GetValue(DataContextProperty);
        public static void SetDataContext(ModelVisual3D obj, object value) => obj.SetValue(DataContextProperty, value);




        private static void PropertyChangedCallback(ModelVisual3D d, DependencyPropertyChangedEventArgs e)
        { 
            if (e.OldValue is DimensionModelVisual3D oldComposition) oldComposition.DetachRoot();

            if (e.NewValue is DimensionModelVisual3D newComposition)
            {
                newComposition.AttachRoot(d);
                d.SetBindingTo(Dimension3D.DataContextProperty, FrameworkElement.DataContextProperty, newComposition);
            }
        }
    }
}
