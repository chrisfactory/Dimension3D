using Dimension3D.Core.Tools;
using System;
using System.ComponentModel;
using System.Security;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Dimension3D.Core
{
    [DefaultEvent("Click")]
    [Localizability(LocalizationCategory.Button)]
    public abstract class DimensionButtonBase3D : DimensionModelVisual3D, ICommandSource
    {
        private static readonly Type _typeofThis = typeof(DimensionButtonBase3D);
        public static readonly RoutedEvent ClickEvent;
        static DimensionButtonBase3D()
        {
            ClickEvent = EventManager.RegisterRoutedEvent("Click", RoutingStrategy.Bubble, typeof(RoutedEventHandler), _typeofThis);
        }
        protected DimensionButtonBase3D() : base()
        {
        }
 

        #region Virtual methods
        /// <summary>
        /// This virtual method is called when button is clicked and it raises the Click event
        /// </summary>
        protected virtual void OnClick()
        {
            RaiseEvent(new RoutedEventArgs(ClickEvent, this));
            CommandHelpers.ExecuteCommandSource(this);
        }


        /// <summary>
        ///     This method is invoked when the IsPressed property changes.
        /// </summary>
        /// <param name="e">DependencyPropertyChangedEventArgs.</param>
        protected virtual void OnIsPressedChanged(DependencyPropertyChangedEventArgs e)
        {
        }

        #endregion Virtual methods

        #region Private helpers

        private bool IsInMainFocusScope
        {
            get
            {
                Visual? focusScope = FocusManager.GetFocusScope(this) as Visual;
                return focusScope == null || VisualTreeHelper.GetParent(focusScope) == null;
            }
        }

        /// <summary>
        /// This method is called when button is clicked via IInvokeProvider.
        /// </summary>
        internal void AutomationButtonBaseClick()
        {
            OnClick();
        }

        private static bool IsValidClickMode(object o)
        {
            ClickMode value = (ClickMode)o;
            return value == ClickMode.Press
                || value == ClickMode.Release
                || value == ClickMode.Hover;
        }



        ///// <summary>
        ///// Override for <seealso cref="UIElement.OnRenderSizeChanged"/>
        ///// </summary>
        //protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        //{
        //    base.OnRenderSizeChanged(sizeInfo);

        //    // *** Workaround ***
        //    // We need OnMouseRealyOver Property here
        //    //
        //    // There is a problem when Button is capturing the Mouse and resizing untill the mouse fall of the Button
        //    // During that time, Button and Mouse didn't really move. However, we need to update the IsPressed property
        //    // because mouse is no longer over the button.
        //    // We migth need a new property called *** IsMouseReallyOver *** property, so we can update IsPressed when
        //    // it's changed. (Can't use IsMouseOver or IsMouseDirectlyOver 'coz once Mouse is captured, they're alway 'true'.
        //    //

        //    // Update IsPressed property
        //    if (Mouse.PrimaryDevice.LeftButton == MouseButtonState.Pressed)
        //    {
        //        // At this point, RenderSize is not updated. We must use finalSize instead.
        //        UpdateIsPressed();
        //    }
        //}

        /// <summary>
        ///     Called when IsPressedProperty is changed on "d."
        /// </summary>
        private static void OnIsPressedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DimensionButtonBase3D ctrl = (DimensionButtonBase3D)d;
            ctrl.OnIsPressedChanged(e);
        }

        private static void OnAccessKeyPressed(object sender, AccessKeyPressedEventArgs e)
        {
            if (!e.Handled && e.Scope == null && e.Target == null)
            {
                e.Target = (UIElement)sender;
            }
        }

        private void UpdateIsPressed()
        {


            //if (IsMouseOver)
            //{
            //    if (!IsPressed)
            //    {
            //        SetIsPressed(true);
            //    }
            //}
            //else if (IsPressed)
            //{
            //    SetIsPressed(false);
            //}
        }

        #endregion Private helpers

        #region Properties and Events



        /// <summary>
        /// Add / Remove ClickEvent handler
        /// </summary>
        [Category("Behavior")]
        public event RoutedEventHandler Click { add { AddHandler(ClickEvent, value); } remove { RemoveHandler(ClickEvent, value); } }

        /// <summary>
        ///     The DependencyProperty for RoutedCommand
        /// </summary>
        //[CommonDependencyProperty]
        public static readonly DependencyProperty CommandProperty =
                DependencyProperty.Register(
                        "Command",
                        typeof(ICommand),
                        _typeofThis,
                        new FrameworkPropertyMetadata((ICommand)null,
                            new PropertyChangedCallback(OnCommandChanged)));

        /// <summary>
        /// The DependencyProperty for the CommandParameter
        /// </summary>
        //[CommonDependencyProperty]
        public static readonly DependencyProperty CommandParameterProperty =
                DependencyProperty.Register(
                        "CommandParameter",
                        typeof(object),
                        _typeofThis,
                        new FrameworkPropertyMetadata((object)null));

        /// <summary>
        ///     The DependencyProperty for Target property
        ///     Flags:              None
        ///     Default Value:      null
        /// </summary>
        //[CommonDependencyProperty]
        public static readonly DependencyProperty CommandTargetProperty =
                DependencyProperty.Register(
                        "CommandTarget",
                        typeof(IInputElement),
                        _typeofThis,
                        new FrameworkPropertyMetadata((IInputElement)null));

        /// <summary>
        ///     The key needed set a read-only property.
        /// </summary>
        internal static readonly DependencyPropertyKey IsPressedPropertyKey =
                DependencyProperty.RegisterReadOnly(
                        "IsPressed",
                        typeof(bool),
                        _typeofThis,
                        new FrameworkPropertyMetadata(false,
                                new PropertyChangedCallback(OnIsPressedChanged)));

        /// <summary>
        ///     The DependencyProperty for the IsPressed property.
        ///     Flags:              None
        ///     Default Value:      false
        /// </summary>
        //[CommonDependencyProperty]
        public static readonly DependencyProperty IsPressedProperty =
                IsPressedPropertyKey.DependencyProperty;

        /// <summary>
        ///     IsPressed is the state of a button indicates that left mouse button is pressed or space key is pressed over the button.
        /// </summary>
        [Browsable(false), Category("Appearance"), ReadOnly(true)]
        public bool IsPressed
        {
            get { return (bool)GetValue(IsPressedProperty); }
            protected set { SetValue(IsPressedPropertyKey, value); }
        }

        internal protected void SetIsPressed(bool pressed)
        {
            if (pressed)
            {
                SetValue(IsPressedPropertyKey, pressed);
            }
            else
            {
                ClearValue(IsPressedPropertyKey);
            }
        }

        /// <summary>
        /// Get or set the Command property
        /// </summary>
        [Bindable(true), Category("Action")]
        [Localizability(LocalizationCategory.NeverLocalize)]
        public ICommand Command
        {
            get
            {
                return (ICommand)GetValue(CommandProperty);
            }
            set
            {
                SetValue(CommandProperty, value);
            }
        }

        private static void OnCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DimensionButtonBase3D b = (DimensionButtonBase3D)d;
            b.OnCommandChanged((ICommand)e.OldValue, (ICommand)e.NewValue);
        }

        private void OnCommandChanged(ICommand oldCommand, ICommand newCommand)
        {
            if (oldCommand != null)
            {
                UnhookCommand(oldCommand);
            }
            if (newCommand != null)
            {
                HookCommand(newCommand);
            }
        }

        private void UnhookCommand(ICommand command)
        {
            CanExecuteChangedEventManager.RemoveHandler(command, OnCanExecuteChanged);
            UpdateCanExecute();
        }

        private void HookCommand(ICommand command)
        {
            CanExecuteChangedEventManager.AddHandler(command, OnCanExecuteChanged);
            UpdateCanExecute();
        }

        private void OnCanExecuteChanged(object sender, EventArgs e)
        {
            UpdateCanExecute();
        }

        private void UpdateCanExecute()
        {
            if (Command != null)
            {
                CanExecute = CommandHelpers.CanExecuteCommandSource(this);
            }
            else
            {
                CanExecute = true;
            }
        }

        /// <summary>
        ///     Fetches the value of the IsEnabled property
        /// </summary>
        /// <remarks>
        ///     The reason this property is overridden is so that Button
        ///     can infuse the value for CanExecute into it.
        /// </remarks>
        protected override bool IsEnabledCore
        {
            get
            {
                return base.IsEnabledCore && CanExecute;
            }
        }

        /// <summary>
        /// Reflects the parameter to pass to the CommandProperty upon execution.
        /// </summary>
        [Bindable(true), Category("Action")]
        [Localizability(LocalizationCategory.NeverLocalize)]
        public object CommandParameter
        {
            get
            {
                return GetValue(CommandParameterProperty);
            }
            set
            {
                SetValue(CommandParameterProperty, value);
            }
        }

        /// <summary>
        ///     The target element on which to fire the command.
        /// </summary>
        [Bindable(true), Category("Action")]
        public IInputElement CommandTarget
        {
            get
            {
                return (IInputElement)GetValue(CommandTargetProperty);
            }
            set
            {
                SetValue(CommandTargetProperty, value);
            }
        }

        /// <summary>
        ///     The DependencyProperty for the ClickMode property.
        ///     Flags:              None
        ///     Default Value:      ClickMode.Release
        /// </summary>
        public static readonly DependencyProperty ClickModeProperty =
                DependencyProperty.Register(
                        "ClickMode",
                        typeof(ClickMode),
                        _typeofThis,
                        new FrameworkPropertyMetadata(ClickMode.Release),
                        new ValidateValueCallback(IsValidClickMode));


        /// <summary>
        ///     ClickMode specify when the Click event should fire
        /// </summary>
        [Bindable(true), Category("Behavior")]
        public ClickMode ClickMode
        {
            get
            {
                return (ClickMode)GetValue(ClickModeProperty);
            }
            set
            {
                SetValue(ClickModeProperty, value);
            }
        }

        #endregion Properties and Events

        #region Override methods
        /// <summary>
        /// This is the method that responds to the MouseButtonEvent event.
        /// </summary>
        /// <param name="e">Event arguments</param>
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            // Ignore when in hover-click mode.
            if (ClickMode != ClickMode.Hover)
            {
                e.Handled = true;


                // It is possible that the mouse state could have changed during all of
                // the call-outs that have happened so far.
                if (e.ButtonState == MouseButtonState.Pressed)
                    SetIsPressed(true);


                if (ClickMode == ClickMode.Press)
                {
                    bool exceptionThrown = true;
                    try
                    {
                        OnClick();
                        exceptionThrown = false;
                    }
                    finally
                    {
                        if (exceptionThrown)
                        {
                            // Cleanup the buttonbase state
                            SetIsPressed(false);
                            ReleaseMouseCapture();
                        }
                    }
                }
            }

            base.OnMouseLeftButtonDown(e);
        }

        /// <summary>
        /// This is the method that responds to the MouseButtonEvent event.
        /// </summary>
        /// <param name="e">Event arguments</param>
        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            // Ignore when in hover-click mode.
            if (ClickMode != ClickMode.Hover)
            {
                e.Handled = true;
                bool shouldClick = IsPressed && ClickMode == ClickMode.Release;

                if (shouldClick)
                {
                    OnClick();
                    SetIsPressed(false);
                }
            }

            base.OnMouseLeftButtonUp(e);
        }

        /// <summary>
        /// This is the method that responds to the MouseEvent event.
        /// </summary>
        /// <param name="e">Event arguments</param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if ((ClickMode != ClickMode.Hover) && e.LeftButton == MouseButtonState.Pressed)
            {
                UpdateIsPressed();

                e.Handled = true;
            }
        }


        /// <summary>
        ///     An event reporting the mouse entered this element.
        /// </summary>
        /// <param name="e">Event arguments</param>
        protected override void OnMouseEnter(MouseEventArgs e)
        {
            base.OnMouseEnter(e);
            if (HandleIsMouseOverChanged())
            {
                e.Handled = true;
            }
        }

        /// <summary>
        ///     An event reporting the mouse left this element.
        /// </summary>
        /// <param name="e">Event arguments</param>
        protected override void OnMouseLeave(MouseEventArgs e)
        {
            IsPressed = false;
            base.OnMouseLeave(e);
            if (HandleIsMouseOverChanged())
            {
                e.Handled = true;
            }
        }

        /// <summary>
        ///     An event reporting that the IsMouseOver property changed.
        /// </summary>
        private bool HandleIsMouseOverChanged()
        {

            if (ClickMode == ClickMode.Hover)
            {
                if (IsMouseOver)
                {
                    // Hovering over the button will click in the OnHover click mode
                    SetIsPressed(true);
                    OnClick();
                }
                else
                {
                    SetIsPressed(false);
                }

                return true;
            }

            return false;
        }




        /// <SecurityNote>
        /// Critical - calling critical InputManager.Current
        /// Safe - InputManager.Current is not exposed and used temporary to determine the mouse state
        /// </SecurityNote>
        [SecurityCritical, SecuritySafeCritical]
        private bool GetMouseLeftButtonReleased()
        {
            return InputManager.Current.PrimaryMouseDevice.LeftButton == MouseButtonState.Released;
        }



        private bool _CanExecute;
        private bool CanExecute
        {
            get => _CanExecute; set
            {
                if (_CanExecute != value)
                {
                    CoerceValue(IsEnabledProperty);

                }
            }
        }
        #endregion
    }
}
