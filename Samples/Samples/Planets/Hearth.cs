using System;
using System.Windows;

namespace Dimension3D.Samples
{
    public class Hearth : Planet
    {
        public Hearth()
        {
            SizeX = 200;
            SizeY = 200;
            SizeZ = 200;

            PositionX = 2200; 
            TrajectoryRadiusX = PositionX;

            RotationDuration = new Duration(TimeSpan.FromSeconds(1.2)); 

            Items.Add(new Moon(this));
        }
    }
}
