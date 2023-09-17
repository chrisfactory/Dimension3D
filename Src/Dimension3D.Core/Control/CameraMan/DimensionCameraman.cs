using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media.Media3D;
using System.Xml.Linq;

namespace Dimension3D.Core
{
    public enum ClickSides : short
    {
        Front = 1,
        Right = 2,
        Back = 4,
        Left = 8,
        Bottom = 16,
        Top = 32
    }
    public class CameramanRoutedEventArgs : RoutedEventArgs
    {
        public CameramanRoutedEventArgs(ClickSides side) : base() { Side = side; }
        public CameramanRoutedEventArgs(ClickSides side, RoutedEvent routedEvent) : base(routedEvent) { Side = side; }
        public CameramanRoutedEventArgs(ClickSides side, RoutedEvent routedEvent, object source) : base(routedEvent, source) { Side = side; }

        public ClickSides Side { get; }
    }
    public class DimensionCameraman : DimensionModelVisual3D
    {
        private static Type _typeofThis = typeof(DimensionCameraman);
        public static readonly DependencyProperty ModelVisual3DProperty;
        public static readonly RoutedEvent ClickEvent;
        public static readonly DependencyProperty FrontProperty;
        public static readonly DependencyProperty RightProperty;
        public static readonly DependencyProperty BackProperty;
        public static readonly DependencyProperty LeftProperty;
        public static readonly DependencyProperty BottomProperty;
        public static readonly DependencyProperty TopProperty;
        static DimensionCameraman()
        {
            DefaultStyleKeyProperty.OverrideMetadata(_typeofThis, new FrameworkPropertyMetadata(_typeofThis));
            ModelVisual3DProperty = DependencyProperty.Register(nameof(ModelVisual3D), typeof(ModelVisual3D), _typeofThis, new FrameworkPropertyMetadata<DimensionCameraman>(ModelVisual3DPropertyChangedCallback));
            FrontProperty = DependencyProperty.Register(nameof(Front), typeof(DimensionButton3D), _typeofThis, new FrameworkPropertyMetadata<DimensionCameraman>(FacesPropertyChangedCallback));
            RightProperty = DependencyProperty.Register(nameof(Right), typeof(DimensionButton3D), _typeofThis, new FrameworkPropertyMetadata<DimensionCameraman>(FacesPropertyChangedCallback));
            BackProperty = DependencyProperty.Register(nameof(Back), typeof(DimensionButton3D), _typeofThis, new FrameworkPropertyMetadata<DimensionCameraman>(FacesPropertyChangedCallback));
            LeftProperty = DependencyProperty.Register(nameof(Left), typeof(DimensionButton3D), _typeofThis, new FrameworkPropertyMetadata<DimensionCameraman>(FacesPropertyChangedCallback));
            BottomProperty = DependencyProperty.Register(nameof(Bottom), typeof(DimensionButton3D), _typeofThis, new FrameworkPropertyMetadata<DimensionCameraman>(FacesPropertyChangedCallback));
            TopProperty = DependencyProperty.Register(nameof(Top), typeof(DimensionButton3D), _typeofThis, new FrameworkPropertyMetadata<DimensionCameraman>(FacesPropertyChangedCallback));
            ClickEvent = EventManager.RegisterRoutedEvent("Click", RoutingStrategy.Bubble, typeof(EventHandler<CameramanRoutedEventArgs>), _typeofThis);
        }



        public event EventHandler<CameramanRoutedEventArgs> Click { add { AddHandler(ClickEvent, value); } remove { RemoveHandler(ClickEvent, value); } }



        public ModelVisual3D ModelVisual3D { get => (ModelVisual3D)GetValue(ModelVisual3DProperty); set => SetValue(ModelVisual3DProperty, value); }


        public DimensionButton3D Front { get => (DimensionButton3D)GetValue(FrontProperty); set => SetValue(FrontProperty, value); }
        public DimensionButton3D Right { get => (DimensionButton3D)GetValue(RightProperty); set => SetValue(RightProperty, value); }
        public DimensionButton3D Back { get => (DimensionButton3D)GetValue(BackProperty); set => SetValue(BackProperty, value); }
        public DimensionButton3D Left { get => (DimensionButton3D)GetValue(LeftProperty); set => SetValue(LeftProperty, value); }
        public DimensionButton3D Bottom { get => (DimensionButton3D)GetValue(BottomProperty); set => SetValue(BottomProperty, value); }
        public DimensionButton3D Top { get => (DimensionButton3D)GetValue(TopProperty); set => SetValue(TopProperty, value); }

        private static void ModelVisual3DPropertyChangedCallback(DimensionCameraman d, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue is ModelVisual3D oldModel)
                oldModel.Children.Remove(d.Visual3D);
            if (e.NewValue is ModelVisual3D newModel)
                newModel.Children.Add(d.Visual3D);
        }

        private static void FacesPropertyChangedCallback(DimensionCameraman d, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue is DimensionButton3D oldDimensionObject)
            {
                WeakEventManager<DimensionButtonBase3D, RoutedEventArgs>.RemoveHandler(oldDimensionObject, "Click", d.OnFaceClickEvent);
                d.Items.Remove(oldDimensionObject);
            }

            if (e.NewValue is DimensionButton3D newDimensionObject)
            {
                d.Items.Add(newDimensionObject);
                WeakEventManager<DimensionButtonBase3D, RoutedEventArgs>.AddHandler(newDimensionObject, "Click", d.OnFaceClickEvent);
            }

        }

        private void OnFaceClickEvent(object? sender, RoutedEventArgs e)
        {
            if (sender == Front)
            {
                RaiseEvent(new CameramanRoutedEventArgs(ClickSides.Front, ClickEvent, this));
            }
            else if (sender == Back)
            {
                RaiseEvent(new CameramanRoutedEventArgs(ClickSides.Back, ClickEvent, this));

            }
            else if (sender == Top)
            {
                RaiseEvent(new CameramanRoutedEventArgs(ClickSides.Top, ClickEvent, this));
            }
            else if (sender == Bottom)
            {
                RaiseEvent(new CameramanRoutedEventArgs(ClickSides.Bottom, ClickEvent, this));
            }
            else if (sender == Right)
            {
                RaiseEvent(new CameramanRoutedEventArgs(ClickSides.Right, ClickEvent, this));

            }
            else if (sender == Left)
            {
                RaiseEvent(new CameramanRoutedEventArgs(ClickSides.Left, ClickEvent, this));

            }
        }

    }
}
