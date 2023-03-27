using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Networking;
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
                return FirstName + " " + LastName;
            }
        }

        public Person(string fname, string lname, string fullname, string profImage)
        {
            FirstName = fname;
            LastName = lname;
            FullName = fullname;
            ProfileImage = profImage;
        }
    }

    public class Musician : Person
    {
        public List<Band> Bands { get; set; } = new List<Band>();
        public List<Album> Albums { get; set; } = new List<Album>();


        public Musician(string fname, string lname, string fullname, string profImage) : base(fname, lname, fullname, profImage)
        {

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
    }

    public class Band
    {
        public string Name { get; set; }
        public List<Musician> Members { get; set; } = new List<Musician>();
        public List<Album> Albums { get; set; } = new List<Album>();

        public Band()
        {
        }

        public void AddAlbum(Album album)
        {
            Albums.Add(album);
            album.AlbumArtist = this;
        }
    }

    public class Album
    {
        public string Name { get; set; }
        public string Year { get; set; }
        public BitmapImage AlbumCover { get; set; }
        public dynamic AlbumArtist { get; set; }

        public Album(string name, string year, BitmapImage cover)
        {
            Name = name;
            Year = year;
            AlbumCover = cover;
        }
    }
}
