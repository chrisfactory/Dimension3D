using Dimension3D.Core.Tools;
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
        public static readonly DependencyProperty Visual3DProperty;
        private static readonly DependencyProperty ChildrenProperty;
        private static readonly DependencyProperty InputElementProperty;

        public static readonly DependencyProperty ItemsSourceProperty;
        public static readonly DependencyProperty ItemTemplateSelectorProperty;
        public static readonly DependencyProperty ItemTemplateProperty;
        static DimensionVisual3D()
        {
            DefaultStyleKeyProperty.OverrideMetadata(_typeofThis, new FrameworkPropertyMetadata(_typeofThis));

            OwnerProperty = DependencyProperty.Register(nameof(Owner), typeof(OwnerContext), _typeofThis, new FrameworkPropertyMetadata());
            Visual3DProperty = DependencyProperty.Register(nameof(Visual3D), typeof(ModelVisual3D), _typeofThis, new FrameworkPropertyMetadata());
            ChildrenProperty = DependencyProperty.Register(nameof(Children), typeof(VisualItems), _typeofThis, new FrameworkPropertyMetadata());
            InputElementProperty = DependencyProperty.Register(nameof(InputElement), typeof(DimensionInputElement3D), _typeofThis, new FrameworkPropertyMetadata());

            ItemsSourceProperty = DependencyProperty.Register(nameof(ItemsSource), typeof(IEnumerable), _typeofThis, new FrameworkPropertyMetadata<DimensionVisual3D>(ItemsPropertyChangedCallback));
            ItemTemplateSelectorProperty = DependencyProperty.Register(nameof(ItemTemplateSelector), typeof(DataTemplateSelector), _typeofThis, new FrameworkPropertyMetadata());
            ItemTemplateProperty = DependencyProperty.Register(nameof(ItemTemplate), typeof(DataTemplate), _typeofThis, new FrameworkPropertyMetadata());
        }


        internal protected DimensionVisual3D()
        {
            this.Loaded += DimensionButton3D_Loaded;
            this.Unloaded += DimensionModelVisual3D_Unloaded;

            Visual3D = new ModelVisual3D();
            InputElement = new DimensionInputElement3D(this);
            Visual3D.Children.Add(InputElement);
            Visual3D.SetBindingTo(ModelVisual3D.TransformProperty, DimensionVisual3D.TransformProperty, this);
        }



        private OwnerContext? Owner { get => (OwnerContext?)GetValue(OwnerProperty); set => SetValue(OwnerProperty, value); }
        public ModelVisual3D Visual3D { get => (ModelVisual3D)GetValue(Visual3DProperty); set => SetValue(Visual3DProperty, value); }
        private VisualItems Children { get => (VisualItems)GetValue(ChildrenProperty); set => SetValue(ChildrenProperty, value); }
        private DimensionInputElement3D InputElement { get => (DimensionInputElement3D)GetValue(InputElementProperty); set => SetValue(InputElementProperty, value); }


        public IEnumerable ItemsSource { get => (IEnumerable)GetValue(ItemsSourceProperty); set => SetValue(ItemsSourceProperty, value); }
        public DataTemplateSelector ItemTemplateSelector { get => (DataTemplateSelector)GetValue(ItemTemplateSelectorProperty); set => SetValue(ItemTemplateSelectorProperty, value); }
        public DataTemplate ItemTemplate { get => (DataTemplate)GetValue(ItemTemplateProperty); set => SetValue(ItemTemplateProperty, value); }


        internal void AttachRoot(Viewport3D viewport)
        {
            if (this.Owner != null)
                throw new InvalidOperationException();
            var visualRoot = new ModelVisual3D();
            Owner = new OwnerContext(visualRoot, this);

            if (this.IsLoaded)
            {
                viewport.Children.Add(visualRoot);
            }
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


        private void DimensionButton3D_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void DimensionModelVisual3D_Unloaded(object sender, RoutedEventArgs e)
        {
            Visual3D.Content = null;
            InputElement.Model = null;
        }
        protected void SetModel(Model3D? model)
        {
            Visual3D.Content = model;
            InputElement.Model = model;
            Arrange(new Rect());
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
