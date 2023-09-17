using Dimension3D.Core.Tools;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Dynamic;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace Dimension3D.Core
{
    public class Dimension3D : DependencyObject
    {

        private static readonly Type _typeofThis = typeof(Dimension3D);
        internal static readonly MethodInfo AddVisualChild;
        internal static readonly MethodInfo AddLogicalChild;
        internal static readonly MethodInfo RemoveVisualChild;

        private static readonly DependencyProperty ItemsControlProperty;
        public static readonly DependencyProperty UseDimension3DProperty;
        public static readonly DependencyProperty ItemsSourceProperty;
        public static readonly DependencyProperty ItemTemplateProperty;
        public static readonly DependencyProperty ItemTemplateSelectorProperty;
        //public static readonly DependencyProperty ItemsProperty;
        static Dimension3D()
        {
            //protected void AddVisualChild(Visual child);
            AddVisualChild = typeof(FrameworkElement).GetMethod("AddVisualChild", System.Reflection.BindingFlags.Instance | BindingFlags.NonPublic) ?? throw new InvalidOperationException("AddVisualChild");
            AddLogicalChild = typeof(FrameworkElement).GetMethod("AddLogicalChild", System.Reflection.BindingFlags.Instance | BindingFlags.NonPublic) ?? throw new InvalidOperationException("AddLogicalChild");
            RemoveVisualChild = typeof(FrameworkElement).GetMethod("RemoveVisualChild", System.Reflection.BindingFlags.Instance | BindingFlags.NonPublic) ?? throw new InvalidOperationException("RemoveVisualChild");


            UseDimension3DProperty = DependencyProperty.RegisterAttached("UseDimension3D", typeof(bool), _typeofThis, new FrameworkPropertyMetadata<Viewport3D>(ItemsControlPropertyChangedCallback));
            ItemsControlProperty = DependencyProperty.RegisterAttached("ItemsControl", typeof(VPItemsControl), _typeofThis, new FrameworkPropertyMetadata());

            ItemsSourceProperty = DependencyProperty.RegisterAttached("ItemsSource", typeof(IEnumerable), _typeofThis, new FrameworkPropertyMetadata());
            ItemTemplateProperty = DependencyProperty.RegisterAttached("ItemTemplate", typeof(DataTemplate), _typeofThis, new FrameworkPropertyMetadata());
            ItemTemplateSelectorProperty = DependencyProperty.RegisterAttached("ItemTemplateSelector", typeof(DataTemplateSelector), _typeofThis, new FrameworkPropertyMetadata());
        }


        private static VPItemsControl? GetItemsControl(Viewport3D obj) => (VPItemsControl?)obj.GetValue(ItemsControlProperty);
        private static void SetItemsControl(Viewport3D obj, VPItemsControl? value) => obj.SetValue(ItemsControlProperty, value);

        public static bool GetUseDimension3D(Viewport3D obj) => (bool)obj.GetValue(UseDimension3DProperty);
        public static void SetUseDimension3D(Viewport3D obj, bool value) => obj.SetValue(UseDimension3DProperty, value);

        public static IEnumerable GetItemsSource(Viewport3D obj) => (IEnumerable)obj.GetValue(ItemsSourceProperty);
        public static void SetItemsSource(Viewport3D obj, IEnumerable value) => obj.SetValue(ItemsSourceProperty, value);

        public static DataTemplate GetItemTemplate(Viewport3D obj) => (DataTemplate)obj.GetValue(ItemTemplateProperty);
        public static void SetItemTemplate(Viewport3D obj, DataTemplate value) => obj.SetValue(ItemTemplateProperty, value);

        public static DataTemplateSelector GetItemTemplateSelector(Viewport3D obj) => (DataTemplateSelector)obj.GetValue(ItemTemplateSelectorProperty);
        public static void SetItemTemplateSelector(Viewport3D obj, DataTemplateSelector value) => obj.SetValue(ItemTemplateSelectorProperty, value);

        public static ItemCollection? GetItems(Viewport3D obj) => GetItemsControl(obj)?.Items;


        internal static ModelVisual3D? GetRootVisualModel(Viewport3D target)
        {
            return GetItemsControl(target)?.RootVisualModel;
        }


        private static void ItemsControlPropertyChangedCallback(Viewport3D d, DependencyPropertyChangedEventArgs e)
        {

            if (e.OldValue is Boolean oldValue && oldValue)
            {
                var ctrl = GetItemsControl(d);
                if (ctrl != null)
                {
                    BindingOperations.ClearBinding(ctrl, VPItemsControl.DataContextProperty);
                    BindingOperations.ClearBinding(ctrl, VPItemsControl.ItemsSourceProperty);
                    BindingOperations.ClearBinding(ctrl, VPItemsControl.ItemTemplateProperty);
                    BindingOperations.ClearBinding(ctrl, VPItemsControl.ItemTemplateSelectorProperty);
                }
            }
            if (e.NewValue is Boolean newValue && newValue)
            {
                var ctrl = new VPItemsControl(d);
                SetItemsControl(d, ctrl);
                ctrl.SetBindingTo(VPItemsControl.DataContextProperty, Viewport3D.DataContextProperty, d);
                ctrl.SetBindingTo(VPItemsControl.ItemsSourceProperty, Dimension3D.ItemsSourceProperty, d);
                ctrl.SetBindingTo(VPItemsControl.ItemTemplateProperty, Dimension3D.ItemTemplateProperty, d);
                ctrl.SetBindingTo(VPItemsControl.ItemTemplateSelectorProperty, Dimension3D.ItemTemplateSelectorProperty, d);
            }
        }




        private class VPItemsControl : ItemsControl
        {
            private readonly Viewport3D _vp;
            private readonly ModelVisual3D _root;
            public VPItemsControl(Viewport3D vp)
            {
                _vp = vp;
                _root = new ModelVisual3D();
                vp.Loaded += Vp_Loaded;
                vp.Unloaded += Vp_Unloaded;
                if (vp.IsLoaded)
                    vp.Children.Add(_root);
            }

            internal ModelVisual3D RootVisualModel { get { return _root; } }

            private void Vp_Unloaded(object sender, RoutedEventArgs e)
            {
                var vp = (Viewport3D)sender;
                Dimension3D.RemoveVisualChild.Invoke(vp, new[] { this });
                _vp.Children.Remove(_root);
            }

            private void Vp_Loaded(object sender, RoutedEventArgs e)
            {
                var vp = (Viewport3D)sender;
                Dimension3D.AddVisualChild.Invoke(vp, new[] { this });
                //Dimension3D.AddLogicalChild.Invoke(vp, new[] { this });
                _vp.Children.Add(_root);
                Arrange(new Rect());
            }

            protected override bool IsItemItsOwnContainerOverride(object item)
            {
                if (item is DimensionVisual3D)
                    return false;
                return base.IsItemItsOwnContainerOverride(item);
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
                            dimensionVisual.Attach(_root);
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


}
