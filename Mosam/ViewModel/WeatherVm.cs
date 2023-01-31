using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mosam.Model;
using Mosam.ViewModel.Commands;
using Mosam.ViewModel.Helper;

namespace Mosam.ViewModel
{
    public class WeatherVm : INotifyPropertyChanged
    {
        private string query;

        public string Query
        {
            get { return query; }
            set 
            { 
                query = value;
                OnPropertyChanged("Query");
            }
        }

        private CurrentCondition currentCondition;

        public CurrentCondition CurrentCondition
        {
            get { return currentCondition; }
            set {
                currentCondition = value;
                OnPropertyChanged("CurrentCondition");
            }
        }

        private SearchCity selectedCity;

        public SearchCity SelectedCity
        {
            get { return selectedCity; }
            set {
                selectedCity = value;
                OnPropertyChanged("SelectedCity");
                GetCurrentConditions();
            }
        }

        public SearchCommand SearchCommand { get; set; }
        public async void MakeQuery()
        {
            var cities = await AccuWeatherHelper.GetCities(Query);
            Cities.Clear();
            foreach (var city in cities)
            {
                Cities.Add(city);
            }
        }

        private async void GetCurrentConditions()
        {
            Query = String.Empty;
            Cities.Clear();
            CurrentCondition = await AccuWeatherHelper.GetCurrentCondition(SelectedCity.Key);
        }
        public ObservableCollection<SearchCity> Cities { get; set; }
        public WeatherVm()
        {
            SearchCommand = new SearchCommand(this);
            Cities = new ObservableCollection<SearchCity>();
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
