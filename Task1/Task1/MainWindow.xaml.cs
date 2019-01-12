using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace Task1
{
    public partial class MainWindow
    {
        #region Variables

        private Viewport3D _viewport3D = new Viewport3D();
        private Model3DGroup _mainModel3DGroup = new Model3DGroup();
        private ModelVisual3D _modelVisual3D = new ModelVisual3D();

        private Point3D _sphereCenter = new Point3D(0, 0, 0);
        private Vector3D _lightDirection = new Vector3D(1, -1, -2);
        private Point3D _cameraPosition = new Point3D(0, 2, 15);

        private double _rowCount = 6;
        private double _columnCount = 6;
        private double _sphereRadius = 2;

        private int _viewportHeight = 413;
        private int _viewportWidth = 457;
        private int _viewportMarginLeft = 164;
        private int _viewportMarginTop = 73;
        private int _viewportMarginRight = 0;
        private int _viewportMarginBottom = 0;
        private HorizontalAlignment _viewportHorizontalAlignment = HorizontalAlignment.Left;
        private VerticalAlignment _viewportVerticalAlignment = VerticalAlignment.Top;

        #endregion

        #region Constructor
        public MainWindow()
        {
            InitializeComponent();
        }
        #endregion

        #region Methods
        private void ViewportInit()
        {
            _viewport3D.Height = _viewportHeight;
            _viewport3D.Width = _viewportWidth;
            _viewport3D.HorizontalAlignment = _viewportHorizontalAlignment;
            _viewport3D.VerticalAlignment = _viewportVerticalAlignment;
            _viewport3D.Margin = new Thickness(_viewportMarginLeft, _viewportMarginTop, _viewportMarginRight, _viewportMarginBottom);
        }

        private void CreateCamera(Point3D position)
        {
            PerspectiveCamera camera = new PerspectiveCamera
            {
                Position = position,
                LookDirection = new Vector3D(-position.X, -position.Y, -position.Z)
            };
            _viewport3D.Camera = camera;
        }

        private void CreateLight(Vector3D dir)
        {
            DirectionalLight directionalLight = new DirectionalLight(Colors.White, dir);
            _mainModel3DGroup.Children.Add(directionalLight);
        }

        private void AddTriangle(MeshGeometry3D mesh, Point3D point1, Point3D point2, Point3D point3)
        {
            int i = mesh.Positions.Count;

            mesh.Positions.Add(point1);
            mesh.Positions.Add(point2);
            mesh.Positions.Add(point3);

            mesh.TriangleIndices.Add(i++);
            mesh.TriangleIndices.Add(i++);
            mesh.TriangleIndices.Add(i);
        }

        private void CreateSphere(Model3DGroup modelGroup, Point3D center, double radius, int rowCount, int columnCount)
        {
            MeshGeometry3D mesh = new MeshGeometry3D();
            DiffuseMaterial diffuseMaterial = new DiffuseMaterial(new SolidColorBrush(Colors.Orange));

            double phi0, theta0;
            double dphi = Math.PI / rowCount;
            double dtheta = 2 * Math.PI / columnCount;

            phi0 = 0;
            double y0 = radius * Math.Cos(phi0);
            double r0 = radius * Math.Sin(phi0);

            for (int i = 0; i < rowCount; i++)
            {
                double phi1 = phi0 + dphi;
                double y1 = radius * Math.Cos(phi1);
                double r1 = radius * Math.Sin(phi1);
            
                theta0 = 0;
                Point3D pt00 = new Point3D(center.X + r0 * Math.Cos(theta0), center.Y + y0, center.Z + r0 * Math.Sin(theta0));
                Point3D pt10 = new Point3D(center.X + r1 * Math.Cos(theta0), center.Y + y1, center.Z + r1 * Math.Sin(theta0));

                for (int j = 0; j < columnCount; j++)
                {
                    double theta1 = theta0 + dtheta;
                    Point3D pt01 = new Point3D(center.X + r0 * Math.Cos(theta1), center.Y + y0, center.Z + r0 * Math.Sin(theta1));
                    Point3D pt11 = new Point3D(center.X + r1 * Math.Cos(theta1), center.Y + y1, center.Z + r1 * Math.Sin(theta1));

                    AddTriangle(mesh, pt00, pt11, pt10);
                    AddTriangle(mesh, pt00, pt01, pt11);

                    theta0 = theta1;
                    pt00 = pt01;
                    pt10 = pt11;
                }

                phi0 = phi1;
                y0 = y1;
                r0 = r1;
            }

            GeometryModel3D geometryModel3D = new GeometryModel3D(mesh, diffuseMaterial);
            modelGroup.Children.Add(geometryModel3D);
        }

        private void CreateMesh(double radius, double rowCount, double columnCount)
        {
            ClearScene(_modelVisual3D);

            CreateLight(_lightDirection);

            CreateSphere(_mainModel3DGroup, _sphereCenter, radius, (int)rowCount, (int)columnCount);

            _modelVisual3D.Content = _mainModel3DGroup;
            _viewport3D.Children.Add(_modelVisual3D);
        }

        private void ClearScene(ModelVisual3D model)
        {
            _mainModel3DGroup.Children.Clear();
            _viewport3D.Children.Remove(model);
        }

        #endregion

        #region EventHandles
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CreateCamera(_cameraPosition);

            ViewportInit();

            MainGrid.Children.Add(_viewport3D);

            CreateMesh(_sphereRadius, _rowCount, _columnCount);
        }

        private void ColumnCountSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _columnCount = columnCountSlider.Value;
            CreateMesh(_sphereRadius, _rowCount, _columnCount);
        }

        private void RowCountSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _rowCount = rowCountSlider.Value;
            CreateMesh(_sphereRadius, _rowCount, _columnCount);
        }

        private void RadiusSphereSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _sphereRadius = radiusSlider.Value;
            CreateMesh(_sphereRadius, _rowCount, _columnCount);
        }

        #endregion
    }
}
