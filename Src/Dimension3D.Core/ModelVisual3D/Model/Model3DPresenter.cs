using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media.Media3D;

namespace Dimension3D.Core
{
    [ContentProperty(nameof(Model))]
    [DefaultProperty(nameof(Model))]
    public class Model3DPresenter : FrameworkElement
    {
        private static Type _typeofThis = typeof(Model3DPresenter);
        public static readonly DependencyProperty ModelProperty;
        static Model3DPresenter()
        {
            ModelProperty = DependencyProperty.Register(nameof(Model), typeof(Model3D), _typeofThis, new FrameworkPropertyMetadata<Model3DPresenter>(ModelPropertyChangedCallback));
        }

      

        public Model3D Model { get => (Model3D)GetValue(ModelProperty); set => SetValue(ModelProperty, value); }

        private static void ModelPropertyChangedCallback(Model3DPresenter d, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue is Model3D oldModel)
                d.RemoveLogicalChild(oldModel);
            if (e.OldValue is Model3D newModel)
                d.AddLogicalChild(newModel);
        }
    }
}
