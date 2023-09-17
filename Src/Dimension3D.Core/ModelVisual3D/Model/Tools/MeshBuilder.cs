using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Media3D;

namespace Dimension3D.Core
{
    [Flags]
    public enum PlanSides : short
    {
        Front = 1,
        Right = 2,
        Back = 4,
        Left = 8,
        Bottom = 16,
        Top = 32
    }
    public static class MeshBuilder
    {

        public static void CreatePlan(MeshGeometry3D mesh, Point3D location, Size3D size, PlanSides sides)
        {
            var finalLocation = ToCenterLocation(location, size);

            double width = size.X;
            double height = size.Y;
            double depth = size.Z;

            if ((sides & PlanSides.Front) == PlanSides.Front)
            {
                AddPlaneToMesh(((Vector3D)finalLocation),
                    mesh,
                    new Vector3D(0, 0, 1),
                    new Point3D(0, height, depth),
                    new Point3D(0, 0, depth),
                    new Point3D(width, 0, depth),
                    new Point3D(width, height, depth));
            }
            else if ((sides & PlanSides.Right) == PlanSides.Right)
            {
                AddPlaneToMesh(((Vector3D)finalLocation),
                    mesh,
                    new Vector3D(1, 0, 0),
                    new Point3D(width, height, depth),
                    new Point3D(width, 0, depth),
                    new Point3D(width, 0, 0),
                    new Point3D(width, height, 0));
            }
            else if ((sides & PlanSides.Back) == PlanSides.Back)
            {
                AddPlaneToMesh(((Vector3D)finalLocation),
                    mesh,
                    new Vector3D(0, 0, -1),
                    new Point3D(width, height, 0),
                    new Point3D(width, 0, 0),
                    new Point3D(0, 0, 0),
                    new Point3D(0, height, 0));
            }
            else if ((sides & PlanSides.Left) == PlanSides.Left)
            {
                AddPlaneToMesh(((Vector3D)finalLocation),
                    mesh,
                    new Vector3D(-1, 0, 0),
                    new Point3D(0, height, 0),
                    new Point3D(0, 0, 0),
                    new Point3D(0, 0, depth),
                    new Point3D(0, height, depth));
            }
            else if ((sides & PlanSides.Bottom) == PlanSides.Bottom)
            {
                AddPlaneToMesh(((Vector3D)finalLocation),
                    mesh,
                    new Vector3D(0, -1, 0),
                    new Point3D(0, 0, depth),
                    new Point3D(0, 0, 0),
                    new Point3D(width, 0, 0),
                    new Point3D(width, 0, depth));
            }
            else if ((sides & PlanSides.Top) == PlanSides.Top)
            {
                AddPlaneToMesh(((Vector3D)finalLocation),
                    mesh,
                    new Vector3D(0, 1, 0),
                    new Point3D(0, height, 0),
                    new Point3D(0, height, depth),
                    new Point3D(width, height, depth),
                    new Point3D(width, height, 0));
            }
        }
        private static Point3D ToCenterLocation(Point3D location, Size3D size)
        {
            var x = location.X - size.X * 0.5;
            var y = location.Y - size.Y * 0.5;
            var z = location.Z - size.Z * 0.5;
            return new Point3D(x, y, z);
        }
        private static MeshGeometry3D AddPlaneToMesh(
           Vector3D position,
           MeshGeometry3D mesh,
           Vector3D normal,
           Point3D upperLeft,
           Point3D lowerLeft,
           Point3D lowerRight,
           Point3D upperRight)
        {
            int offset = mesh.Positions.Count;

            mesh.Positions.Add(position + upperLeft);
            mesh.Positions.Add(position + lowerLeft);
            mesh.Positions.Add(position + lowerRight);
            mesh.Positions.Add(position + upperRight);

            mesh.Normals.Add(normal);
            mesh.Normals.Add(normal);
            mesh.Normals.Add(normal);
            mesh.Normals.Add(normal);

            mesh.TextureCoordinates.Add(new Point(0, 0));
            mesh.TextureCoordinates.Add(new Point(0, 1));
            mesh.TextureCoordinates.Add(new Point(1, 1));
            mesh.TextureCoordinates.Add(new Point(1, 0));

            mesh.TriangleIndices.Add(offset + 0);
            mesh.TriangleIndices.Add(offset + 1);
            mesh.TriangleIndices.Add(offset + 2);
            mesh.TriangleIndices.Add(offset + 0);
            mesh.TriangleIndices.Add(offset + 2);
            mesh.TriangleIndices.Add(offset + 3);

            return mesh;
        }


