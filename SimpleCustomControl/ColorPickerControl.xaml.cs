using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace SimpleCustomControl
{
    public sealed partial class ColorPickerControl : UserControl
    {
        private TranslateTransform _controlTransform;
        private RenderTargetBitmap _bitmap;
        private Color _color;

       


        public ColorPickerControl(RenderTargetBitmap renderTargetBitmap)
        {
            this.InitializeComponent();
            _bitmap = renderTargetBitmap;

            MainCanvas.PointerPressed += new PointerEventHandler(Pointer_Pressed);
            MainCanvas.PointerMoved += new PointerEventHandler(Pointer_Moved);
            MainCanvas.PointerEntered += new PointerEventHandler(Pointer_Entered);

            _controlTransform = new TranslateTransform();
            SControl.RenderTransform = _controlTransform;

            var image = new Image
            {
                //Stretch = Stretch.UniformToFill
            };

            image.Source = _bitmap;
        }


        private async void getPixels(Point location)
        {
            var pixelBuffer = await _bitmap.GetPixelsAsync();
            byte[] pixels = pixelBuffer.ToArray();
            
            Color color = GetPixelColor(pixels, (int)(location.X * DisplayProperties.LogicalDpi / 96), (int)(location.Y * DisplayProperties.LogicalDpi / 96), 
                (uint) _bitmap.PixelWidth, (uint)_bitmap.PixelHeight);
            SControl.setCurrentColor(color);
            _color = color;
            //SControl.Background = new SolidColorBrush(color);
        }

        public Color GetPixelColor(byte[] m_pixelData, int xPosPhys, int yPosPhys, uint m_pixelWidth, uint m_pixelHeight)
        {

            if (xPosPhys >= 0 && yPosPhys >= 0 && xPosPhys < m_pixelWidth && yPosPhys < m_pixelHeight && null != m_pixelData && m_pixelWidth > 0 && m_pixelHeight > 0)
            {
                int arrayIndexStart = (int)((xPosPhys * 4) + (yPosPhys * m_pixelWidth * 4));
                if (arrayIndexStart >= 0 && arrayIndexStart < m_pixelData.Length - 3)
                {
                    byte pixel_b = m_pixelData[arrayIndexStart];
                    byte pixel_g = m_pixelData[arrayIndexStart + 1];
                    byte pixel_r = m_pixelData[arrayIndexStart + 2];
                    byte pixel_a = m_pixelData[arrayIndexStart + 3];
                    return Color.FromArgb(pixel_a, pixel_r, pixel_g, pixel_b);
                }
            }

            return Color.FromArgb(0, 0, 0, 0);
        }

        void Pointer_Pressed(object sender, PointerRoutedEventArgs e)
        {
            Windows.UI.Input.PointerPoint currentPoint = e.GetCurrentPoint(MainCanvas);
            handlePointerEntered(currentPoint.Position);
        }


        void Pointer_Entered(object sender, PointerRoutedEventArgs e)
        {
            Windows.UI.Input.PointerPoint currentPoint = e.GetCurrentPoint(MainCanvas);
            if (currentPoint.IsInContact)
            {
                handlePointerEntered(currentPoint.Position);
            }
        }

        private void handlePointerEntered(Point location)
        {
            setControlPosition(location);
          //  SControl.Background = new SolidColorBrush(Colors.Red);
            getPixels(location);


        }

        void Pointer_Moved(object sender, PointerRoutedEventArgs e)
        {
            // Retrieve the point associated with the current event
            Windows.UI.Input.PointerPoint currentPoint = e.GetCurrentPoint(MainCanvas);
            // Create a popup if the pointer being moved is in contact with the screen
            if (currentPoint.IsInContact)
            {
                setControlPosition(currentPoint.Position);
                getPixels(currentPoint.Position);
            }
        }

        public void setControlPosition(Point location)
        {
            _controlTransform.X = location.X;
            _controlTransform.Y = location.Y;
        }

       
    }
}
