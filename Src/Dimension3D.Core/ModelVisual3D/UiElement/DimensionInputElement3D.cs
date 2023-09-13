using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Media3D;

namespace Dimension3D.Core
{
    internal sealed class DimensionInputElement3D : UIElement3D
    { 
        private static readonly Type _typeofThis = typeof(DimensionInputElement3D);

        public static readonly DependencyProperty Model3DProperty;
        static DimensionInputElement3D()
        {
            Model3DProperty = DependencyProperty.Register(nameof(Model), typeof(Model3D), _typeofThis, new FrameworkPropertyMetadata<DimensionInputElement3D>(Model3DPropertyChangedCallback));
        }



        private DimensionVisual3D _target;

        public DimensionInputElement3D(DimensionVisual3D visual3D)
        {
            _target = visual3D;
        }

        internal Model3D? Model { get => (Model3D?)GetValue(Model3DProperty); set => SetValue(Model3DProperty, value); }

        private static void Model3DPropertyChangedCallback(DimensionInputElement3D d, DependencyPropertyChangedEventArgs e)
        {
            d.Visual3DModel = e.NewValue as Model3D;
            d.InvalidateModel();
        }


        private void OnRaiseEvent<T>(T e, [System.Runtime.CompilerServices.CallerMemberName] string memberName = "")
           where T : RoutedEventArgs => _target.RaiseEvent(e); 

