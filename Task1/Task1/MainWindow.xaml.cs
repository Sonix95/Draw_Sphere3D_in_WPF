using Classes.Task1;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using Task1.Interfaces;

namespace Task1
{
    public partial class MainWindow
    {
        #region Variables

        private Point3D _cameraPosition = new Point3D(0, 2, 15);

        private double _rowCount = 6;
        private double _columnCount = 6;
        private double _sphereRadius = 2;

        ISceneManager scene = new SceneManager();

        #endregion

        #region Constructor
        public MainWindow()
        {
            InitializeComponent();
        }
        #endregion

        #region EventHandles
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            scene.CreateCamera(_cameraPosition);

            scene.ViewportInit();

            MainGrid.Children.Add(scene.Viewport3D);

            scene.CreateMesh(_sphereRadius, _rowCount, _columnCount);
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Slider slider = sender as Slider;
            if (slider != null)
            {
                switch (slider.Name)
                {
                    case "columnCountSlider":
                        _columnCount = slider.Value;
                        break;
                    case "rowCountSlider":
                        _rowCount = slider.Value;
                        break;
                    case "radiusSlider":
                        _sphereRadius = slider.Value;
                        break;
                    default:
                        Console.WriteLine($"Undefined object: {slider.Name}");
                        break;
                }
                scene.CreateMesh(_sphereRadius, _rowCount, _columnCount);
            }
        }

        #endregion
    }
}
