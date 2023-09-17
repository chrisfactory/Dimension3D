using Dimension3D.Core.Tools;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Media3D;

namespace Dimension3D.Core
{
    [TemplatePart(Name = "PART_VIEWPORT3D", Type = typeof(Viewport3D))]
    public class DimensionCameramanViewer : Control
    {
        private static Type _typeofThis = typeof(DimensionCameramanViewer);
        public static readonly DependencyProperty ViewportProperty;

        static DimensionCameramanViewer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(_typeofThis, new FrameworkPropertyMetadata(_typeofThis));
            ViewportProperty = DependencyProperty.Register(nameof(Viewport), typeof(Viewport3D), _typeofThis, new FrameworkPropertyMetadata());
        }




        public Viewport3D Viewport { get => (Viewport3D)GetValue(ViewportProperty); set => SetValue(ViewportProperty, value); }






        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            var cameraman = this.Template.FindName("PART_CAMERA_PILOT", this) as DimensionCameraman;
            if (cameraman != null)
            {
                WeakEventManager<DimensionCameraman, CameramanRoutedEventArgs>.AddHandler(cameraman, "Click", OnFaceClickEvent);

            }

        }

        private void OnFaceClickEvent(object? sender, CameramanRoutedEventArgs e)
        {
            var vp = Viewport;
            if (vp != null)
            {
                var cam = vp.Camera as PerspectiveCamera;
              
                var model = Dimension3D.GetRootVisualModel(Viewport);
                if (model != null && cam != null)
                {
                    cam.Position = new Point3D(0, 0, 7000);
                    var transform = new Transform3DGroup();
                    var rot = new RotateTransform3D();
                    transform.Children.Add(rot);


                    switch (e.Side)
                    {
                        case ClickSides.Front:
                            rot.Rotation = new AxisAngleRotation3D(new Vector3D(0, 0, 0), 0);
                            break;
                        case ClickSides.Right:
                            rot.Rotation = new AxisAngleRotation3D(new Vector3D(0, -1, 0), 90);
                            break;
                        case ClickSides.Back:
                            rot.Rotation = new AxisAngleRotation3D(new Vector3D(0, -1, 0), 180);
                            break;
                        case ClickSides.Left:
                            rot.Rotation = new AxisAngleRotation3D(new Vector3D(0, 1, 0), 90);
                            break;
                        case ClickSides.Bottom:
                            rot.Rotation = new AxisAngleRotation3D(new Vector3D(-1, 0, 0), 90);
                            break;
                        case ClickSides.Top:
                            rot.Rotation = new AxisAngleRotation3D(new Vector3D(1, 0, 0), 90);
                            break;
                        default:
                            break;
                    }
                    model.Transform = transform;
                }
            }
        }
    }
}
