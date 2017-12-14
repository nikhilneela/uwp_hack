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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SimpleCustomControl
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private ColorPickerControl _colorPickerControl;

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void HideColorPickerClicked(object sender, RoutedEventArgs e)
        {
            ShowColorPickerButton.Visibility = Visibility.Visible;
            HideColorPickerButton.Visibility = Visibility.Collapsed;
            ParentView.Children.Remove(_colorPickerControl);

        }

        private async void ShowColorPickerClicked(object sender, RoutedEventArgs e)
        {
            ShowColorPickerButton.Visibility = Visibility.Collapsed;
            HideColorPickerButton.Visibility = Visibility.Visible;

            RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap();

            await renderTargetBitmap.RenderAsync(ParentView);

            _colorPickerControl = new ColorPickerControl(renderTargetBitmap);
            Point location = new Point(ParentView.ActualWidth / 2, ParentView.ActualHeight / 2);
            _colorPickerControl.setControlPosition(location);
            ParentView.Children.Add(_colorPickerControl);
        }
    }
}
