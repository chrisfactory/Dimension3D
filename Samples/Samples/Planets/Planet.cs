using Prism.Commands;
using System.Collections.ObjectModel;
using System.Windows;

namespace Dimension3D.Samples
{
    public abstract class Planet : ViewModelBase
    {
        private ObservableCollection<ViewModelBase> _Items = new ObservableCollection<ViewModelBase>();

        public Planet()
        {
            CommandClick = new DelegateCommand(ClickAction, CanClickDemo);
            base.RegisterCommands(CommandClick);
        }
        public ObservableCollection<ViewModelBase> Items { get => _Items; protected set => NotifySet(ref _Items, value); }


        public DelegateCommand CommandClick { get; private set; }




        private double _SizeX = 10;
        private double _SizeY = 10;
        private double _SizeZ = 10;
        public double SizeX { get => _SizeX; set => NotifySet(ref _SizeX, value); }
        public double SizeY { get => _SizeY; set => NotifySet(ref _SizeY, value); }
        public double SizeZ { get => _SizeZ; set => NotifySet(ref _SizeZ, value); }

        private double _PositionX = 0;
        private double _PositionY = 0;
        private double _PositionZ = 0;
        public double PositionX { get => _PositionX; set => NotifySet(ref _PositionX, value); }
        public double PositionY { get => _PositionY; set => NotifySet(ref _PositionY, value); }
        public double PositionZ { get => _PositionZ; set => NotifySet(ref _PositionZ, value); }

        private double _TrajectoryCenterX = 0;
        private double _TrajectoryCenterY = 0;
        private double _TrajectoryCenterZ = 0;
        public double TrajectoryCenterX { get => _TrajectoryCenterX; set => NotifySet(ref _TrajectoryCenterX, value); }
        public double TrajectoryCenterY { get => _TrajectoryCenterY; set => NotifySet(ref _TrajectoryCenterY, value); }
        public double TrajectoryCenterZ { get => _TrajectoryCenterZ; set => NotifySet(ref _TrajectoryCenterZ, value); }


        private double _TrajectoryRadiusX = 1;
        private double _TrajectoryRadiusY = 1;
        private double _TrajectoryRadiusZ = 1;
        public double TrajectoryRadiusX { get => _TrajectoryRadiusX; set => NotifySet(ref _TrajectoryRadiusX, value); }
        public double TrajectoryRadiusY { get => _TrajectoryRadiusY; set => NotifySet(ref _TrajectoryRadiusY, value); }
        public double TrajectoryRadiusZ { get => _TrajectoryRadiusZ; set => NotifySet(ref _TrajectoryRadiusZ, value); }

        private Duration? _RotationDuration = null;
        public Duration? RotationDuration { get => _RotationDuration; set => NotifySet(ref _RotationDuration, value); } 



        private bool CanClickDemo() => true;
        protected virtual void ClickAction() { }
    }
}
