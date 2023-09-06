using System.Collections.ObjectModel;
using System.Linq;

namespace Dimension3D.Samples
{
    public class GlobalViewModel : ViewModelBase
    {
        private ObservableCollection<SampleBase> _Samples;
        private SampleBase? _CurrentSample;

        public GlobalViewModel()
        {
            _Samples = new()
            {
                new BasicDemoViwModel(),
            };

            _CurrentSample = Samples.FirstOrDefault();
        }



        public ObservableCollection<SampleBase> Samples { get => _Samples; set => NotifySet(ref _Samples, value); }
        public SampleBase? CurrentSample { get => _CurrentSample; set => NotifySet(ref _CurrentSample, value); }

        protected override void OnPropertyChanged(string propertyName)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == nameof(CurrentSample))
                CurrentSample?.ResetDemo.Execute();
        }
    }
}
