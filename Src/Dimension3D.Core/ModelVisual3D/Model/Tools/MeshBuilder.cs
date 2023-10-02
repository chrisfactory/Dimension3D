using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Media.Media3D;

namespace Dimension3D.Core
{
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



        public static void CreateRegularPolygon(MeshGeometry3D mesh, Point3D location, int sides, double radius, PlanSides side)
        {
            InnerCreateArc(mesh, location, 0, 360, sides, radius, 0);
        }




        public static void CreatePlan(MeshGeometry3D mesh, Point3D location, Size size, PlanSides side)
        {


            double deltaHeight = size.Height * 0.5;
            double deltaWidth = size.Width * 0.5;




            switch (side)
            {
                case PlanSides.Front:
                    {
                        AddPlaneToMesh(mesh,
                              new Vector3D(0, 0, 1),
                              new Point3D(location.X - deltaWidth, location.Y + deltaHeight, location.Z),
                              new Point3D(location.X - deltaWidth, location.Y - deltaHeight, location.Z),
                              new Point3D(location.X + deltaWidth, location.Y - deltaHeight, location.Z),
                              new Point3D(location.X + deltaWidth, location.Y + deltaHeight, location.Z));
                    }
                    break;
                case PlanSides.Back:
                    {
                        AddPlaneToMesh(mesh,
                                new Vector3D(0, 0, -1),
                                new Point3D(location.X + deltaWidth, location.Y + deltaHeight, location.Z),
                                new Point3D(location.X + deltaWidth, location.Y - deltaHeight, location.Z),
                                new Point3D(location.X - deltaWidth, location.Y - deltaHeight, location.Z),
                                new Point3D(location.X - deltaWidth, location.Y + deltaHeight, location.Z));
                    }
                    break;
                case PlanSides.Right:
                    {
                        AddPlaneToMesh(mesh,
                                   new Vector3D(1, 0, 0),
                                   new Point3D(location.X, location.Y + deltaHeight, location.Z + deltaWidth),
                                   new Point3D(location.X, location.Y - deltaHeight, location.Z + deltaWidth),
                                   new Point3D(location.X, location.Y - deltaHeight, location.Z - deltaWidth),
                                   new Point3D(location.X, location.Y + deltaHeight, location.Z - deltaWidth));
                    }
                    break;
                case PlanSides.Left:
                    {
                        AddPlaneToMesh(mesh,
                                  new Vector3D(-1, 0, 0),
                                  new Point3D(location.X, location.Y + deltaHeight, location.Z - deltaWidth),
                                  new Point3D(location.X, location.Y - deltaHeight, location.Z - deltaWidth),
                                  new Point3D(location.X, location.Y - deltaHeight, location.Z + deltaWidth),
                                  new Point3D(location.X, location.Y + deltaHeight, location.Z + deltaWidth));
                    }
                    break;
                case PlanSides.Top:
                    {
                        AddPlaneToMesh(mesh,
                                    new Vector3D(0, 1, 0),
                                    new Point3D(location.X - deltaWidth, location.Y, location.Z - deltaHeight),
                                    new Point3D(location.X - deltaWidth, location.Y, location.Z + deltaHeight),
                                    new Point3D(location.X + deltaWidth, location.Y, location.Z + deltaHeight),
                                    new Point3D(location.X + deltaWidth, location.Y, location.Z - deltaHeight));
                    }
                    break;
                case PlanSides.Bottom:
                    {
                        AddPlaneToMesh(mesh,
                                    new Vector3D(0, -1, 0),
                                    new Point3D(location.X - deltaWidth, location.Y, location.Z + deltaHeight),
                                    new Point3D(location.X - deltaWidth, location.Y, location.Z - deltaHeight),
                                    new Point3D(location.X + deltaWidth, location.Y, location.Z - deltaHeight),
                                    new Point3D(location.X + deltaWidth, location.Y, location.Z + deltaHeight));
                    }
                    break;
                default:
                    break;
            }
        }

