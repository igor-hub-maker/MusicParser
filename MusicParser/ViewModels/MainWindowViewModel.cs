using MusicParser.Models;
using MusicParser.Services;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicParser.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private bool isVisible = false;
        private string url;
        private List<Track> tracks = new List<Track>();

        public List<Track> Tracks
        {
            get => tracks;
            set => this.RaiseAndSetIfChanged(ref tracks, value);

        }

        public string Url
        {
            get => url;
            set => this.RaiseAndSetIfChanged(ref url, value);
        }

        public bool IsVisible
        {
            get => isVisible;
            set => this.RaiseAndSetIfChanged(ref isVisible, value);
        }

        public void OnCLick()
        {
            var parceService = new ParceService();
            Tracks = parceService.parce(Url);
            IsVisible = true;
        }
    }
}

