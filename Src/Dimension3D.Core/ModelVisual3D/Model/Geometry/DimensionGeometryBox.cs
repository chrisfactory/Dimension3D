using System.Windows;
using System.Windows.Media.Media3D;

namespace Dimension3D.Core
{
    public class DimensionGeometryBox : DimensionGeometryModel3D
    {
        protected override MeshGeometry3D ProvideMesh()
        {
            var center = new Point3D();
            var size = new Size(1, 1);
            var mesh = new MeshGeometry3D();
            MeshBuilder.CreatePlan(mesh, center, size, PlanSides.Front);
            MeshBuilder.CreatePlan(mesh, center, size, PlanSides.Back);
            MeshBuilder.CreatePlan(mesh, center, size, PlanSides.Left);
            MeshBuilder.CreatePlan(mesh, center, size, PlanSides.Right);
            MeshBuilder.CreatePlan(mesh, center, size, PlanSides.Top);
            MeshBuilder.CreatePlan(mesh, center, size, PlanSides.Bottom);

            return mesh;

        }
    }
}
