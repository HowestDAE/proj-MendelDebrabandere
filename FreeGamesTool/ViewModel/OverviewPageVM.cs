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
using System.Windows.Input;

namespace FreeGamesTool.ViewModel
{
    public class OverviewPageVM : ObservableObject
    {
        private GameRepository _repository;
        private ObservableCollection<Game> _games;

        public ObservableCollection<Game> Games
        {
            get { return _games; }
            set { SetProperty(ref _games, value); }
        }

        public ICommand ViewGameDetailCommand { get; }

        public OverviewPageVM()
        {
            _repository = new GameRepository();
            Games = new ObservableCollection<Game>();
            LoadGames();

            ViewGameDetailCommand = new RelayCommand<int>(NavigateToGameDetail);
        }

        private async void LoadGames()
        {
            var games = await _repository.GetAllGames();
            foreach (var game in games)
            {
                Games.Add(game);
            }
        }

        private void NavigateToGameDetail(int ID)
        {
            // Access the instance of the MainViewModel and invoke NavigateToGameDetail
            MainViewModel mainViewModel = (MainViewModel)App.Current.MainWindow.DataContext;
            mainViewModel.NavigateToGameDetail(ID);
        }
    }
}
