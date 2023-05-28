using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using FreeGamesTool.View;

namespace FreeGamesTool.ViewModel
{
    public class MainViewModel : ObservableObject
    {

        public MainViewModel()
        {
            GamePage = new DetailPage();
            CurrentPage = GamePage;
        }

        public DetailPage GamePage { get; set; }

        private Page _currentPage;

        public Page CurrentPage
        {
            get { return _currentPage; }
            set
            {
                _currentPage = value;
                OnPropertyChanged(nameof(CurrentPage));
            }
        }
    }
}
