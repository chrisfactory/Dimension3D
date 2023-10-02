using Dimension3D.Core.Tools;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Media3D;

namespace Dimension3D.Core
{
    public enum ClickSides
    {
        FaceFront,
        FaceRight,
        FaceBack,
        FaceLeft,
        FaceBottom,
        FaceTop,

        EdgeFrontRight,
        EdgeFrontLeft,
        EdgeBackRight,
        EdgeBackLeft,
        EdgeTopRight,
        EdgeTopLeft,
        EdgeTopBack,
        EdgeTopFront,
        EdgeBottomRight,
        EdgeBottomLeft,
        EdgeBottomBack,
        EdgeBottomFront,

        VertexTopFrontRight,
        VertexTopFrontLeft,
        VertexTopBackRight,
        VertexTopBackLeft,
        VertexBottomFrontRight,
        VertexBottomFrontLeft,
        VertexBottomBackRight,
        VertexBottomBackLeft

    }

    public class DimensionCameramanViewer : Control
    {
        private static Type _typeofThis = typeof(DimensionCameramanViewer);
        public static readonly DependencyProperty ViewportProperty;
        public static readonly DependencyProperty ZoomElementProperty;

        static DimensionCameramanViewer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(_typeofThis, new FrameworkPropertyMetadata(_typeofThis));

            ViewportProperty = DependencyProperty.Register(nameof(Viewport), typeof(Viewport3D), _typeofThis, new FrameworkPropertyMetadata());
            ZoomElementProperty = DependencyProperty.Register(nameof(ZoomElement), typeof(UIElement), _typeofThis, new FrameworkPropertyMetadata<DimensionCameramanViewer>(ZoomElementPropertyChangedCallback));


            CommandHelpers.RegisterCommandHandler(_typeofThis, FaceCommand, new ExecutedRoutedEventHandler(OnFaceCommand));
        }

        private static void OnFaceCommand(object sender, ExecutedRoutedEventArgs e)
        {
            if (sender is DimensionCameramanViewer camViewer)
            {
                var side = ExtractParameter(e.Parameter);
                if (side != null)
                    camViewer.OnFaceClickEvent(sender, e, side.Value);
            }
        }

