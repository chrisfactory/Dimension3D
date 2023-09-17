using Dimension3D.Core;
using System;
using System.Reflection.Metadata;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace Dimension3D.Samples
{
    public class PlanetControl : DimensionButton3D
    {
        private static readonly Type _typeofThis = typeof(PlanetControl);
        public static readonly DependencyProperty MaterialProperty;
        public static readonly DependencyProperty SizeXProperty;
        public static readonly DependencyProperty SizeYProperty;
        public static readonly DependencyProperty SizeZProperty;
        public static readonly DependencyProperty PositionXProperty;
        public static readonly DependencyProperty PositionYProperty;
        public static readonly DependencyProperty PositionZProperty;
        public static readonly DependencyProperty TrajectoryCenterXProperty;
        public static readonly DependencyProperty TrajectoryCenterYProperty;
        public static readonly DependencyProperty TrajectoryCenterZProperty;
        public static readonly DependencyProperty TrajectoryRadiusXProperty;
        public static readonly DependencyProperty TrajectoryRadiusYProperty;
        public static readonly DependencyProperty TrajectoryRadiusZProperty;
        public static readonly DependencyProperty RotationDurationProperty;
        public static readonly DependencyProperty ShowTrajectoryProperty;
        public static readonly DependencyProperty ShowInclinationPlanProperty;
        static PlanetControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(_typeofThis, new FrameworkPropertyMetadata(_typeofThis));
            MaterialProperty = DependencyProperty.Register(nameof(Material), typeof(Material), _typeofThis, new FrameworkPropertyMetadata());
            SizeXProperty = DependencyProperty.Register(nameof(SizeX), typeof(double), _typeofThis, new FrameworkPropertyMetadata(100.0));
            SizeYProperty = DependencyProperty.Register(nameof(SizeY), typeof(double), _typeofThis, new FrameworkPropertyMetadata(100.0));
            SizeZProperty = DependencyProperty.Register(nameof(SizeZ), typeof(double), _typeofThis, new FrameworkPropertyMetadata(100.0));
            PositionXProperty = DependencyProperty.Register(nameof(PositionX), typeof(double), _typeofThis, new FrameworkPropertyMetadata());
            PositionYProperty = DependencyProperty.Register(nameof(PositionY), typeof(double), _typeofThis, new FrameworkPropertyMetadata());
            PositionZProperty = DependencyProperty.Register(nameof(PositionZ), typeof(double), _typeofThis, new FrameworkPropertyMetadata());
            TrajectoryCenterXProperty = DependencyProperty.Register(nameof(TrajectoryCenterX), typeof(double), _typeofThis, new FrameworkPropertyMetadata());
            TrajectoryCenterYProperty = DependencyProperty.Register(nameof(TrajectoryCenterY), typeof(double), _typeofThis, new FrameworkPropertyMetadata());
            TrajectoryCenterZProperty = DependencyProperty.Register(nameof(TrajectoryCenterZ), typeof(double), _typeofThis, new FrameworkPropertyMetadata());
            TrajectoryRadiusXProperty = DependencyProperty.Register(nameof(TrajectoryRadiusX), typeof(double), _typeofThis, new FrameworkPropertyMetadata());
            TrajectoryRadiusYProperty = DependencyProperty.Register(nameof(TrajectoryRadiusY), typeof(double), _typeofThis, new FrameworkPropertyMetadata());
            TrajectoryRadiusZProperty = DependencyProperty.Register(nameof(TrajectoryRadiusZ), typeof(double), _typeofThis, new FrameworkPropertyMetadata());
            RotationDurationProperty = DependencyProperty.Register(nameof(RotationDuration), typeof(Duration), _typeofThis, new FrameworkPropertyMetadata<PlanetControl>(RotationDurationPropertyChangedCallback));
            ShowTrajectoryProperty = DependencyProperty.Register(nameof(ShowTrajectory), typeof(bool), _typeofThis, new FrameworkPropertyMetadata());
            ShowInclinationPlanProperty = DependencyProperty.Register(nameof(ShowInclinationPlan), typeof(bool), _typeofThis, new FrameworkPropertyMetadata());
        }



        private Storyboard StoryboardRotationAnimation = new Storyboard();
        public PlanetControl()
        {
            NameScope.SetNameScope(this, new NameScope());

        }
        public Material Material { get => (Material)GetValue(MaterialProperty); set => SetValue(MaterialProperty, value); }


        public double SizeX { get => (double)GetValue(SizeXProperty); set => SetValue(SizeXProperty, value); }
        public double SizeY { get => (double)GetValue(SizeYProperty); set => SetValue(SizeYProperty, value); }
        public double SizeZ { get => (double)GetValue(SizeZProperty); set => SetValue(SizeZProperty, value); }

        public double PositionX { get => (double)GetValue(PositionXProperty); set => SetValue(PositionXProperty, value); }
        public double PositionY { get => (double)GetValue(PositionYProperty); set => SetValue(PositionYProperty, value); }
        public double PositionZ { get => (double)GetValue(PositionZProperty); set => SetValue(PositionZProperty, value); }



        public double TrajectoryCenterX { get => (double)GetValue(TrajectoryCenterXProperty); set => SetValue(TrajectoryCenterXProperty, value); }
        public double TrajectoryCenterY { get => (double)GetValue(TrajectoryCenterYProperty); set => SetValue(TrajectoryCenterYProperty, value); }
        public double TrajectoryCenterZ { get => (double)GetValue(TrajectoryCenterZProperty); set => SetValue(TrajectoryCenterZProperty, value); }


        public double TrajectoryRadiusX { get => (double)GetValue(TrajectoryRadiusXProperty); set => SetValue(TrajectoryRadiusXProperty, value); }
        public double TrajectoryRadiusY { get => (double)GetValue(TrajectoryRadiusYProperty); set => SetValue(TrajectoryRadiusYProperty, value); }
        public double TrajectoryRadiusZ { get => (double)GetValue(TrajectoryRadiusZProperty); set => SetValue(TrajectoryRadiusZProperty, value); }


        public Duration? RotationDuration { get => (Duration?)GetValue(RotationDurationProperty); set => SetValue(RotationDurationProperty, value); }









        public bool ShowTrajectory { get => (bool)GetValue(ShowTrajectoryProperty); set => SetValue(ShowTrajectoryProperty, value); }
        public bool ShowInclinationPlan { get => (bool)GetValue(ShowInclinationPlanProperty); set => SetValue(ShowInclinationPlanProperty, value); }


        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            var rotation = this.Template.FindName("rotation", this) as AxisAngleRotation3D;

            if (rotation != null)
            {
                RegisterName("rotation", rotation);

                DoubleAnimation animation = new DoubleAnimation();
                animation.From = 0;
                animation.To = 360;
                animation.RepeatBehavior = RepeatBehavior.Forever;
                animation.AutoReverse = false;
                if (RotationDuration != null)
                    animation.Duration = RotationDuration.Value;

                Storyboard.SetTargetName(animation, "rotation");
                Storyboard.SetTargetProperty(animation, new PropertyPath(AxisAngleRotation3D.AngleProperty));

                StoryboardRotationAnimation.Children.Add(animation);
                if (RotationDuration.HasValue)
                    StoryboardRotationAnimation.Begin(this, true);

            }


        }

        private static void RotationDurationPropertyChangedCallback(PlanetControl d, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue != null)
            {
                d.StoryboardRotationAnimation.Stop();
                d.StoryboardRotationAnimation.Children.Clear();
            }
            if (e.NewValue is Duration newDuration)
            {

                DoubleAnimation animation = new DoubleAnimation();
                animation.From = 0;
                animation.To = 360;
                animation.RepeatBehavior = RepeatBehavior.Forever;
                animation.AutoReverse = false;
                animation.Duration = newDuration;
                Storyboard.SetTargetName(animation, "rotation");
                Storyboard.SetTargetProperty(animation, new PropertyPath(AxisAngleRotation3D.AngleProperty));

                d.StoryboardRotationAnimation.Begin();
            }
        }

    }
}
