using Classes.Task1;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using Task1.Interfaces;

namespace Task1.Classes
{
    class ViewportInitializer : IViewportInitializer
    {
        private int _viewportHeight = 413;
        private int _viewportWidth = 457;
        private int _viewportMarginLeft = 164;
        private int _viewportMarginTop = 73;
        private int _viewportMarginRight = 0;
        private int _viewportMarginBottom = 0;
        private HorizontalAlignment _viewportHorizontalAlignment = HorizontalAlignment.Left;
        private VerticalAlignment _viewportVerticalAlignment = VerticalAlignment.Top;

        private Viewport3D _viewport3D = new Viewport3D();

        public void ViewportInit()
        {
            _viewport3D.Height = _viewportHeight;
            _viewport3D.Width = _viewportWidth;
            _viewport3D.HorizontalAlignment = _viewportHorizontalAlignment;
            _viewport3D.VerticalAlignment = _viewportVerticalAlignment;
            _viewport3D.Margin = new Thickness(_viewportMarginLeft, _viewportMarginTop, _viewportMarginRight, _viewportMarginBottom);
        }
    }
}
