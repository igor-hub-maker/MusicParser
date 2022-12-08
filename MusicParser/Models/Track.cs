using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicParser.Models
{
    public class Track
    {
        public Avalonia.Media.Imaging.Bitmap TrackIcon { get; set; }

        public string Name { get; set; }

        public string ArtistName { get; set; }

        public string Ganre { get; set; }

        public string Label { get; set; }

        public string Duration { get; set; }
    }
}
