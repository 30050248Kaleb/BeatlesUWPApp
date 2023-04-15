using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Windows.Devices.Geolocation;
using Windows.Networking;
using Windows.Networking.NetworkOperators;
using Windows.UI.Xaml.Media.Imaging;

namespace BeatlesApp
{
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string ProfileImage { get; set; }

        public BasicGeoposition Location { get; set; }

        public string FirstLastName
        {
            get
            {
                if (LastName == String.Empty)
                {
                    return FirstName;
                }
                else
                {
                    return FirstName + " " + LastName;
                }
            }
        }

        public Person(string fname, string lname, string fullname)
        {
            FirstName = fname;
            LastName = lname;
            FullName = fullname;
        }
    }

    public class Musician : Person
    {
        public List<Band> Bands { get; set; } = new List<Band>();
        public ObservableCollection<Album> Albums { get; set; } = new ObservableCollection<Album>();


        public Musician(string fname, string lname, string fullname) : base(fname, lname, fullname)
        {
            if(ProfileImage == string.Empty)
                GetPicture();
            GetAlbums();
        }

        public void AddToBand(Band band)
        {
            Bands.Add(band);
            band.Members.Add(this);
        }

        public void AddAlbum(Album album)
        {
            Albums.Add(album);
            album.AlbumArtist = this;
        }

        public void GetPicture()
        {
            // Generated with GitHub Copilot
            var httpClient = new HttpClient();
            var getPictureRequestUri = new Uri($"http://ws.audioscrobbler.com/2.0/?method=artist.getinfo&artist={FirstLastName}&api_key={App.apiKey}&format=json");
            var artistPictureJson = httpClient.GetStringAsync(getPictureRequestUri).Result;
            dynamic pictureResult = JsonConvert.DeserializeObject(artistPictureJson);
            try
            {
                var bitmap = new BitmapImage(new Uri(pictureResult["artist"]["image"].Last["#text"].ToString(), UriKind.Absolute));
                ProfileImage = bitmap.UriSource.ToString();
            }
            catch (Exception ex)
            {
                var bitmap = new BitmapImage(new Uri("ms-appx:///Assets/Images/logo.png", UriKind.RelativeOrAbsolute));
                ProfileImage = bitmap.UriSource.ToString();
            }
        }

        public async void GetAlbums()
        {
            try
            {
                var httpClient = new HttpClient();
                var getAlbumsRequestUri = new Uri($"http://ws.audioscrobbler.com/2.0/?method=artist.gettopalbums&artist={FirstLastName}&api_key={App.apiKey}&format=json");
                var artistTopAlbumsJson = await httpClient.GetStringAsync(getAlbumsRequestUri);
                dynamic topAlbumsResult = JsonConvert.DeserializeObject(artistTopAlbumsJson);

                var albums = topAlbumsResult.topalbums.album;
                Albums = new ObservableCollection<Album>();
                foreach (var album in albums)
                {
                    try
                    {
                        // Album Name
                        string name = album["name"].ToString();

                        // Encode the album name, incase it has &s in it, to be able to use it in the url
                        if (album["name"].ToString().Contains("&") || album["name"].ToString().Contains(" "))
                        {
                            name = HttpUtility.UrlEncode(name);
                        }
                        var albumInfoRequestUri = new Uri($"http://ws.audioscrobbler.com/2.0/?method=album.getinfo&api_key={App.apiKey}&artist={FirstLastName}&album={name}&format=json");
                        var albumJson = await httpClient.GetStringAsync(albumInfoRequestUri);
                        dynamic albumResult = JsonConvert.DeserializeObject(albumJson);

                        // Album Cover
                        try
                        {
                            var bitmap = new BitmapImage(new Uri(albumResult["album"]["image"].Last["#text"].ToString(), UriKind.Absolute));

                            AddAlbum(new Album(album["name"].ToString(), bitmap, albumResult));
                        }
                        catch (Exception ex)
                        {
                            var bitmap = ProfileImage;
                        }
                    }
                    catch { }

                }
            }
            catch { }
        }
    }

    public class Band
    {
        public string Name { get; set; }
        public List<Musician> Members { get; set; } = new List<Musician>();
        public ObservableCollection<Album> Albums { get; set; } = new ObservableCollection<Album>();

        public Band(string name)
        {
            Name = name;
            GetAlbums();
        }

        public void AddAlbum(Album album)
        {
            Albums.Add(album);
            album.AlbumArtist = this;
        }

        public async void GetAlbums()
        {
            try
            {
                var httpClient = new HttpClient();
                var getAlbumsRequestUri = new Uri($"http://ws.audioscrobbler.com/2.0/?method=artist.gettopalbums&artist={Name}&api_key={App.apiKey}&format=json");
                var artistTopAlbumsJson = await httpClient.GetStringAsync(getAlbumsRequestUri);
                dynamic topAlbumsResult = JsonConvert.DeserializeObject(artistTopAlbumsJson);

                var albums = topAlbumsResult.topalbums.album;
                Albums = new ObservableCollection<Album>();
                foreach (var album in albums)
                {
                    try
                    {
                        // Album Name
                        string name = album["name"].ToString();

                        // Encode the album name, incase it has &s in it, to be able to use it in the url
                        if (album["name"].ToString().Contains("&") || album["name"].ToString().Contains(" "))
                        {
                            name = HttpUtility.UrlEncode(name);
                        }
                        var albumInfoRequestUri = new Uri($"http://ws.audioscrobbler.com/2.0/?method=album.getinfo&api_key={App.apiKey}&artist={Name}&album={name}&format=json");
                        var albumJson = await httpClient.GetStringAsync(albumInfoRequestUri);
                        dynamic albumResult = JsonConvert.DeserializeObject(albumJson);

                        // Album Cover
                        try
                        {
                            var bitmap = new BitmapImage(new Uri(albumResult["album"]["image"].Last["#text"].ToString(), UriKind.Absolute));

                            AddAlbum(new Album(album["name"].ToString(), bitmap, albumResult));
                        }
                        catch (Exception ex)
                        {
                            var bitmap = new BitmapImage(new Uri(""));
                        }
                    }
                    catch { }

                }
            }
            catch { }
        }
    }

    public class Album
    {
        public string Name { get; set; }
        public BitmapImage AlbumCover { get; set; }
        public dynamic AlbumArtist { get; set; }
        public ObservableCollection<string> Tracklist { get; set; } = new ObservableCollection<string>();
        public dynamic AlbumJson { get; set; }

        public Album(string name, BitmapImage cover, dynamic albumJson)
        {
            Name = name;
            AlbumCover = cover;
            AlbumJson = albumJson;
        }
    }
}