        private static MeshGeometry3D AddPlaneToMesh(
           MeshGeometry3D mesh,
           Vector3D normal,
           Point3D upperLeft,
           Point3D lowerLeft,
           Point3D lowerRight,
           Point3D upperRight)
        {
            int offset = mesh.Positions.Count;

            mesh.Positions.Add(upperLeft);
            mesh.Positions.Add(lowerLeft);
            mesh.Positions.Add(lowerRight);
            mesh.Positions.Add(upperRight);

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
        public static MeshGeometry3D AddOctagonToMesh(
          MeshGeometry3D mesh,
          Point3D location,
          Size size,
          Size cornerSize,
          PlanSides side)
        {
            int offset = mesh.Positions.Count;



            var p0 = new Point3D(location.X - size.Width * 0.5, location.Y + size.Height * 0.5 - cornerSize.Height, location.Z);
            var p1 = new Point3D(location.X - size.Width * 0.5, location.Y - size.Height * 0.5 + cornerSize.Height, location.Z);
            var p2 = new Point3D(location.X + size.Width * 0.5, location.Y - size.Height * 0.5 + cornerSize.Height, location.Z);
            var p3 = new Point3D(location.X + size.Width * 0.5, location.Y + size.Height * 0.5 - cornerSize.Height, location.Z);

            var p4 = new Point3D(p0.X + cornerSize.Width, p0.Y + cornerSize.Height, p0.Z);
            var p5 = new Point3D(p0.X + cornerSize.Width, p0.Y, p0.Z);
            var p6 = new Point3D(p3.X - cornerSize.Width, p3.Y, p3.Z);
            var p7 = new Point3D(p3.X - cornerSize.Width, p3.Y + cornerSize.Height, p3.Z);

            var p8 = new Point3D(p1.X + cornerSize.Width, p1.Y, p1.Z);
            var p9 = new Point3D(p1.X + cornerSize.Width, p1.Y - cornerSize.Height, p1.Z);
            var p10 = new Point3D(p2.X - cornerSize.Width, p2.Y - cornerSize.Height, p2.Z);
            var p11 = new Point3D(p2.X - cornerSize.Width, p2.Y, p2.Z);
            var points = new List<Point3D>() { p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11 };



            var normal = new Vector3D(0, 0, 1);
            var rotation = new RotateTransform3D();
            rotation.CenterX = location.X;
            rotation.CenterY = location.Y;
            rotation.CenterZ = location.Z;

            switch (side)
            {
                case PlanSides.Front:
                    rotation.Rotation = new AxisAngleRotation3D(new Vector3D(0, 1, 0), 0);
                    break;
                case PlanSides.Back:
                    rotation.Rotation = new AxisAngleRotation3D(new Vector3D(0, 1, 0), 180);
                    break;
                case PlanSides.Right:
                    rotation.Rotation = new AxisAngleRotation3D(new Vector3D(0, 1, 0), 90);
                    break;
                case PlanSides.Left:
                    rotation.Rotation = new AxisAngleRotation3D(new Vector3D(0, 1, 0), -90);
                    break;
                case PlanSides.Top:
                    rotation.Rotation = new AxisAngleRotation3D(new Vector3D(1, 0, 0), -90);
                    break;
                case PlanSides.Bottom:
                    rotation.Rotation = new AxisAngleRotation3D(new Vector3D(1, 0, 0), 90);
                    break;
                default:
                    break;
            }



            normal = rotation.Transform(normal);

            foreach (var p in points)
            {
                mesh.Positions.Add(rotation.Transform(p));
                mesh.Normals.Add(normal);
            }






            double deltaCornerY = cornerSize.Height / size.Height;
            double deltaCornerX = cornerSize.Width / size.Width;

            mesh.TextureCoordinates.Add(new Point(0, 0 + deltaCornerY));
            mesh.TextureCoordinates.Add(new Point(0, 1 - deltaCornerY));
            mesh.TextureCoordinates.Add(new Point(1, 1 - deltaCornerY));
            mesh.TextureCoordinates.Add(new Point(1, 0 + deltaCornerY));

            mesh.TextureCoordinates.Add(new Point(0 + deltaCornerX, 0));
            mesh.TextureCoordinates.Add(new Point(0 + deltaCornerX, 0 + deltaCornerY));
            mesh.TextureCoordinates.Add(new Point(1 - deltaCornerX, 0 + deltaCornerY));
            mesh.TextureCoordinates.Add(new Point(1 - deltaCornerX, 0));

            mesh.TextureCoordinates.Add(new Point(0 + deltaCornerX, 1 - deltaCornerY));
            mesh.TextureCoordinates.Add(new Point(0 + deltaCornerX, 1));
            mesh.TextureCoordinates.Add(new Point(1 - deltaCornerX, 1));
            mesh.TextureCoordinates.Add(new Point(1 - deltaCornerX, 1 - deltaCornerY));

            mesh.TriangleIndices.Add(offset + 0);
            mesh.TriangleIndices.Add(offset + 1);
            mesh.TriangleIndices.Add(offset + 2);
            mesh.TriangleIndices.Add(offset + 0);
            mesh.TriangleIndices.Add(offset + 2);
            mesh.TriangleIndices.Add(offset + 3);

            mesh.TriangleIndices.Add(offset + 4);
            mesh.TriangleIndices.Add(offset + 5);
            mesh.TriangleIndices.Add(offset + 6);
            mesh.TriangleIndices.Add(offset + 4);
            mesh.TriangleIndices.Add(offset + 6);
            mesh.TriangleIndices.Add(offset + 7);


            mesh.TriangleIndices.Add(offset + 8);
            mesh.TriangleIndices.Add(offset + 9);
            mesh.TriangleIndices.Add(offset + 10);
            mesh.TriangleIndices.Add(offset + 8);
            mesh.TriangleIndices.Add(offset + 10);
            mesh.TriangleIndices.Add(offset + 11);

            mesh.TriangleIndices.Add(offset + 5);
            mesh.TriangleIndices.Add(offset + 4);
            mesh.TriangleIndices.Add(offset + 0);

            mesh.TriangleIndices.Add(offset + 6);
            mesh.TriangleIndices.Add(offset + 3);
            mesh.TriangleIndices.Add(offset + 7);

            mesh.TriangleIndices.Add(offset + 8);
            mesh.TriangleIndices.Add(offset + 1);
            mesh.TriangleIndices.Add(offset + 9);

            mesh.TriangleIndices.Add(offset + 11);
            mesh.TriangleIndices.Add(offset + 10);
            mesh.TriangleIndices.Add(offset + 2);

            return mesh;
        }


        public static void CreateTessellateSphere(MeshGeometry3D mesh, Point3D location, int tDiv, int pDiv, double radius)
        {
            double dt = DegToRad(360.0 / tDiv);
            double dp = DegToRad(180.0 / pDiv);

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

        internal static double DegToRad(double degrees) => ((degrees * Math.PI) / 180.0);
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
            return new Point(theta / (2 * Math.PI), phi / (Math.PI));
        }




        public static void CreateArc(MeshGeometry3D mesh, Point3D location,
             double startAngle,
             double endAngle,
             double radius,
             double width,
             int vertexCount)
        {


            double outerRadius = radius + (width / 2);
            double innerRadius = radius - (width / 2);

            InnerCreateArc(mesh, location, startAngle, endAngle, vertexCount, outerRadius, innerRadius);

        }

        private static void InnerCreateArc(MeshGeometry3D mesh, Point3D location, double startAngle, double endAngle, int vertexCount, double outerRadius, double innerRadius)
        {
            double start = DegToRad(startAngle);
            double end = DegToRad(endAngle);

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

                positions.Add(new Point3D(outerX + location.X, outerY + location.Y, location.Z));
                positions.Add(new Point3D(innerX + location.X, innerY + location.Y, location.Z));


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
