using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FreeGamesTool.Model;
using FreeGamesTool.Repository;

namespace FreeGamesTool.ViewModel
{
    public class DetailPageVM : ObservableObject
    {
        //GAME
        private Game _currentGame;
        public Game CurrentGame
        {
            get { return _currentGame; }
            set { SetProperty(ref _currentGame, value); }
        }

        //GAME ID CHANGER
        private int _currentGameId;
        public int CurrentGameId
        {
            get { return _currentGameId; }
            set { SetProperty(ref _currentGameId, value); }
        }
        public ICommand PreviousGameCommand { get; }
        public ICommand NextGameCommand { get; }
        //**********************************

        //GAME SCREENSHOT CHANGER
        private List<Screenshot> _screenshots;
        public List<Screenshot> Screenshots
        {
            get { return _screenshots; }
            set { SetProperty(ref _screenshots, value); }
        }
        private Screenshot _currentScreenshot;
        public Screenshot CurrentScreenshot
        {
            get { return _currentScreenshot; }
            set { SetProperty(ref _currentScreenshot, value); }
        }
        private int _currentScreenshotIndex;
        public int CurrentScreenshotIndex
        {
            get { return _currentScreenshotIndex; }
            set { SetProperty(ref _currentScreenshotIndex, value); }
        }
        public ICommand PreviousScreenshotCommand { get; }
        public ICommand NextScreenshotCommand { get; }
        //**********************************

        //Repo
        private GameRepository _repository;

        //Constructor
        public DetailPageVM()
        {
            _repository = new GameRepository();

            // Initialize the commands
            PreviousScreenshotCommand = new RelayCommand(PreviousScreenshot);
            NextScreenshotCommand = new RelayCommand(NextScreenshot);

            PreviousGameCommand = new RelayCommand(PreviousGame);
            NextGameCommand = new RelayCommand(NextGame);

            ViewOverviewCommand = new RelayCommand(NavigateToOverview);

            CurrentGameId = 325;
            GetGameData();
        }

        //Async data loader
        public async void GetGameData()
        {
            CurrentGame = await _repository.GetGameById(CurrentGameId);
            //If the game isnt found, create an empty game class to display that its not found
            if (CurrentGame == null)
            {
                CurrentGame = new Game(CurrentGameId);
            }

            Screenshots = CurrentGame.Screenshots;
            CurrentScreenshotIndex = 0;
            UpdateScreenshot();
        }

        //Screenshot changer commands
        private void PreviousScreenshot()
        {
            CurrentScreenshotIndex--;
            if (CurrentScreenshotIndex < 0)
                CurrentScreenshotIndex = Screenshots.Count - 1;
            UpdateScreenshot();
        }
        private void NextScreenshot()
        {
            CurrentScreenshotIndex++;
            if (CurrentScreenshotIndex >= Screenshots.Count)
                CurrentScreenshotIndex = 0;
            UpdateScreenshot();
        }
        private void UpdateScreenshot()
        {
            if (Screenshots.Count > 0)
            {
                CurrentScreenshot = Screenshots[_currentScreenshotIndex];
            }
            else
            {
                CurrentScreenshot = null;
            }
        }
        //****************************************

        //Game Id changers
        private void PreviousGame()
        {
            CurrentGameId--;
            if (CurrentGameId < 1)
                CurrentGameId = 552;
            GetGameData();
        }
        private void NextGame()
        {
            CurrentGameId++;
            if (CurrentGameId > 552)
                CurrentGameId = 1;
            GetGameData();
        }
        //****************************************

        // Go to overview button command
        public ICommand ViewOverviewCommand { get; }
        private void NavigateToOverview()
        {
            // Access the instance of the MainViewModel and invoke NavigateToGameDetail
            MainViewModel mainViewModel = (MainViewModel)App.Current.MainWindow.DataContext;
            mainViewModel.NavigateToOverview();
        }
    }
}
