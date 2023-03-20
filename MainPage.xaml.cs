using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Enumeration;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using BeatlesApp.Pages;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace BeatlesApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(200, 400));
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            MainFrame.Navigate(typeof(Homepage));
            Navbar.SelectedItem = Navbar.MenuItems[1];
        }

        private void Page1NavItemTapped(object sender, TappedRoutedEventArgs e)
        {
            if (MainFrame.SourcePageType != typeof(Homepage))
            {
                MainFrame.Navigate(typeof(Homepage));
            }
        }

        private void Page2NavItemTapped(object sender, TappedRoutedEventArgs e)
        {
            if (MainFrame.SourcePageType != typeof(Homepage))
            {
                MainFrame.Navigate(typeof(Homepage));
            }
        }

        private void ChangeSizePhoneTapped(object sender, TappedRoutedEventArgs e)
        {
            ApplicationView.GetForCurrentView().TryResizeView(new Size(368, 636));
        }

        private void ChangeSizeDesktopTapped(object sender, TappedRoutedEventArgs e)
        {
            ApplicationView.GetForCurrentView().TryResizeView(new Size(1288, 715));
        }

        private void ChangeSizeXboxTapped(object sender, TappedRoutedEventArgs e)
        {
            ApplicationView.GetForCurrentView().TryResizeView(new Size(976, 538));
        }
    }
}
