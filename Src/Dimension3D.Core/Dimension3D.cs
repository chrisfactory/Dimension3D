using Dimension3D.Core.Tools;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Diagnostics.Tracing;
using System.Dynamic;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
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


        internal static RootModelVisual3DContext? GetModelContext(Viewport3D? target)
        {
            if (target == null)
                return null;

            return GetItemsControl(target)?.ModelContext;
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

            private readonly RootModelVisual3DContext _modelContext;
            public VPItemsControl(Viewport3D vp)
            {
                NameScope.SetNameScope(this, new NameScope());
                _vp = vp;
                _root = new ModelVisual3D();
                vp.Loaded += Vp_Loaded;
                vp.Unloaded += Vp_Unloaded;
                if (vp.IsLoaded)
                    vp.Children.Add(_root);


                var transform = new Transform3DGroup();


                var rotation = new QuaternionRotation3D();
                var rotationTransform = new RotateTransform3D(rotation);
                transform.Children.Add(rotationTransform);
                RegisterName("rotation", rotation);

                var scale = new ScaleTransform3D();
                transform.Children.Add(scale);

                var translate = new TranslateTransform3D();
                transform.Children.Add(translate);

                _root.Transform = transform;
                _modelContext = new RootModelVisual3DContext(rotationTransform, this, rotation, scale, translate);
            }






            internal RootModelVisual3DContext ModelContext { get { return _modelContext; } }

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
    internal class RootModelVisual3DContext
    {
        private readonly Storyboard _StoryBoard = new Storyboard();
        private readonly FrameworkElement _control;
        private readonly QuaternionRotation3D _rotation;
        private readonly QuaternionAnimation _rotationAnimation;
        private readonly ScaleTransform3D _scale;
        private readonly TranslateTransform3D _translate;
        RotateTransform3D _tr;
        public RootModelVisual3DContext(RotateTransform3D tr,
            FrameworkElement control,
            QuaternionRotation3D rotation,
            ScaleTransform3D scale,
            TranslateTransform3D translate)
        {
            _tr = tr;
            _control = control;
            _rotation = rotation;
            _scale = scale;
            _translate = translate;
            _StoryBoard.Completed += StoryboardRotationAnimation_Completed;
            this._rotationAnimation = new QuaternionAnimation();
            _rotationAnimation.AutoReverse = false;

            Storyboard.SetTargetName(_rotationAnimation, "rotation");
            Storyboard.SetTargetProperty(_rotationAnimation, new PropertyPath(QuaternionRotation3D.QuaternionProperty));
            _StoryBoard.Children.Add(_rotationAnimation);
        }

        private void StoryboardRotationAnimation_Completed(object? sender, EventArgs e)
        {

        }

        public void BeginRotation(Vector3D desiredAxis, double desiredAngle, TimeSpan? ts = null)
        {
            if (ts == null)
                _rotationAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(600));
            else
                _rotationAnimation.Duration = new Duration(ts.Value);
            _rotationAnimation.To = new Quaternion(desiredAxis, desiredAngle);
            _StoryBoard.Begin(_control, true);
        }

        public void ZoomEvent(MouseWheelEventArgs e)
        {
            double delta = -1 * e.Delta / 7000.0;
            double scale = Math.Exp(delta);

            _scale.ScaleX *= scale;
            _scale.ScaleY *= scale;
            _scale.ScaleZ *= scale;
        }




        private bool _LeftDown;
        private bool _RightDown;
        private Vector3D _previousPosition3D;
        private Point _previousPosition2D;
        public void MouseMove(Viewport3D? vp, MouseEventArgs e)
        {
            if (vp == null) return;
            Point currentPosition = e.GetPosition(vp);
            CheckStateButtonLeft(e);
            CheckStateButtonRight(e);

            if (_LeftDown)
            {

                if (_previousPosition3D.Length == 0)
                    return;

                Vector3D currentPosition3D = ProjectToTrackball(vp.ActualWidth, vp.ActualHeight, currentPosition);
                Vector3D axis = Vector3D.CrossProduct(_previousPosition3D, currentPosition3D);
                double angle = Vector3D.AngleBetween(_previousPosition3D, currentPosition3D) * 4;
                if (angle == 0.0)
                    return;

                var delta = new Quaternion(axis, angle);
                if (!delta.IsIdentity)
                {
                    var newQ = _rotation.Quaternion * delta;
                    //_rotation.Quaternion = newQ;
                    BeginRotation(newQ.Axis, newQ.Angle, TimeSpan.Zero);
                }
                _previousPosition3D = currentPosition3D;

            }

            if (_RightDown)
            {


                Pan(vp, currentPosition);
            }

            _previousPosition2D = currentPosition;
        }
        private Vector3D ProjectToTrackball(double width, double height, Point point)
        {

            double x = point.X / (width / 2);    // Scale so bounds map to [0,0] - [2,2]
            double y = point.Y / (height / 2);

            x = x - 1;                           // Translate 0,0 to the center
            y = 1 - y;                           // Flip so +Y is up instead of down

            double z2 = 1 - x * x - y * y;       // z^2 = 1 - x^2 - y^2
            double z = z2 > 0 ? Math.Sqrt(z2) : 0;

            return new Vector3D(x, y, z);

        }
        private void Pan(Viewport3D vp, Point currentPosition)
        {
            Vector3D currentPosition3D = ProjectToTrackball(vp.ActualWidth, vp.ActualHeight, currentPosition);

            Vector change = Point.Subtract(_previousPosition2D, currentPosition);

            Vector3D changeVector = new Vector3D(change.X * -0.5, change.Y * -0.5, 0);

            _translate.OffsetX += changeVector.X * .1;
            _translate.OffsetY -= changeVector.Y * .1;
            _translate.OffsetZ += changeVector.Z * .1;

            _previousPosition3D = currentPosition3D;
        }


        public void MouseDown(Viewport3D? vp, MouseButtonEventArgs e)
        {
            if (vp == null) return;

            if (!_RightDown && e.LeftButton == MouseButtonState.Pressed)
            {
                e.MouseDevice.Capture(vp);
                e.Handled = true;

                _LeftDown = true;
                Point pos = e.GetPosition(vp);
                _previousPosition3D = ProjectToTrackball(vp.ActualWidth, vp.ActualHeight, pos);
                //_leftLastPos = new Point(pos.X - vp.ActualWidth / 2, vp.ActualHeight / 2 - pos.Y);
            }

            if (!_LeftDown && e.RightButton == MouseButtonState.Pressed)
            {
                e.MouseDevice.Capture(vp);
                e.Handled = true;

                _RightDown = true;
                Point pos = e.GetPosition(vp);
                _previousPosition3D = ProjectToTrackball(vp.ActualWidth, vp.ActualHeight, pos);
                //_rightLastPos = new Point(pos.X - vp.ActualWidth / 2, vp.ActualHeight / 2 - pos.Y);
            }
        }

        public void MouseUp(MouseButtonEventArgs e)
        {


            var any = _LeftDown || _RightDown;
            CheckStateButtonLeft(e);
            CheckStateButtonRight(e);

            if (any && !_LeftDown && !_RightDown)
                e.MouseDevice.Captured?.ReleaseMouseCapture();

        }

        private void CheckStateButtonRight(MouseEventArgs e)
        {
            if (_RightDown && e.RightButton == MouseButtonState.Released)
                _RightDown = false;
        }

        private void CheckStateButtonLeft(MouseEventArgs e)
        {
            if (_LeftDown && e.LeftButton == MouseButtonState.Released)
                _LeftDown = false;
        }
    }

}
