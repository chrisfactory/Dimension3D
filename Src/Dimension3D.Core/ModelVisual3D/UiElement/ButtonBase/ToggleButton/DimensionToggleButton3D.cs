using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dimension3D.Core
{
    [DefaultEvent("Checked")]
    public abstract class DimensionToggleButton3D : DimensionButtonBase3D
    {
        private static readonly Type _typeofThis = typeof(DimensionToggleButton3D);
        #region Constructors

     

        /// <summary>
        ///     Default ToggleButton constructor
        /// </summary>
        /// <remarks>
        ///     Automatic determination of current Dispatcher. Use alternative constructor
        ///     that accepts a Dispatcher for best performance.
        /// </remarks>
        public DimensionToggleButton3D() : base()
        {
        }
        #endregion

        #region Properties and Events

        /// <summary>
        ///     Checked event
        /// </summary>
        public static readonly RoutedEvent CheckedEvent = EventManager.RegisterRoutedEvent("Checked", RoutingStrategy.Bubble, typeof(RoutedEventHandler), _typeofThis);

        /// <summary>
        ///     Unchecked event
        /// </summary>
        public static readonly RoutedEvent UncheckedEvent = EventManager.RegisterRoutedEvent("Unchecked", RoutingStrategy.Bubble, typeof(RoutedEventHandler), _typeofThis);

        /// <summary>
        ///     Indeterminate event
        /// </summary>
        public static readonly RoutedEvent IndeterminateEvent = EventManager.RegisterRoutedEvent("Indeterminate", RoutingStrategy.Bubble, typeof(RoutedEventHandler), _typeofThis);

        /// <summary>
        ///     Add / Remove Checked handler
        /// </summary>
        [Category("Behavior")]
        public event RoutedEventHandler Checked
        {
            add
            {
                AddHandler(CheckedEvent, value);
            }

            remove
            {
                RemoveHandler(CheckedEvent, value);
            }
        }

        /// <summary>
        ///     Add / Remove Unchecked handler
        /// </summary>
        [Category("Behavior")]
        public event RoutedEventHandler Unchecked
        {
            add
            {
                AddHandler(UncheckedEvent, value);
            }

            remove
            {
                RemoveHandler(UncheckedEvent, value);
            }
        }

        /// <summary>
        ///     Add / Remove Indeterminate handler
        /// </summary>
        [Category("Behavior")]
        public event RoutedEventHandler Indeterminate
        {
            add
            {
                AddHandler(IndeterminateEvent, value);
            }

            remove
            {
                RemoveHandler(IndeterminateEvent, value);
            }
        }

        /// <summary>
        ///     The DependencyProperty for the IsChecked property.
        ///     Flags:              BindsTwoWayByDefault
        ///     Default Value:      false
        /// </summary>
        public static readonly DependencyProperty IsCheckedProperty =
                DependencyProperty.Register(
                        "IsChecked",
                        typeof(bool?),
                        _typeofThis,
                        new FrameworkPropertyMetadata(
                                false,
                                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault | FrameworkPropertyMetadataOptions.Journal,
                                new PropertyChangedCallback(OnIsCheckedChanged)));

        /// <summary>
        ///     Indicates whether the ToggleButton is checked
        /// </summary>
        [Category("Appearance")]
        [TypeConverter(typeof(NullableBoolConverter))]
        [Localizability(LocalizationCategory.None, Readability = Readability.Unreadable)]
        public bool? IsChecked
        {
            get
            {
                // Because Nullable<bool> unboxing is very slow (uses reflection) first we cast to bool
                object value = GetValue(IsCheckedProperty);
                if (value == null)
                    return new Nullable<bool>();
                else
                    return new Nullable<bool>((bool)value);
            }
            set
            {
                SetValue(IsCheckedProperty, value.HasValue ? value.Value : null);
            }
        }

        private static object OnGetIsChecked(DependencyObject d) { return ((DimensionToggleButton3D)d).IsChecked; }

        /// <summary>
        ///     Called when IsChecked is changed on "d."
        /// </summary>
        /// <param name="d">The object on which the property was changed.</param>
        /// <param name="e">EventArgs that contains the old and new values for this property</param>
        private static void OnIsCheckedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DimensionToggleButton3D button = (DimensionToggleButton3D)d;
            bool? oldValue = (bool?)e.OldValue;
            bool? newValue = (bool?)e.NewValue;

            //doing soft casting here because the peer can be that of RadioButton and it is not derived from
            //ToggleButtonAutomationPeer - specifically to avoid implementing TogglePattern
            //ToggleButtonAutomationPeer peer = UIElementAutomationPeer.FromElement(button) as ToggleButtonAutomationPeer;
            //if (peer != null)
            //{
            //    peer.RaiseToggleStatePropertyChangedEvent(oldValue, newValue);
            //}

            if (newValue == true)
            {
                button.OnChecked(new RoutedEventArgs(CheckedEvent));
            }
            else if (newValue == false)
            {
                button.OnUnchecked(new RoutedEventArgs(UncheckedEvent));
            }
            else
            {
                button.OnIndeterminate(new RoutedEventArgs(IndeterminateEvent));
            }

        }

        /// <summary>
        ///     Called when IsChecked becomes true.
        /// </summary>
        /// <param name="e">Event arguments for the routed event that is raised by the default implementation of this method.</param>
        protected virtual void OnChecked(RoutedEventArgs e)
        {
            RaiseEvent(e);
        }

        /// <summary>
        ///     Called when IsChecked becomes false.
        /// </summary>
        /// <param name="e">Event arguments for the routed event that is raised by the default implementation of this method.</param>
        protected virtual void OnUnchecked(RoutedEventArgs e)
        {
            RaiseEvent(e);
        }

        /// <summary>
        ///     Called when IsChecked becomes null.
        /// </summary>
        /// <param name="e">Event arguments for the routed event that is raised by the default implementation of this method.</param>
        protected virtual void OnIndeterminate(RoutedEventArgs e)
        {
            RaiseEvent(e);
        }

        /// <summary>
        ///     The DependencyProperty for the IsThreeState property.
        ///     Flags:              None
        ///     Default Value:      false
        /// </summary>
        public static readonly DependencyProperty IsThreeStateProperty =
                DependencyProperty.Register(
                        "IsThreeState",
                        typeof(bool),
                        _typeofThis,
                        new FrameworkPropertyMetadata(false));

        /// <summary>
        ///     The IsThreeState property determines whether the control supports two or three states.
        ///     IsChecked property can be set to null as a third state when IsThreeState is true
        /// </summary>
        [Bindable(true), Category("Behavior")]
        public bool IsThreeState
        {
            get { return (bool)GetValue(IsThreeStateProperty); }
            set { SetValue(IsThreeStateProperty, value); }
        }

        #endregion

        #region Override methods

        /// <summary>
        /// Creates AutomationPeer (<see cref="UIElement.OnCreateAutomationPeer"/>)
        /// </summary>
        //protected override AutomationPeer OnCreateAutomationPeer()
        //{
        //    return new ToggleButtonAutomationPeer(this);
        //}

        /// <summary>
        /// This override method is called when the control is clicked by mouse or keyboard
        /// </summary>
        protected override void OnClick()
        {
            OnToggle();
            base.OnClick();
        }

        #endregion

        #region Method Overrides





        #endregion

        #region Protected virtual methods

        /// <summary>
        /// This vitrual method is called from OnClick(). ToggleButton toggles IsChecked property.
        /// Subclasses can override this method to implement their own toggle behavior
        /// </summary>
        protected internal virtual void OnToggle()
        {
            // If IsChecked == true && IsThreeState == true   --->  IsChecked = null
            // If IsChecked == true && IsThreeState == false  --->  IsChecked = false
            // If IsChecked == false                          --->  IsChecked = true
            // If IsChecked == null                           --->  IsChecked = false
            bool? isChecked;
            if (IsChecked == true)
                isChecked = IsThreeState ? (bool?)null : (bool?)false;
            else // false or null
                isChecked = IsChecked.HasValue; // HasValue returns true if IsChecked==false
            IsChecked = isChecked;
            //SetIsPressed( isChecked??false);
        }

        #endregion

        #region Data

        #endregion

        #region Accessibility

        #endregion
    }
}
