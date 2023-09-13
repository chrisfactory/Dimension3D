using System;
using System.Windows.Media.Media3D;

namespace Dimension3D.Core
{
    public class DimensionGeometrySphere : DimensionGeometryProvider
    {


        public int ThetaDiv { get; set; } = 25;
        public int PhiDiv { get; set; } = 25;

      
        protected override MeshGeometry3D ProvideMesh()
        {
            var thetaDiv = ThetaDiv;
            var phiDiv = PhiDiv;
            var mesh = new MeshGeometry3D();
            MeshBuilder.CreateTessellateSphere(mesh, new Point3D(), thetaDiv, phiDiv, 0.5);

            return mesh;
        }
    }
}
