using System.Collections.Generic;
using System.IO;
using System.Net;
using HtmlAgilityPack;
using MusicParser.Models;

namespace MusicParser.Services
{
    public class ParceService
    {
        public List<Track> parce(string url)
        {
            List<Track> tracks = new();
            HtmlWeb htmlWeb = new HtmlWeb();
            HtmlDocument htmlDoc = htmlWeb.Load(url);
            List<string> names = trackName(htmlDoc);
            List<string> artistName = artists(htmlDoc);
            List<string> label = labels(htmlDoc);
            List<string> ganre = ganres(htmlDoc);
            List<string> duration = durations(htmlDoc);
            List<Avalonia.Media.Imaging.Bitmap> icon = icons(htmlDoc);

            for (int i = 0; i < names.Count; i++)
            {
                Track tr = new Track();
                tr.TrackIcon = icon[i];
                tr.Name = names[i];
                tr.Ganre = ganre[i];
                tr.Duration = duration[i];
                if (names.Count != label.Count)
                {
                    tr.ArtistName = artistName[i + 1];
                    tr.Label = label[i + 1];
                }
                else
                {
                    tr.ArtistName = artistName[i];
                    tr.Label = label[i];
                }
                tracks.Add(tr);
            }
            return tracks;
        }

        private List<string> trackName(HtmlDocument html_doc)
        {
            List<string> result = new();
            var getTrackName = html_doc.DocumentNode.SelectNodes("//div[contains(@class,'trk-cell title')]//a");
            foreach (var title in getTrackName)
            {
                string trackName = " ";
                foreach (var t in title.InnerText.Split(' '))
                {
                    if (t == "&amp;")
                    {
                        trackName += "&";
                    }
                    else
                    {
                        trackName += t + " ";
                    }

                }
                result.Add(trackName);
            }
            return result;
        }

        private List<string> artists(HtmlDocument html_doc)
        {
            List<string> result = new();
            var getArtists = html_doc.DocumentNode.SelectNodes("//div[contains(@class,'trk-cell artists')]");
            foreach (var getArtist in getArtists )
            {
                string artist = " ";
                foreach (var t in getArtist.InnerText.Split(' '))
                {
                    if (t == "&amp;")
                    {
                        artist += "&";
                    }
                    else
                    {
                        artist += t + " ";
                    }
                }
                result.Add(artist);
            }
            return result;
        }

        private List<string> labels(HtmlDocument html_doc)
        {
            List<string> result = new();
            var getLabels = html_doc.DocumentNode.SelectNodes("//div[contains(@class,'trk-cell label')]");
            foreach (var getLabel in getLabels)
            {
                string label
                    = " ";
                foreach (var t in getLabel.InnerText.Split(' '))
                {
                    if (t == "&amp;")
                    {
                        label += "&";
                    }
                    else
                    {
                        label += t + " ";
                    }
                }
                result.Add(label);
            }
            return result;
        }

        private List<string> ganres(HtmlDocument html_doc)
        {
            List<string> result = new();
            var getGenres = html_doc.DocumentNode.SelectNodes("//div[contains(@class,'trk-cell genre')]//a");
            foreach (var getGenre in getGenres)
            {
                string ganre = " ";
                foreach (var t in getGenre.InnerText.Split(' '))
                {
                    if (t == "&amp;")
                    {
                        ganre += "&";
                    }
                    else
                    {
                        ganre += t + " ";
                    }
                }
                result.Add(ganre);
            }
            return result;
        }

        private List<string> durations(HtmlDocument html_doc)
        {
            List<string> result = new();
            var getDurations = html_doc.DocumentNode.SelectNodes("//span[contains(@class,'duration')]");
            foreach (var getDuration in getDurations)
            {
                result.Add(getDuration.InnerText);
            }
            return result;
        }
        private List<Avalonia.Media.Imaging.Bitmap> icons(HtmlDocument html_doc)
        {
            List<Avalonia.Media.Imaging.Bitmap> result = new();
            var getIcons = html_doc.DocumentNode.SelectNodes("//div[contains(@class,'trk-cell thumb')]//img");
            foreach (var getIcon in getIcons)
            {
                string uri = getIcon.OuterHtml.Split('"')[1];
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