        public static RoutedCommand FaceCommand { get; } = new RoutedCommand("FaceCommand", _typeofThis);

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            e.Handled = true;
            base.OnMouseLeftButtonUp(e);
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            e.Handled = true;
            base.OnMouseLeftButtonDown(e);
        }
        protected override void OnMouseRightButtonDown(MouseButtonEventArgs e)
        {
            e.Handled = true;
            base.OnMouseRightButtonDown(e);
        }
        protected override void OnMouseRightButtonUp(MouseButtonEventArgs e)
        {
            e.Handled = true;
            base.OnMouseRightButtonUp(e);
        }
        private static ClickSides? ExtractParameter(object? parameter)
        {
            if (parameter is ClickSides side)
                return side;
            if (parameter is String s && Enum.TryParse<ClickSides>(s, out var sideResult))
                return sideResult;

            return null;
        }



        public DimensionCameramanViewer()
        {


        }



        public Viewport3D? Viewport { get => (Viewport3D?)GetValue(ViewportProperty); set => SetValue(ViewportProperty, value); }
        public UIElement? ZoomElement { get => (UIElement?)GetValue(ZoomElementProperty); set => SetValue(ZoomElementProperty, value); }



        private static void ZoomElementPropertyChangedCallback(DimensionCameramanViewer d, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue is UIElement oldZoomElement)
            {

                WeakEventManager<UIElement, MouseWheelEventArgs>.RemoveHandler(oldZoomElement, "MouseWheel", d.OnMouseWheelEvent);
                WeakEventManager<UIElement, MouseEventArgs>.RemoveHandler(oldZoomElement, "MouseMove", d.OnMouseMoveEvent);
                WeakEventManager<UIElement, MouseButtonEventArgs>.RemoveHandler(oldZoomElement, "MouseDown", d.OnMouseDownEvent);
                WeakEventManager<UIElement, MouseButtonEventArgs>.RemoveHandler(oldZoomElement, "MouseUp", d.OnMouseUpEvent);
            }
            if (e.NewValue is UIElement newZoomElement)
            {

                WeakEventManager<UIElement, MouseWheelEventArgs>.AddHandler(newZoomElement, "MouseWheel", d.OnMouseWheelEvent);
                WeakEventManager<UIElement, MouseEventArgs>.AddHandler(newZoomElement, "MouseMove", d.OnMouseMoveEvent);
                WeakEventManager<UIElement, MouseButtonEventArgs>.AddHandler(newZoomElement, "MouseDown", d.OnMouseDownEvent);
                WeakEventManager<UIElement, MouseButtonEventArgs>.AddHandler(newZoomElement, "MouseUp", d.OnMouseUpEvent);
            }
        }

        private void OnMouseMoveEvent(object? sender, MouseEventArgs e)
        {
            var model = Dimension3D.GetModelContext(Viewport);
            if (model != null)
            {
                model.MouseMove(Viewport, e);
            }
        }

        private void OnMouseDownEvent(object? sender, MouseButtonEventArgs e)
        {
            var model = Dimension3D.GetModelContext(Viewport);
            if (model != null)
            {
                model.MouseDown(Viewport, e);
            }
        }

        private void OnMouseUpEvent(object? sender, MouseButtonEventArgs e)
        {
            var model = Dimension3D.GetModelContext(Viewport);
            if (model != null)
            {
                model.MouseUp(e);
            }
        }

        private void OnMouseWheelEvent(object? sender, MouseWheelEventArgs e)
        {
            var model = Dimension3D.GetModelContext(Viewport);
            if (model != null)
            {
                model.ZoomEvent(e);
            }
        }



        private void OnFaceClickEvent(object? sender, ExecutedRoutedEventArgs e, ClickSides side)
        {
            var model = Dimension3D.GetModelContext(Viewport);
            if (model != null)
            {
                switch (side)
                {
                    case ClickSides.FaceFront:
                        model.BeginRotation(new Vector3D(0, 1, 0), 0);
                        break;
                    case ClickSides.FaceBack:
                        model.BeginRotation(new Vector3D(0, 1, 0), 180);
                        break;
                    case ClickSides.FaceRight:
                        model.BeginRotation(new Vector3D(0, 1, 0), -90);
                        break;
                    case ClickSides.FaceLeft:
                        model.BeginRotation(new Vector3D(0, 1, 0), 90);
                        break;
                    case ClickSides.FaceTop:
                        model.BeginRotation(new Vector3D(1, 0, 0), 90);
                        break;
                    case ClickSides.FaceBottom:
                        model.BeginRotation(new Vector3D(1, 0, 0), -90);
                        break;
                    case ClickSides.EdgeFrontRight:
                        model.BeginRotation(new Vector3D(0, 1, 0), -45);
                        break;
                    case ClickSides.EdgeFrontLeft:
                        model.BeginRotation(new Vector3D(0, 1, 0), 45);
                        break;
                    case ClickSides.EdgeBackRight:
                        model.BeginRotation(new Vector3D(0, 1, 0), -135);
                        break;
                    case ClickSides.EdgeBackLeft:
                        model.BeginRotation(new Vector3D(0, 1, 0), 135);
                        break;
                    case ClickSides.EdgeTopRight:
                        {
                            var rotation = new Quaternion(new Vector3D(0, 1, 0), -90) * new Quaternion(new Vector3D(0, 0, 1), -45);
                            model.BeginRotation(rotation.Axis, rotation.Angle);
                        }
                        break;
                    case ClickSides.EdgeTopLeft:
                        {
                            var rotation = new Quaternion(new Vector3D(0, 1, 0), 90) * new Quaternion(new Vector3D(0, 0, 1), 45);
                            model.BeginRotation(rotation.Axis, rotation.Angle);
                        }
                        break;
                    case ClickSides.EdgeTopBack:
                        {
                            var rotation = new Quaternion(new Vector3D(0, 1, 0), 180) * new Quaternion(new Vector3D(1, 0, 0), -45);
                            model.BeginRotation(rotation.Axis, rotation.Angle);
                        }
                        break;
                    case ClickSides.EdgeTopFront:
                        model.BeginRotation(new Vector3D(1, 0, 0), 45);
                        break;
                    case ClickSides.EdgeBottomRight:
                        {
                            var rotation = new Quaternion(new Vector3D(0, 1, 0), -90) * new Quaternion(new Vector3D(0, 0, 1), 45);
                            model.BeginRotation(rotation.Axis, rotation.Angle);
                        }
                        break;
                    case ClickSides.EdgeBottomLeft:
                        {
                            var rotation = new Quaternion(new Vector3D(0, 1, 0), 90) * new Quaternion(new Vector3D(0, 0, 1), -45);
                            model.BeginRotation(rotation.Axis, rotation.Angle);
                        }
                        break;
                    case ClickSides.EdgeBottomBack:
                        {
                            var rotation = new Quaternion(new Vector3D(0, 1, 0), 180) * new Quaternion(new Vector3D(1, 0, 0), 45);
                            model.BeginRotation(rotation.Axis, rotation.Angle);
                        }
                        break;
                    case ClickSides.EdgeBottomFront:
                        model.BeginRotation(new Vector3D(1, 0, 0), -45);
                        break;
                    case ClickSides.VertexTopFrontRight:
                        {
                            var rotation = new Quaternion(new Vector3D(1, 0, 0), 45) * new Quaternion(new Vector3D(0, 1, 0), -45);
                            model.BeginRotation(rotation.Axis, rotation.Angle);
                        }
                        break;
                    case ClickSides.VertexTopFrontLeft:
                        {
                            var rotation = new Quaternion(new Vector3D(1, 0, 0), 45) * new Quaternion(new Vector3D(0, 1, 0), 45);
                            model.BeginRotation(rotation.Axis, rotation.Angle);
                        }
                        break;
                    case ClickSides.VertexTopBackRight:
                        {
                            var rotation = new Quaternion(new Vector3D(1, 0, 0), 45) * new Quaternion(new Vector3D(0, 1, 0), -135);
                            model.BeginRotation(rotation.Axis, rotation.Angle);
                        }
                        break;
                    case ClickSides.VertexTopBackLeft:
                        {
                            var rotation = new Quaternion(new Vector3D(1, 0, 0), 45) * new Quaternion(new Vector3D(0, 1, 0), 135);
                            model.BeginRotation(rotation.Axis, rotation.Angle);
                        }
                        break;
                    case ClickSides.VertexBottomFrontRight:
                        {
                            var rotation = new Quaternion(new Vector3D(1, 0, 0), -45) * new Quaternion(new Vector3D(0, 1, 0), -45);
                            model.BeginRotation(rotation.Axis, rotation.Angle);
                        }
                        break;
                    case ClickSides.VertexBottomFrontLeft:
                        {
                            var rotation = new Quaternion(new Vector3D(1, 0, 0), -45) * new Quaternion(new Vector3D(0, 1, 0), 45);
                            model.BeginRotation(rotation.Axis, rotation.Angle);
                        }
                        break;
                    case ClickSides.VertexBottomBackRight:
                        {
                            var rotation = new Quaternion(new Vector3D(1, 0, 0), -45) * new Quaternion(new Vector3D(0, 1, 0), -135);
                            model.BeginRotation(rotation.Axis, rotation.Angle);
                        }
                        break;
                    case ClickSides.VertexBottomBackLeft:
                        {
                            var rotation = new Quaternion(new Vector3D(1, 0, 0), -45) * new Quaternion(new Vector3D(0, 1, 0), 135);
                            model.BeginRotation(rotation.Axis, rotation.Angle);
                        }
                        break;
                    default:
                        break;
                }

            }
        }
        private static double GetDesiredCamDistance(Size displaySize, double objectSizeX, double objectSizeY, double fieldOfView)
        {

            float ratio = 1;

            if (displaySize.Height < displaySize.Width && displaySize.Width > 0)
                ratio = (float)displaySize.Height / (float)displaySize.Width;

            var size = objectSizeX > objectSizeY ? objectSizeX : objectSizeY;

            var alpha = MeshBuilder.DegToRad(fieldOfView / 2);




            var result = (size) / (2 * Math.Tan(alpha));


            return result;
        }
    }
}
