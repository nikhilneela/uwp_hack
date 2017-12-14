using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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

            SControl.Content = image;

        }


        private async void getPixels()
        {
            await _bitmap.GetPixelsAsync();
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
            
        }

        void Pointer_Moved(object sender, PointerRoutedEventArgs e)
        {
            // Retrieve the point associated with the current event
            Windows.UI.Input.PointerPoint currentPoint = e.GetCurrentPoint(MainCanvas);
            // Create a popup if the pointer being moved is in contact with the screen
            if (currentPoint.IsInContact)
            {
                setControlPosition(currentPoint.Position);
            }
        }

        public void setControlPosition(Point location)
        {
            _controlTransform.X = location.X;
            _controlTransform.Y = location.Y;
        }

       
    }
}
