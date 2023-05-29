using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using FreeGamesTool.Model;
using FreeGamesTool.View;

namespace FreeGamesTool.ViewModel
{
    public class MainViewModel : ObservableObject
    {

        public MainViewModel()
        {
            GamePage = new DetailPage();
            OverviewPage = new OverviewPage();
            CurrentPage = OverviewPage;
        }

        public DetailPage GamePage { get; set; }

        public OverviewPage OverviewPage { get; set; }

        private Page _currentPage;

        public Page CurrentPage
        {
            get { return _currentPage; }
            set { SetProperty(ref _currentPage, value); }
        }

        public void NavigateToGameDetail(int ID)
        {
            (GamePage.DataContext as DetailPageVM).CurrentGameId = ID;
            (GamePage.DataContext as DetailPageVM).GetGameData();

            CurrentPage = GamePage;
        }

        public void NavigateToOverview()
        {
            CurrentPage = OverviewPage;
        }
    }
}
