using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace Task1.Interfaces
{
    interface IDrawSphere
    {
        void CreateSphere(Model3DGroup modelGroup, Point3D center, double radius, int rowCount, int columnCount);
        void AddTriangle(MeshGeometry3D mesh, Point3D point1, Point3D point2, Point3D point3);
    }
}
