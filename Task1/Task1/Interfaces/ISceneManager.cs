using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace Task1.Interfaces
{
    interface ISceneManager
    {
        Viewport3D Viewport3D { get; set; }

        void ViewportInit();
        void CreateCamera(Point3D position);
        void CreateLight(Vector3D dir);
        void CreateMesh(double radius, double rowCount, double columnCount);
        void ClearScene(ModelVisual3D model);
    }
}
