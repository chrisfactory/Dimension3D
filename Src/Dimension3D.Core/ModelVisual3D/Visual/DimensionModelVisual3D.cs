using Dimension3D.Core.Tools;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media.Media3D;

namespace Dimension3D.Core
{
    [TemplatePart(Name = "PART_MODEL", Type = typeof(Model3DPresenter))]
    [ContentProperty(nameof(Model))]
    [DefaultProperty(nameof(Model))]
    public class DimensionModelVisual3D : DimensionVisual3D
    {
        private static Type _typeofThis = typeof(DimensionModelVisual3D);
        public static readonly DependencyProperty ModelProperty;
        static DimensionModelVisual3D()
        {
            DefaultStyleKeyProperty.OverrideMetadata(_typeofThis, new FrameworkPropertyMetadata(_typeofThis));
            ModelProperty = DependencyProperty.Register(nameof(Model), typeof(Model3D), _typeofThis, new FrameworkPropertyMetadata<DimensionModelVisual3D>(ModelPropertyChangedCallback));
        }


        public Model3D Model { get => (Model3D)GetValue(ModelProperty); set => SetValue(ModelProperty, value); }


        private static void ModelPropertyChangedCallback(DimensionModelVisual3D d, DependencyPropertyChangedEventArgs e)
        { 
            d.InvalidateModel();
        }

        protected override void InvalidateModel()
        {
            this.ApplyModel(Model);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            var modelPresenter = this.Template.FindName("PART_MODEL", this) as Model3DPresenter;
      
            if (modelPresenter != null)
            {
                this.SetBindingTo(ModelProperty, Model3DPresenter.ModelProperty, modelPresenter);
            }
            //ApplyModel(modelPresenter);
        }
    }
}