        public static void CreateTessellateSphere(MeshGeometry3D mesh, Point3D location, int tDiv, int pDiv, double radius)
        {
            double dt = DegToRad(360.0) / tDiv;
            double dp = DegToRad(180.0) / pDiv;

            for (int pi = 0; pi <= pDiv; pi++)
            {
                double phi = pi * dp;

                for (int ti = 0; ti <= tDiv; ti++)
                {
                    // we want to start the mesh on the x axis
                    double theta = ti * dt;

                    var pos = GetPosition(theta, phi, radius);
                    mesh.Positions.Add((Vector3D)location + pos);
                    mesh.Normals.Add(GetNormal(theta, phi));
                    mesh.TextureCoordinates.Add(GetTextureCoordinate(theta, phi));
                }
            }

            for (int pi = 0; pi < pDiv; pi++)
            {
                for (int ti = 0; ti < tDiv; ti++)
                {
                    int x0 = ti;
                    int x1 = (ti + 1);
                    int y0 = pi * (tDiv + 1);
                    int y1 = (pi + 1) * (tDiv + 1);

                    mesh.TriangleIndices.Add(x0 + y0);
                    mesh.TriangleIndices.Add(x0 + y1);
                    mesh.TriangleIndices.Add(x1 + y0);

                    mesh.TriangleIndices.Add(x1 + y0);
                    mesh.TriangleIndices.Add(x0 + y1);
                    mesh.TriangleIndices.Add(x1 + y1);
                }
            }
        }

        private static double DegToRad(double degrees) => (degrees / 180.0) * Math.PI;
        private static Point3D GetPosition(double theta, double phi, double radius)
        {
            double x = radius * Math.Sin(theta) * Math.Sin(phi);
            double y = radius * Math.Cos(phi);
            double z = radius * Math.Cos(theta) * Math.Sin(phi);

            return new Point3D(x, y, z);
        }

        private static Vector3D GetNormal(double theta, double phi)
        {
            return (Vector3D)GetPosition(theta, phi, 1.0);
        }



        private static System.Windows.Point GetTextureCoordinate(double theta, double phi)
        {
            System.Windows.Point p = new System.Windows.Point(theta / (2 * Math.PI),
                                phi / (Math.PI));

            return p;
        }




        public static void CreateArc(MeshGeometry3D mesh,
             double startAngle,
             double endAngle,
             double radius,
             double width,
             int vertexCount)
        {
            double start = DegToRad(startAngle);
            double end = DegToRad(endAngle);


            double outerRadius = radius + (width / 2);
            double innerRadius = radius - (width / 2);

            var positions = new List<Point3D>();
            var coordinates = new List<Point>();
            var triangles = new List<int>();

            for (int vertexIndex = 0; vertexIndex < vertexCount + 1; vertexIndex++)
            {

                double lengthRatio = (double)vertexIndex / vertexCount;
                if (lengthRatio > 1)
                    lengthRatio = 1;

                double angle = start + ((end - start) * lengthRatio);

                double outerX = Math.Sin(angle) * outerRadius;
                double outerY = Math.Cos(angle) * outerRadius;

                double innerX = Math.Sin(angle) * innerRadius;
                double innerY = Math.Cos(angle) * innerRadius;

                positions.Add(new Point3D(outerX, outerY, 0));
                positions.Add(new Point3D(innerX, innerY, 0));


                coordinates.Add(new Point(lengthRatio, 0));
                coordinates.Add(new Point(lengthRatio, 1));

                triangles.AddRange(new int[] { vertexIndex * 2 + 0, vertexIndex * 2 + 1, vertexIndex * 2 + 2 });
                triangles.AddRange(new int[] { vertexIndex * 2 + 1, vertexIndex * 2 + 3, vertexIndex * 2 + 2 });
            }

            mesh.Positions = new Point3DCollection(positions);
            mesh.TextureCoordinates = new System.Windows.Media.PointCollection(coordinates);
            mesh.TriangleIndices = new System.Windows.Media.Int32Collection(triangles);

        }

    }
}
