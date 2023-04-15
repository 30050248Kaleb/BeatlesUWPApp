using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
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
using Newtonsoft.Json.Linq;
using static System.Net.Mime.MediaTypeNames;
using System.Reflection.PortableExecutable;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace BeatlesApp.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AlbumInfo : Page
    {
        Album mainAlbum;
        List<string> trackList = new List<string>();
        public AlbumInfo()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            mainAlbum = (Album)e.Parameter;
            SetInformationForPage();
        }

        private async void SetInformationForPage()
        {
            TextBlockTracks.Text = "Loading tracks...";
            TextBlockTracks.Visibility = Visibility.Visible;

            try { TextBlockAlbumName.Text = mainAlbum.Name; }
            catch (Exception ex) { TextBlockAlbumName.Text = "Error: " + ex; }

            try 
            { 
                if(mainAlbum.AlbumArtist is Band)
                {
                    TextBlockArtistName.Text = mainAlbum.AlbumArtist.Name;
                }
                else
                {
                    TextBlockArtistName.Text = mainAlbum.AlbumArtist.FirstLastName;
                }
            }
            catch (Exception ex) { TextBlockArtistName.Text = "Error: " + ex; }

            try { ImageProfileImage.Source = mainAlbum.AlbumCover; }
            catch (Exception) { ImageProfileImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/Images/logo.png", UriKind.RelativeOrAbsolute)); }
            
            try
            {
                // Set About Text
                try
                {
                    dynamic tracks = mainAlbum.AlbumJson.album.tracks.track;
                    if (tracks != null)
                    {
                        TextBlockTracks.Visibility = Visibility.Collapsed;
                        int trackCounter = 1;
                        foreach (JObject track in tracks)
                        {
                            trackList.Add(trackCounter + ". " + track.GetValue("name").ToString());
                            trackCounter++;
                        }
                    }
                    else
                    {
                        TextBlockTracks.Text = "No tracks found";
                        TextBlockTracks.Visibility = Visibility.Visible;
                    }
                }
                catch (Exception ex)
                {
                    TextBlockTracks.Text = "No tracks found";
                    TextBlockTracks.Visibility = Visibility.Visible;
                }
            }
            catch (Exception)
            {

            }
        }
    }
}