        #region System.Windows.Input.Mouse
        protected override void OnPreviewMouseDown(MouseButtonEventArgs e)
        {
            OnRaiseEvent(e);
            base.OnPreviewMouseDown(e);
        }
        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            OnRaiseEvent(e);
            base.OnMouseDown(e);
        }
        protected override void OnPreviewMouseUp(MouseButtonEventArgs e)
        {
            OnRaiseEvent(e);
            base.OnPreviewMouseUp(e);
        }
        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            OnRaiseEvent(e);
            base.OnMouseUp(e);
        }
        protected override void OnPreviewMouseMove(MouseEventArgs e)
        {
            OnRaiseEvent(e);
            base.OnPreviewMouseMove(e);
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            OnRaiseEvent(e);
            base.OnMouseMove(e);
        }
        protected override void OnPreviewMouseWheel(MouseWheelEventArgs e)
        {
            OnRaiseEvent(e);
            base.OnPreviewMouseWheel(e);
        }
        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            OnRaiseEvent(e);
            base.OnMouseWheel(e);
        }
        protected override void OnMouseEnter(MouseEventArgs e)
        {
            OnRaiseEvent(e);
            base.OnMouseEnter(e);
        }
        protected override void OnMouseLeave(MouseEventArgs e)
        {
            OnRaiseEvent(e);
            base.OnMouseLeave(e);
        }
        protected override void OnGotMouseCapture(MouseEventArgs e)
        {
            OnRaiseEvent(e);
            base.OnGotMouseCapture(e);
        }
        protected override void OnLostMouseCapture(MouseEventArgs e)
        {
            OnRaiseEvent(e);
            base.OnLostMouseCapture(e);
        }
        protected override void OnQueryCursor(QueryCursorEventArgs e)
        {
            OnRaiseEvent(e);
            base.OnQueryCursor(e);
        }
        #endregion System.Windows.Input.Mouse

        //REPETITION AVEC Input.Mouse
        //#region System.Windows.UIElement
        //protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
        //{
        //    OnRaiseEvent(e);
        //    base.OnPreviewMouseLeftButtonDown(e);
        //}
        //protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        //{
        //    OnRaiseEvent(e);
        //    base.OnMouseLeftButtonDown(e);
        //}
        //protected override void OnPreviewMouseLeftButtonUp(MouseButtonEventArgs e)
        //{
        //    OnRaiseEvent(e);
        //    base.OnPreviewMouseLeftButtonUp(e);
        //}
        //protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        //{
        //    OnRaiseEvent(e);
        //    base.OnMouseLeftButtonUp(e);
        //}
        //protected override void OnPreviewMouseRightButtonDown(MouseButtonEventArgs e)
        //{
        //    OnRaiseEvent(e);
        //    base.OnPreviewMouseRightButtonDown(e);
        //}
        //protected override void OnMouseRightButtonDown(MouseButtonEventArgs e)
        //{
        //    OnRaiseEvent(e);
        //    base.OnMouseRightButtonDown(e);
        //}
        //protected override void OnPreviewMouseRightButtonUp(MouseButtonEventArgs e)
        //{
        //    OnRaiseEvent(e);
        //    base.OnPreviewMouseRightButtonUp(e);
        //}
        //protected override void OnMouseRightButtonUp(MouseButtonEventArgs e)
        //{
        //    OnRaiseEvent(e);
        //    base.OnMouseRightButtonUp(e);
        //}
        //#endregion System.Windows.UIElement

        #region System.Windows.Input.Stylus
        protected override void OnPreviewStylusDown(StylusDownEventArgs e)
        {
            OnRaiseEvent(e);
            base.OnPreviewStylusDown(e);
        }
        protected override void OnStylusDown(StylusDownEventArgs e)
        {
            OnRaiseEvent(e);
            base.OnStylusDown(e);
        }
        protected override void OnPreviewStylusUp(StylusEventArgs e)
        {
            OnRaiseEvent(e);
            base.OnPreviewStylusUp(e);
        }
        protected override void OnStylusUp(StylusEventArgs e)
        {
            OnRaiseEvent(e);
            base.OnStylusUp(e);
        }
        protected override void OnPreviewStylusMove(StylusEventArgs e)
        {
            OnRaiseEvent(e);
            base.OnPreviewStylusMove(e);
        }
        protected override void OnStylusMove(StylusEventArgs e)
        {
            OnRaiseEvent(e);
            base.OnStylusMove(e);
        }
        protected override void OnPreviewStylusInAirMove(StylusEventArgs e)
        {
            OnRaiseEvent(e);
            base.OnPreviewStylusInAirMove(e);
        }
        protected override void OnStylusInAirMove(StylusEventArgs e)
        {
            OnRaiseEvent(e);
            base.OnStylusInAirMove(e);
        }
        protected override void OnStylusEnter(StylusEventArgs e)
        {
            OnRaiseEvent(e);
            base.OnStylusEnter(e);
        }
        protected override void OnStylusLeave(StylusEventArgs e)
        {
            OnRaiseEvent(e);
            base.OnStylusLeave(e);
        }
        protected override void OnPreviewStylusInRange(StylusEventArgs e)
        {
            OnRaiseEvent(e);
            base.OnPreviewStylusInRange(e);
        }
        protected override void OnStylusInRange(StylusEventArgs e)
        {
            OnRaiseEvent(e);
            base.OnStylusInRange(e);
        }
        protected override void OnPreviewStylusOutOfRange(StylusEventArgs e)
        {
            OnRaiseEvent(e);
            base.OnPreviewStylusOutOfRange(e);
        }
        protected override void OnStylusOutOfRange(StylusEventArgs e)
        {
            OnRaiseEvent(e);
            base.OnStylusOutOfRange(e);
        }
        protected override void OnPreviewStylusSystemGesture(StylusSystemGestureEventArgs e)
        {
            OnRaiseEvent(e);
            base.OnPreviewStylusSystemGesture(e);
        }
        protected override void OnStylusSystemGesture(StylusSystemGestureEventArgs e)
        {
            OnRaiseEvent(e);
            base.OnStylusSystemGesture(e);
        }
        protected override void OnGotStylusCapture(StylusEventArgs e)
        {
            OnRaiseEvent(e);
            base.OnGotStylusCapture(e);
        }
        protected override void OnLostStylusCapture(StylusEventArgs e)
        {
            OnRaiseEvent(e);
            base.OnLostStylusCapture(e);
        }
        protected override void OnStylusButtonDown(StylusButtonEventArgs e)
        {
            OnRaiseEvent(e);
            base.OnStylusButtonDown(e);
        }
        protected override void OnStylusButtonUp(StylusButtonEventArgs e)
        {
            OnRaiseEvent(e);
            base.OnStylusButtonUp(e);
        }
        protected override void OnPreviewStylusButtonDown(StylusButtonEventArgs e)
        {
            OnRaiseEvent(e);
            base.OnPreviewStylusButtonDown(e);
        }
        protected override void OnPreviewStylusButtonUp(StylusButtonEventArgs e)
        {
            OnRaiseEvent(e);
            base.OnPreviewStylusButtonUp(e);
        }
        #endregion System.Windows.Input.Stylus

        #region System.Windows.Input.Keyboard
        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            OnRaiseEvent(e);
            base.OnPreviewKeyDown(e);
        }
        protected override void OnKeyDown(KeyEventArgs e)
        {
            OnRaiseEvent(e);
            base.OnKeyDown(e);
        }
        protected override void OnPreviewKeyUp(KeyEventArgs e)
        {
            OnRaiseEvent(e);
            base.OnPreviewKeyUp(e);
        }
        protected override void OnKeyUp(KeyEventArgs e)
        {
            OnRaiseEvent(e);
            base.OnKeyUp(e);
        }
        protected override void OnPreviewGotKeyboardFocus(KeyboardFocusChangedEventArgs e)
        {
            OnRaiseEvent(e);
            base.OnPreviewGotKeyboardFocus(e);
        }
        protected override void OnGotKeyboardFocus(KeyboardFocusChangedEventArgs e)
        {
            OnRaiseEvent(e);
            base.OnGotKeyboardFocus(e);
        }
        protected override void OnPreviewLostKeyboardFocus(KeyboardFocusChangedEventArgs e)
        {
            OnRaiseEvent(e);
            base.OnPreviewLostKeyboardFocus(e);
        }
        protected override void OnLostKeyboardFocus(KeyboardFocusChangedEventArgs e)
        {
            OnRaiseEvent(e);
            base.OnLostKeyboardFocus(e);
        }
        #endregion System.Windows.Input.Keyboard

        #region System.Windows.Input.TextCompositionManager
        protected override void OnPreviewTextInput(TextCompositionEventArgs e)
        {
            OnRaiseEvent(e);
            base.OnPreviewTextInput(e);
        }
        protected override void OnTextInput(TextCompositionEventArgs e)
        {
            OnRaiseEvent(e);
            base.OnTextInput(e);
        }
        #endregion System.Windows.Input.TextCompositionManager

        #region System.Windows.DragDrop
        protected override void OnPreviewQueryContinueDrag(QueryContinueDragEventArgs e)
        {
            OnRaiseEvent(e);
            base.OnPreviewQueryContinueDrag(e);
        }
        protected override void OnQueryContinueDrag(QueryContinueDragEventArgs e)
        {
            OnRaiseEvent(e);
            base.OnQueryContinueDrag(e);
        }
        protected override void OnPreviewGiveFeedback(GiveFeedbackEventArgs e)
        {
            OnRaiseEvent(e);
            base.OnPreviewGiveFeedback(e);
        }
        protected override void OnGiveFeedback(GiveFeedbackEventArgs e)
        {
            OnRaiseEvent(e);
            base.OnGiveFeedback(e);
        }
        protected override void OnPreviewDragEnter(DragEventArgs e)
        {
            OnRaiseEvent(e);
            base.OnPreviewDragEnter(e);
        }
        protected override void OnDragEnter(DragEventArgs e)
        {
            OnRaiseEvent(e);
            base.OnDragEnter(e);
        }
        protected override void OnPreviewDragOver(DragEventArgs e)
        {
            OnRaiseEvent(e);
            base.OnPreviewDragOver(e);
        }
        protected override void OnDragOver(DragEventArgs e)
        {
            OnRaiseEvent(e);
            base.OnDragOver(e);
        }
        protected override void OnPreviewDragLeave(DragEventArgs e)
        {
            OnRaiseEvent(e);
            base.OnPreviewDragLeave(e);
        }
        protected override void OnDragLeave(DragEventArgs e)
        {
            OnRaiseEvent(e);
            base.OnDragLeave(e);
        }
        protected override void OnPreviewDrop(DragEventArgs e)
        {
            OnRaiseEvent(e);
            base.OnPreviewDrop(e);
        }
        protected override void OnDrop(DragEventArgs e)
        {
            OnRaiseEvent(e);
            base.OnDrop(e);
        }
        #endregion System.Windows.DragDrop

        #region System.Windows.Input.Touch
        protected override void OnPreviewTouchDown(TouchEventArgs e)
        {
            OnRaiseEvent(e);
            base.OnPreviewTouchDown(e);
        }
        protected override void OnTouchDown(TouchEventArgs e)
        {
            OnRaiseEvent(e);
            base.OnTouchDown(e);
        }
        protected override void OnPreviewTouchMove(TouchEventArgs e)
        {
            OnRaiseEvent(e);
            base.OnPreviewTouchMove(e);
        }
        protected override void OnTouchMove(TouchEventArgs e)
        {
            OnRaiseEvent(e);
            base.OnTouchMove(e);
        }
        protected override void OnPreviewTouchUp(TouchEventArgs e)
        {
            OnRaiseEvent(e);
            base.OnPreviewTouchUp(e);
        }
        protected override void OnTouchUp(TouchEventArgs e)
        {
            OnRaiseEvent(e);
            base.OnTouchUp(e);
        }
        protected override void OnGotTouchCapture(TouchEventArgs e)
        {
            OnRaiseEvent(e);
            base.OnGotTouchCapture(e);
        }
        protected override void OnLostTouchCapture(TouchEventArgs e)
        {
            OnRaiseEvent(e);
            base.OnLostTouchCapture(e);
        }
        protected override void OnTouchEnter(TouchEventArgs e)
        {
            OnRaiseEvent(e);
            base.OnTouchEnter(e);
        }
        protected override void OnTouchLeave(TouchEventArgs e)
        {
            OnRaiseEvent(e);
            base.OnTouchLeave(e);
        }
        #endregion System.Windows.Input.Touch

        #region System.Windows.Input.FocusManager
        protected override void OnGotFocus(RoutedEventArgs e)
        {
            OnRaiseEvent(e);
            base.OnGotFocus(e);
        }
        protected override void OnLostFocus(RoutedEventArgs e)
        {
            OnRaiseEvent(e);
            base.OnLostFocus(e);
        }
        #endregion System.Windows.Input.FocusManager



     
    }
}
