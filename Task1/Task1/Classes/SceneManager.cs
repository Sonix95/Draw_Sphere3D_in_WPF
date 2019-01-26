using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using Task1.Interfaces;

namespace Classes.Task1
{
    class SceneManager : ISceneManager
    {
        public Viewport3D _viewport3D = new Viewport3D();
        private Model3DGroup _mainModel3DGroup = new Model3DGroup();
        private ModelVisual3D _modelVisual3D = new ModelVisual3D();

        private Point3D _sphereCenter = new Point3D(0, 0, 0);
        private Vector3D _lightDirection = new Vector3D(1, -1, -2);

        private int _viewportHeight = 413;
        private int _viewportWidth = 457;
        private int _viewportMarginLeft = 164;
        private int _viewportMarginTop = 73;
        private int _viewportMarginRight = 0;
        private int _viewportMarginBottom = 0;
        private HorizontalAlignment _viewportHorizontalAlignment = HorizontalAlignment.Left;
        private VerticalAlignment _viewportVerticalAlignment = VerticalAlignment.Top;

        private IDrawSphere _drawSphere = new DrawSphere();

        public Viewport3D Viewport3D { get => _viewport3D; set => _viewport3D = value; }

        public void ViewportInit()
        {
            _viewport3D.Height = _viewportHeight;
            _viewport3D.Width = _viewportWidth;
            _viewport3D.HorizontalAlignment = _viewportHorizontalAlignment;
            _viewport3D.VerticalAlignment = _viewportVerticalAlignment;
            _viewport3D.Margin = new Thickness(_viewportMarginLeft, _viewportMarginTop, _viewportMarginRight, _viewportMarginBottom);
        }

        public void CreateCamera(Point3D position)
        {
            PerspectiveCamera camera = new PerspectiveCamera
            {
                Position = position,
                LookDirection = new Vector3D(-position.X, -position.Y, -position.Z)
            };
            _viewport3D.Camera = camera;
        }

        public void CreateLight(Vector3D dir)
        {
            DirectionalLight directionalLight = new DirectionalLight(Colors.White, dir);
            _mainModel3DGroup.Children.Add(directionalLight);
        }

        public void CreateMesh(double radius, double rowCount, double columnCount)
        {
            ClearScene(_modelVisual3D);

            CreateLight(_lightDirection);

            _drawSphere.CreateSphere(_mainModel3DGroup, _sphereCenter, radius, (int)rowCount, (int)columnCount);

            _modelVisual3D.Content = _mainModel3DGroup;
            _viewport3D.Children.Add(_modelVisual3D);
        }

        public void ClearScene(ModelVisual3D model)
        {
            _mainModel3DGroup.Children.Clear();
            _viewport3D.Children.Remove(model);
        }

    }
}
