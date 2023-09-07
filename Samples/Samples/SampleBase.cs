using Prism.Commands;
using System.Collections.ObjectModel;

namespace Dimension3D.Samples
{
    public abstract class SampleBase : ViewModelBase
    {
        private string _Title;
        private ObservableCollection<ViewModelBase>? _Items;
        private ObservableCollection<ViewModelBase>? _SatelliteItems;
        public SampleBase(string title)
        {
            _Title = title;
            ResetDemo = new DelegateCommand(ResetDemoAction, CanResetDemo);
            CommandClick = new DelegateCommand(ClickAction, CanClickDemo);
            base.RegisterCommands(ResetDemo, CommandClick);
            ResetDemoAction();
        }



        public string Title { get => _Title; set => NotifySet(ref _Title, value); }
        public ObservableCollection<ViewModelBase>? Items { get => _Items; protected set => NotifySet(ref _Items, value); }
        public ObservableCollection<ViewModelBase>? SatelliteItems { get => _SatelliteItems; protected set => NotifySet(ref _SatelliteItems, value); }
        public DelegateCommand ResetDemo { get; private set; }
        public DelegateCommand CommandClick { get; private set; }


        protected virtual bool CanResetDemo() => true;

        protected abstract void ResetDemoAction();

        private bool CanClickDemo() => true;
        protected virtual void ClickAction() { }

    }
}
