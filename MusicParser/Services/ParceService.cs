using System.Collections.Generic;
using System.IO;
using System.Net;
using HtmlAgilityPack;
using MusicParser.Models;

namespace MusicParser.Services
{
    public class ParceService
    {

        public List<Track> Parce(string url)
        {

            List<Track> tracks = new();
            HtmlWeb htmlWeb = new HtmlWeb();
            HtmlDocument htmlDoc = htmlWeb.Load(url);
            List<string> names = GetTrackNames(htmlDoc);
            List<string> artistNames = GetArtists(htmlDoc);
            List<string> labels = GetLabels(htmlDoc);
            List<string> ganres = GetGanres(htmlDoc);
            List<string> durations = GetDurations(htmlDoc);
            List<Avalonia.Media.Imaging.Bitmap> icons = GetIcons(htmlDoc);

            for (int i = 0; i < names.Count; i++)
            {
                Track track = new Track();
                track.TrackIcon = icons[i];
                track.Name = names[i];
                track.Ganre = ganres[i];
                track.Duration = durations[i];
                if (names.Count != labels.Count)
                {
                    track.ArtistName = artistNames[i + 1];
                    track.Label = labels[i + 1];
                }
                else
                {
                    track.ArtistName = artistNames[i];
                    track.Label = labels[i];
                }
                tracks.Add(track);
            }
            return tracks;
        }

        private List<string> GetTrackNames(HtmlDocument htmlDoc)
        {

            var result = new List<string>();
            var NonSortTrackNames = htmlDoc.DocumentNode.SelectNodes("//div[contains(@class,'trk-cell title')]//a");
            foreach (var NonSortTrackName in NonSortTrackNames)
            {
                var trackName = NonSortTrackName.InnerText.Split(' ');
                string trackResult = "";
                foreach (var title in trackName)
                {
                    if (title == "&amp;")
                    {
                        trackResult += "&";
                    }
                    else
                    {
                        trackResult += title + " ";
                    }

                }
                result.Add(trackResult);
            }
            return result;
        }

        private List<string> GetArtists(HtmlDocument htmlDoc)
        {

            var result = new List<string>();
            var NonSortArtists = htmlDoc.DocumentNode.SelectNodes("//div[contains(@class,'trk-cell artists')]");
            foreach (var NonSortArtist in NonSortArtists )
            {
                var artists = NonSortArtist.InnerText.Split(' ');
                string artistResult = "";
                foreach (var artist in artists)
                {
                    if (artist == "&amp;")
                    {
                        artistResult += "&";
                    }
                    else
                    {
                        artistResult += artist + " ";
                    }
                }
                result.Add(artistResult);
            }
            return result;
        }

        private List<string> GetLabels(HtmlDocument htmlDoc)
        {

            var result = new List<string>();
            var NonSortLabels = htmlDoc.DocumentNode.SelectNodes("//div[contains(@class,'trk-cell label')]");
            foreach (var NonSortLabel in NonSortLabels)
            {
                var labels = NonSortLabel.InnerText.Split(' ');
                string labelResult = "";
                foreach (var label in labels)
                {
                    if (label == "&amp;")
                    {
                        labelResult += "&";
                    }
                    else
                    {
                        labelResult += label + " ";
                    }
                }
                result.Add(labelResult);
            }
            return result;
        }

        private List<string> GetGanres(HtmlDocument htmlDoc)
        {

            var result = new List<string>();
            var NonSortGenres = htmlDoc.DocumentNode.SelectNodes("//div[contains(@class,'trk-cell genre')]//a");
            foreach (var NonSortGenre in NonSortGenres)
            {
                var ganres = NonSortGenre.InnerText.Split(' ');
                string ganreResult = "";
                foreach (var ganre in ganres)
                {
                    if (ganre == "&amp;")
                    {
                        ganreResult += "&";
                    }
                    else
                    {
                        ganreResult += ganre + " ";
                    }
                }
                result.Add(ganreResult);
            }
            return result;
        }

        private List<string> GetDurations(HtmlDocument htmlDoc)
        {

            var result = new List<string>();
            var NonSortDurations = htmlDoc.DocumentNode.SelectNodes("//span[contains(@class,'duration')]");
            foreach (var duration in NonSortDurations)
            {
                result.Add(duration.InnerText);
            }
            return result;
        }

        private List<Avalonia.Media.Imaging.Bitmap> GetIcons(HtmlDocument htmlDoc)
        {

            var result = new List<Avalonia.Media.Imaging.Bitmap>();
            var IconsUris = htmlDoc.DocumentNode.SelectNodes("//div[contains(@class,'trk-cell thumb')]//img");
            foreach (var IconUri in IconsUris)
            {
                string uri = IconUri.OuterHtml.Split('"')[1];
                result.Add(new(ImageDataFromUrl(uri)));
            }
            return result;
        }

        private static Stream ImageDataFromUrl(string url)
        {

            byte[] imageData = null;

            using (var wc = new WebClient())
                imageData = wc.DownloadData(url);
            return new MemoryStream(imageData);
        }
    }
}
