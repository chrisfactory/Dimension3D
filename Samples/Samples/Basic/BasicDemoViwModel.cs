using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimension3D.Samples
{

    public class BasicDemoViwModel : SampleBase
    {
        private double _OffsetX = 0;
        private int s = 1;
        public BasicDemoViwModel() : base("Basic Demo")
        {
            this.Items = new System.Collections.ObjectModel.ObservableCollection<ViewModelBase>()
            {
                new Sun(), 
                // new Moon(2200),
                new Hearth(),
           
            };
        }


        protected override void ResetDemoAction()
        {

        }
    }
}
