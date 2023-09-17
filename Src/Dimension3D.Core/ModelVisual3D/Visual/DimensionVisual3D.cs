using Dimension3D.Core.Tools;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace Dimension3D.Core
{

    public abstract class DimensionVisual3D : DimensionElement3D
    {
        private static Type _typeofThis = typeof(DimensionVisual3D);
        private static readonly DependencyProperty OwnerProperty;
        private static readonly DependencyProperty Visual3DProperty;
        private static readonly DependencyProperty InputElementProperty;

        internal static readonly DependencyPropertyKey IsMouseOverPropertyKey;
        new public static readonly DependencyProperty IsMouseOverProperty;
        static DimensionVisual3D()
        {
            DefaultStyleKeyProperty.OverrideMetadata(_typeofThis, new FrameworkPropertyMetadata(_typeofThis));

            OwnerProperty = DependencyProperty.Register(nameof(Owner), typeof(OwnerContext), _typeofThis, new FrameworkPropertyMetadata());
            Visual3DProperty = DependencyProperty.Register(nameof(Visual3D), typeof(ModelVisual3D), _typeofThis, new FrameworkPropertyMetadata());
            InputElementProperty = DependencyProperty.Register(nameof(InputElement), typeof(DimensionInputElement3D), _typeofThis, new FrameworkPropertyMetadata());

            IsMouseOverPropertyKey = DependencyProperty.RegisterReadOnly(nameof(IsMouseOver), typeof(bool), _typeofThis, new FrameworkPropertyMetadata(false));
            IsMouseOverProperty = IsMouseOverPropertyKey.DependencyProperty;
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
        internal ModelVisual3D Visual3D { get => (ModelVisual3D)GetValue(Visual3DProperty); set => SetValue(Visual3DProperty, value); }
        private DimensionInputElement3D InputElement { get => (DimensionInputElement3D)GetValue(InputElementProperty); set => SetValue(InputElementProperty, value); }

        new public bool IsMouseOver { get => (bool)GetValue(IsMouseOverProperty); private set => SetValue(IsMouseOverPropertyKey, value); }

        internal void Attach(ModelVisual3D visual)
        {
            if (this.Owner != null)
                throw new InvalidOperationException();
        
            Owner = new OwnerContext(visual, this); 
        }
        //internal void AttachVisual3D(DimensionVisual3D parent)
        //{
        //    if (this.Owner != null)
        //        throw new InvalidOperationException();
  
        //    Owner = new OwnerContext(parent.Visual3D, this);
        //}


        internal void DetachRoot()
        {
            var owner = this.Owner;
            if (owner != null)
                owner.Detach();
            Owner = null;
        }

        protected abstract void InvalidateModel();

        protected override void OnMouseEnter(MouseEventArgs e)
        {
            IsMouseOver = true;
            base.OnMouseEnter(e);
        }
        protected override void OnMouseLeave(MouseEventArgs e)
        {
            IsMouseOver = false;
            base.OnMouseLeave(e);
        }


        private void DimensionButton3D_Loaded(object sender, RoutedEventArgs e)
        {
            this.Arrange(new Rect());
        }

        private void DimensionModelVisual3D_Unloaded(object sender, RoutedEventArgs e)
        {
            Visual3D.Content = null;
            InputElement.Model = null;
        }
        protected void ApplyModel(Model3D? model)
        {
            Visual3D.Content = model;
            InputElement.Model = model;
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
                        dimensionVisual.Attach(this.Visual3D);
                    }
                }
            });
            base.PrepareContainerForItemOverride(element, item); 
        }

        protected override void AddChild(object value)
        {
            base.AddChild(value);
        }

      

        private class OwnerContext
        {
            private readonly DimensionVisual3D _owner;
            private readonly ModelVisual3D _ownerVisual;
            public OwnerContext(ModelVisual3D ownerVisual, DimensionVisual3D rootElement)
            {
                _ownerVisual = ownerVisual;
                _owner = rootElement;
                _ownerVisual.Children.Add(_owner.Visual3D);
            }

            //public ModelVisual3D ParentVisual { get; private set; }
            public void Detach()
            {
                _ownerVisual.Children.Remove(_owner.Visual3D);
            }
        }
    }
}
