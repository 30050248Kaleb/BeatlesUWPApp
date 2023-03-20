using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace BeatlesApp.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Homepage : Page
    {
        List<Musician> musicians = new List<Musician>();

        public Homepage()
        {
            this.InitializeComponent();


            
        }

        private void JohnTapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(PersonInfo), App.theBeatles.Members[0]);
        }

        private void MembersGridView_ItemClick(object sender, TappedRoutedEventArgs e)
        {

            Frame.Navigate(typeof(PersonInfo), App.theBeatles.Members[MembersGridView.SelectedIndex]);
        }
    }
}
