using Dimension3D.Core.Tools;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace Dimension3D.Core
{
    public class Dimension3D : DependencyObject
    {

        private static readonly Type _typeofThis = typeof(Dimension3D);
        public static readonly MethodInfo AddVisualChild;
        public static readonly MethodInfo RemoveVisualChild;
        public static readonly DependencyProperty ItemsSourceProperty;
        public static readonly DependencyProperty ItemTemplateProperty;
        public static readonly DependencyProperty ItemTemplateSelectorProperty;
        static Dimension3D()
        {
            //protected void AddVisualChild(Visual child);
            AddVisualChild = typeof(FrameworkElement).GetMethod("AddVisualChild", System.Reflection.BindingFlags.Instance | BindingFlags.NonPublic) ?? throw new InvalidOperationException("AddVisualChild");
            RemoveVisualChild = typeof(FrameworkElement).GetMethod("RemoveVisualChild", System.Reflection.BindingFlags.Instance | BindingFlags.NonPublic) ?? throw new InvalidOperationException("RemoveVisualChild");

            ItemsSourceProperty = DependencyProperty.RegisterAttached("ItemsSource", typeof(IEnumerable), _typeofThis, new FrameworkPropertyMetadata<Viewport3D>(ItemsSourcePropertyChangedCallback));
            ItemTemplateProperty = DependencyProperty.RegisterAttached("ItemTemplate", typeof(DataTemplate), _typeofThis, new FrameworkPropertyMetadata());
            ItemTemplateSelectorProperty = DependencyProperty.RegisterAttached("ItemTemplateSelector", typeof(DataTemplateSelector), _typeofThis, new FrameworkPropertyMetadata());
        }



        public static IEnumerable GetItemsSource(Viewport3D obj) => (IEnumerable)obj.GetValue(ItemsSourceProperty);
        public static void SetItemsSource(Viewport3D obj, IEnumerable value) => obj.SetValue(ItemsSourceProperty, value);


        public static DataTemplate GetItemTemplate(DependencyObject obj) => (DataTemplate)obj.GetValue(ItemTemplateProperty);
        public static void SetItemTemplate(DependencyObject obj, DataTemplate value) => obj.SetValue(ItemTemplateProperty, value);



        public static DataTemplateSelector GetItemTemplateSelector(DependencyObject obj) => (DataTemplateSelector)obj.GetValue(ItemTemplateSelectorProperty);
        public static void SetItemTemplateSelector(DependencyObject obj, DataTemplateSelector value) => obj.SetValue(ItemTemplateSelectorProperty, value);



        private static void ItemsSourcePropertyChangedCallback(Viewport3D d, DependencyPropertyChangedEventArgs e)
        {
            var ctrl = new VPItemsControl(d);

            ctrl.SetBindingTo(VPItemsControl.DataContextProperty, Viewport3D.DataContextProperty, d);
            ctrl.SetBindingTo(VPItemsControl.ItemsSourceProperty, Dimension3D.ItemsSourceProperty, d);
            ctrl.SetBindingTo(VPItemsControl.ItemTemplateProperty, Dimension3D.ItemTemplateProperty, d);
            ctrl.SetBindingTo(VPItemsControl.ItemTemplateSelectorProperty, Dimension3D.ItemTemplateSelectorProperty, d);

        }



    }

    public class VPItemsControl : ItemsControl
    {
        private readonly Viewport3D _vp;
        public VPItemsControl(Viewport3D vp)
        {
            _vp = vp;
            vp.Loaded += Vp_Loaded;
            vp.Unloaded += Vp_Unloaded;  
        }

        private void Vp_Unloaded(object sender, RoutedEventArgs e)
        {
            var vp = (Viewport3D)sender;
            Dimension3D.RemoveVisualChild.Invoke(vp, new[] { this });
        }

        private void Vp_Loaded(object sender, RoutedEventArgs e)
        {
            var vp = (Viewport3D)sender;
            Dimension3D.AddVisualChild.Invoke(vp, new[] { this });
            Arrange(new Rect());
        }

        
        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            element.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Render, () =>
            {
                if (VisualTreeHelper.GetChildrenCount(element) > 0)
                {
                    var child = VisualTreeHelper.GetChild(element, 0);
                    if (child is DimensionVisual3D dimensionVisual)
                    {
                        dimensionVisual.AttachRoot(_vp);
                    }
                }
            });
            base.PrepareContainerForItemOverride(element, item); 

        }
         
        protected override void ClearContainerForItemOverride(DependencyObject element, object item)
        {
            element.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Render, () =>
            {
                if (VisualTreeHelper.GetChildrenCount(element) > 0)
                {
                    var child = VisualTreeHelper.GetChild(element, 0);
                    if (child is DimensionVisual3D dimensionVisual)
                    {
                        dimensionVisual.DetachRoot();
                    }
                }
            });
            base.ClearContainerForItemOverride(element, item);
        }
    }
}
