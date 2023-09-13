using System;
using System.Windows;
using System.Windows.Media.Media3D;

namespace Dimension3D.Core
{
    public class DimensionModelVisual3D : DimensionVisual3D
    {
        private static Type _typeofThis = typeof(DimensionModelVisual3D);
        public static readonly DependencyProperty ModelProperty;
        static DimensionModelVisual3D()
        {
            DefaultStyleKeyProperty.OverrideMetadata(_typeofThis, new FrameworkPropertyMetadata(_typeofThis));
            ModelProperty = DependencyProperty.Register(nameof(Model), typeof(DimensionModel3D), _typeofThis, new FrameworkPropertyMetadata<DimensionModelVisual3D>(ModelPropertyChangedCallback));
        }




        public DimensionModel3D Model { get => (DimensionModel3D)GetValue(ModelProperty); set => SetValue(ModelProperty, value); }



        private static void ModelPropertyChangedCallback(DimensionModelVisual3D d, DependencyPropertyChangedEventArgs e)
        {
            Model3D? newModel = null;
            if (e.NewValue is DimensionModel3D dimensionModel3D)
                newModel = dimensionModel3D.GetModel();
            d.SetModel(newModel);
        }
    }
}
