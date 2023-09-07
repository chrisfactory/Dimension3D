using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Input;
using System.Windows.Threading;
using System.Windows.Controls;

namespace Dimension3D.Core
{
    [DefaultEvent("Click")]
    [Localizability(LocalizationCategory.Button)]
    public class DimensionRepeatButton3D : DimensionButtonBase3D
    {
        private static readonly Type _typeofThis = typeof(DimensionRepeatButton3D);


        #region Constructors

        static DimensionRepeatButton3D()
        {

            ClickModeProperty.OverrideMetadata(_typeofThis, new FrameworkPropertyMetadata(ClickMode.Press));
        }

        /// <summary>
        ///     Default RepeatButton constructor
        /// </summary>
        /// <remarks>
        ///     Automatic determination of current Dispatcher. Use alternative constructor
        ///     that accepts a Dispatcher for best performance.
        /// </remarks>
        public DimensionRepeatButton3D() : base()
        {
        }

        #endregion

        #region Dependencies and Events

        /// <summary>
        ///     The Property for the Delay property.
        ///     Flags:              Can be used in style rules
        ///     Default Value:      Depend on SPI_GETKEYBOARDDELAY from SystemMetrics
        /// </summary>
        public static readonly DependencyProperty DelayProperty
            = DependencyProperty.Register("Delay", typeof(int), _typeofThis,
                                          new FrameworkPropertyMetadata(GetKeyboardDelay()),
                                          new ValidateValueCallback(IsDelayValid));

        /// <summary>
        ///     Specifies the amount of time, in milliseconds, to wait before repeating begins.
        /// Must be non-negative
        /// </summary>
        [Bindable(true), Category("Behavior")]
        public int Delay
        {
            get
            {
                return (int)GetValue(DelayProperty);
            }
            set
            {
                SetValue(DelayProperty, value);
            }
        }

        /// <summary>
        ///     The Property for the Interval property.
        ///     Flags:              Can be used in style rules
        ///     Default Value:      Depend on SPI_GETKEYBOARDSPEED from SystemMetrics
        /// </summary>
        public static readonly DependencyProperty IntervalProperty
            = DependencyProperty.Register("Interval", typeof(int), _typeofThis,
                                          new FrameworkPropertyMetadata(GetKeyboardSpeed()),
                                          new ValidateValueCallback(IsIntervalValid));

        /// <summary>
        ///     Specifies the amount of time, in milliseconds, between repeats once repeating starts.
        /// Must be non-negative
        /// </summary>
        [Bindable(true), Category("Behavior")]
        public int Interval
        {
            get
            {
                return (int)GetValue(IntervalProperty);
            }
            set
            {
                SetValue(IntervalProperty, value);
            }
        }

        #endregion Dependencies and Events

        #region Private helpers

        private static bool IsDelayValid(object value) { return ((int)value) >= 0; }
        private static bool IsIntervalValid(object value) { return ((int)value) > 0; }

        /// <summary>
        /// Starts a _timer ticking
        /// </summary>
        private void StartTimer()
        {
            if (_timer == null)
            {
                _timer = new DispatcherTimer();
                _timer.Tick += new EventHandler(OnTimeout);
            }
            else if (_timer.IsEnabled)
                return;

            _timer.Interval = TimeSpan.FromMilliseconds(Delay);
            _timer.Start();
        }

        /// <summary>
        /// Stops a _timer that has already started
        /// </summary>
        private void StopTimer()
        {
            if (_timer != null)
            {
                _timer.Stop();
            }
        }



        /// <summary>
        /// This is the handler for when the repeat _timer expires. All we do
        /// is invoke a click.
        /// </summary>
        /// <param name="sender">Sender of the event</param>
        /// <param name="e">Event arguments</param>
        private void OnTimeout(object sender, EventArgs e)
        {
            TimeSpan interval = TimeSpan.FromMilliseconds(Interval);
            if (_timer.Interval != interval)
                _timer.Interval = interval;

            if (IsPressed)
            {
                try
                {
                    OnClick();

                }
                finally
                {
                }
            }
        }

        /// <summary>
        /// Retrieves the keyboard repeat-delay setting, which is a value in the range from 0
        /// (approximately 250 ms delay) through 3 (approximately 1 second delay).
        /// The actual delay associated with each value may vary depending on the hardware.
        /// </summary>
        /// <returns></returns>
        internal static int GetKeyboardDelay()
        {
            int delay = SystemParameters.KeyboardDelay;
            // SPI_GETKEYBOARDDELAY 0,1,2,3 correspond to 250,500,750,1000ms
            if (delay < 0 || delay > 3)
                delay = 0;
            return (delay + 1) * 250;
        }

        /// <summary>
        /// Retrieves the keyboard repeat-speed setting, which is a value in the range from 0
        /// (approximately 2.5 repetitions per second) through 31 (approximately 30 repetitions per second).
        /// The actual repeat rates are hardware-dependent and may vary from a linear scale by as much as 20%
        /// </summary>
        /// <returns></returns>
        internal static int GetKeyboardSpeed()
        {
            int speed = SystemParameters.KeyboardSpeed;
            // SPI_GETKEYBOARDSPEED 0,...,31 correspond to 1000/2.5=400,...,1000/30 ms
            if (speed < 0 || speed > 31)
                speed = 31;
            return (31 - speed) * (400 - 1000 / 30) / 31 + 1000 / 30;
        }

        #endregion Private helpers

        #region Override methods



        /// <summary>
        /// Raises InvokedAutomationEvent and call the base method to raise the Click event
        /// </summary>
        /// <ExternalAPI/>
        protected override void OnClick()
        {
            //if (AutomationPeer.ListenerExists(AutomationEvents.InvokePatternOnInvoked))
            //{
            //    AutomationPeer peer = UIElementAutomationPeer.CreatePeerForElement(this);
            //    if (peer != null)
            //        peer.RaiseAutomationEvent(AutomationEvents.InvokePatternOnInvoked);
            //}

            base.OnClick();
        }

        /// <summary>
        /// This is the method that responds to the MouseButtonEvent event.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            if (IsPressed && (ClickMode != ClickMode.Hover))
            {
                StartTimer();
            }
        }

        /// <summary>
        /// This is the method that responds to the MouseButtonEvent event.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);

            if (ClickMode != ClickMode.Hover)
            {
                StopTimer();
            }
        }

        /// <summary>
        ///     Called when this element loses mouse capture.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLostMouseCapture(MouseEventArgs e)
        {
            base.OnLostMouseCapture(e);
            StopTimer();
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
                    StartTimer();
                }
                else
                {
                    StopTimer();
                }

                return true;
            }

            return false;
        }

        /// <summary>
        /// This is the method that responds to the KeyDown event.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if ((e.Key == Key.Space) && (ClickMode != ClickMode.Hover))
            {
                StartTimer();
            }
        }

        /// <summary>
        /// This is the method that responds to the KeyUp event.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyUp(KeyEventArgs e)
        {
            if ((e.Key == Key.Space) && (ClickMode != ClickMode.Hover))
            {
                StopTimer();
            }
            base.OnKeyUp(e);
        }



        #endregion

        #region Data

        private DispatcherTimer _timer;

        #endregion

    }
}
