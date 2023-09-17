using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dimension3D.Samples
{
    public class Moon : Planet
    {
        public Moon(Hearth relatedHearth)
        {
            SizeX = relatedHearth.SizeY / 3.66 ;
            SizeY = SizeX;
            SizeZ = SizeX;

            PositionX = relatedHearth.PositionX + 450;
            TrajectoryCenterX = relatedHearth.PositionX;
            TrajectoryRadiusX = 450;

            RotationDuration = new Duration(TimeSpan.FromSeconds(32.4));//27j 

        }
    }
}
