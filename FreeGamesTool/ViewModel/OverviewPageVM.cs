using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FreeGamesTool.Model;
using FreeGamesTool.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FreeGamesTool.ViewModel
{
    public class OverviewPageVM : ObservableObject
    {
        private GameRepository _repository;
        private ObservableCollection<Game> _games;
        private ObservableCollection<Game> _allGames;
        private ObservableCollection<string> _genres;
        private ObservableCollection<string> _platforms;
        private string _selectedGenre;
        private string _selectedPlatform;

        public ObservableCollection<Game> Games
        {
            get { return _games; }
            set { SetProperty(ref _games, value); }
        }
        public ObservableCollection<Game> AllGames
        {
            get { return _allGames; }
            set { SetProperty(ref _allGames, value); }
        }

        public ObservableCollection<string> Genres
        {
            get { return _genres; }
            set { SetProperty(ref _genres, value); }
        }

        public ObservableCollection<string> Platforms
        {
            get { return _platforms; }
            set { SetProperty(ref _platforms, value); }
        }

        public string SelectedGenre
        {
            get { return _selectedGenre; }
            set { SetProperty(ref _selectedGenre, value); }
        }

        public string SelectedPlatform
        {
            get { return _selectedPlatform; }
            set { SetProperty(ref _selectedPlatform, value); }
        }

        public ICommand ViewGameDetailCommand { get; }
        public ICommand ApplyFiltersCommand { get; }

        public OverviewPageVM()
        {
            _repository = new GameRepository();
            Games = new ObservableCollection<Game>();
            AllGames = new ObservableCollection<Game>();
            Genres = new ObservableCollection<string>();
            Platforms = new ObservableCollection<string>();

            ViewGameDetailCommand = new RelayCommand<int>(NavigateToGameDetail);
            ApplyFiltersCommand = new RelayCommand(ApplyFilters);

            LoadGamesAsync();
        }

        private async void LoadGamesAsync()
        {
            await LoadGames();
            InitializeFilters();
        }

        private async Task LoadGames()
        {
            var games = await _repository.GetAllGames();
            foreach (var game in games)
            {
                AllGames.Add(game);
            }

            Games = new ObservableCollection<Game>(AllGames);
        }

        private void InitializeFilters()
        {
            Genres.Add(String.Empty);
            Genres.Add("MMO");
            Genres.Add("RPG");
            Genres.Add("Shooter");
            Genres.Add("MOBA");
            Genres.Add("Battle Royale");
            Genres.Add("Strategy");
            Genres.Add("Fantasy");
            Genres.Add("Card Game");
            Genres.Add("Racing");
            Genres.Add("Fighting");
            Genres.Add("Social");
            Genres.Add("Sports");
            Platforms.Add(String.Empty);
            Platforms.Add("Windows");
            Platforms.Add("Web Browser");
        }
        
        private async void ApplyFilters()
        {
            await Application.Current.Dispatcher.InvokeAsync(() =>
            {
                IEnumerable<Game> filteredGames = AllGames;

                if (!string.IsNullOrEmpty(SelectedGenre))
                {
                    filteredGames = filteredGames.Where(game => game.Genre.Contains(SelectedGenre));
                }

                if (!string.IsNullOrEmpty(SelectedPlatform))
                {
                    filteredGames = filteredGames.Where(game => game.Platform.Contains(SelectedPlatform));
                }


                Games.Clear();

                foreach (var game in filteredGames)
                {
                    Games.Add(game);
                }
            });
        }

        private void NavigateToGameDetail(int ID)
        {
            // Access the instance of the MainViewModel and invoke NavigateToGameDetail
            MainViewModel mainViewModel = (MainViewModel)App.Current.MainWindow.DataContext;
            mainViewModel.NavigateToGameDetail(ID);
        }
    }
}
