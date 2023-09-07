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
        private static readonly DependencyProperty OwnerProperty;
        private static readonly DependencyProperty ModelVisualProperty;
        private static readonly DependencyProperty ChildrenProperty;
        private static readonly DependencyProperty SatelliteChildrenProperty;

        public static readonly DependencyProperty InputElementProperty;
        public static readonly DependencyProperty ItemsSourceProperty;
        public static readonly DependencyProperty SatelliteItemsSourceProperty;
        public static readonly DependencyProperty ItemTemplateSelectorProperty;
        public static readonly DependencyProperty ItemTemplateProperty;
        static DimensionVisual3D()
        {
            OwnerProperty = DependencyProperty.Register(nameof(Owner), typeof(OwnerContext), _typeofThis, new FrameworkPropertyMetadata());
            ModelVisualProperty = DependencyProperty.Register(nameof(Visual3D), typeof(ModelVisual3D), _typeofThis, new FrameworkPropertyMetadata());
            ChildrenProperty = DependencyProperty.Register(nameof(Children), typeof(VisualItems), _typeofThis, new FrameworkPropertyMetadata());
            SatelliteChildrenProperty = DependencyProperty.Register(nameof(SatelliteChildren), typeof(VisualItems), _typeofThis, new FrameworkPropertyMetadata());

            InputElementProperty = DependencyProperty.Register(nameof(InputElement), typeof(DimensionInputElement3D), _typeofThis, new FrameworkPropertyMetadata());
            ItemsSourceProperty = DependencyProperty.Register(nameof(ItemsSource), typeof(IEnumerable), _typeofThis, new FrameworkPropertyMetadata<DimensionVisual3D>(ItemsPropertyChangedCallback));
            SatelliteItemsSourceProperty = DependencyProperty.Register(nameof(SatelliteItemsSource), typeof(IEnumerable), _typeofThis, new FrameworkPropertyMetadata());
            ItemTemplateSelectorProperty = DependencyProperty.Register(nameof(ItemTemplateSelector), typeof(DataTemplateSelector), _typeofThis, new FrameworkPropertyMetadata());
            ItemTemplateProperty = DependencyProperty.Register(nameof(ItemTemplate), typeof(DataTemplate), _typeofThis, new FrameworkPropertyMetadata());
        }


        protected DimensionVisual3D()
        {
            Visual3D = new ModelVisual3D();
        }
        private OwnerContext? Owner { get => (OwnerContext?)GetValue(OwnerProperty); set => SetValue(OwnerProperty, value); }
        private ModelVisual3D Visual3D { get => (ModelVisual3D)GetValue(ModelVisualProperty); set => SetValue(ModelVisualProperty, value); }
        private VisualItems Children { get => (VisualItems)GetValue(ChildrenProperty); set => SetValue(ChildrenProperty, value); }
        private VisualItems SatelliteChildren { get => (VisualItems)GetValue(SatelliteChildrenProperty); set => SetValue(SatelliteChildrenProperty, value); }

        public DimensionInputElement3D InputElement { get => (DimensionInputElement3D)GetValue(InputElementProperty); set => SetValue(InputElementProperty, value); }
        public IEnumerable ItemsSource { get => (IEnumerable)GetValue(ItemsSourceProperty); set => SetValue(ItemsSourceProperty, value); }
        public IEnumerable SatelliteItemsSource { get => (IEnumerable)GetValue(SatelliteItemsSourceProperty); set => SetValue(SatelliteItemsSourceProperty, value); }
        public DataTemplateSelector ItemTemplateSelector { get => (DataTemplateSelector)GetValue(ItemTemplateSelectorProperty); set => SetValue(ItemTemplateSelectorProperty, value); }
        public DataTemplate ItemTemplate { get => (DataTemplate)GetValue(ItemTemplateProperty); set => SetValue(ItemTemplateProperty, value); }

         
        internal void AttachRoot(ModelVisual3D modelVisual3D)
        {
            if (this.Owner != null)
                throw new InvalidOperationException();
            Owner = new OwnerContext(modelVisual3D, this);
        }

        internal void DetachRoot()
        {
            var owner = this.Owner;
            if (owner != null)
                owner.Detach();
            Owner = null;
        }
        #region Items
        private static void ItemsPropertyChangedCallback(DimensionVisual3D d, DependencyPropertyChangedEventArgs e)
        {

        }
        #endregion


        protected override void OnApplyTransform()
        {
            base.OnApplyTransform();
            Visual3D.Transform = this.Transform;
        }

        private class OwnerContext
        {
            private readonly DimensionVisual3D _owner;
            private readonly ModelVisual3D _ownerVisual;
            private readonly ModelVisual3D _RootVisual;
            public OwnerContext(ModelVisual3D ownerVisual, DimensionVisual3D rootElement)
            {
                _ownerVisual = ownerVisual;
                _owner = rootElement;
                _RootVisual = new ModelVisual3D();
                _ownerVisual.Children.Add(_RootVisual);
                _ownerVisual.Children.Add(_owner.Visual3D);
            }

            public ModelVisual3D RootVisual { get => _RootVisual; }
            public void Detach()
            {
                _ownerVisual.Children.Remove(_owner.Visual3D);
                _ownerVisual.Children.Remove(_RootVisual);
            }
        }
    }
}
