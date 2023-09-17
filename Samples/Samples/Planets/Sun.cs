using Dimension3D.Samples;
using System.Windows;
using System;

namespace Dimension3D.Samples
{
    public class Sun : Planet
    {
        public Sun()
        {
            SizeX = 800;
            SizeY = 800;
            SizeZ = 800;

            RotationDuration = new Duration(TimeSpan.FromSeconds(30));
        }
    }
}
