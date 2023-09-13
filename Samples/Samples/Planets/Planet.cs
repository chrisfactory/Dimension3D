using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


        private bool CanClickDemo() => true;
        protected virtual void ClickAction() { }
    }
}
