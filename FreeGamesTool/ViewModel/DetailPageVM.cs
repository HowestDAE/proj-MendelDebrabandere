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
        private Game _currentGame;
        public Game CurrentGame
        {
            get { return _currentGame; }
            set { SetProperty(ref _currentGame, value); }
        }

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

        private GameRepository _repository;

        public DetailPageVM()
        {
            _repository = new GameRepository();

            // Initialize the commands
            PreviousScreenshotCommand = new RelayCommand(PreviousScreenshot);
            NextScreenshotCommand = new RelayCommand(NextScreenshot);

            // Load the game data and screenshots
            //GetGameData(452);
            GetGameData(200);
        }

        private async void GetGameData(int gameId)
        {
            CurrentGame = await _repository.GetGameById(gameId);
            Screenshots = CurrentGame.Screenshots;
            CurrentScreenshotIndex = 0;
            UpdateScreenshot();
        }

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
        }
    }
}
