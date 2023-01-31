using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Mosam.Model;
using Newtonsoft.Json;

namespace Mosam.ViewModel.Helper
{
    public class AccuWeatherHelper
    {
        public const string BASE_URL = "http://dataservice.accuweather.com/";
        public const string AUTOCOMPLETE_ENDPOINT = "locations/v1/cities/autocomplete?apikey={0}&q={1}";
        public const string CURRENT_CONDITION_ENDPOINT = "currentconditions/v1/{0}?apikey={1}";
        public const string API_KEY = "ZvJIRXubZ1ADTnSscmWsOljAIKQWH7At";
        public static async Task<List<SearchCity>> GetCities(string query)
        {
            List<SearchCity> cityList = new List<SearchCity>();
            string url = BASE_URL + string.Format(AUTOCOMPLETE_ENDPOINT, API_KEY, query);
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                string json = await response.Content.ReadAsStringAsync();
                cityList = JsonConvert.DeserializeObject<List<SearchCity>>(json);
            }
            return cityList;
        }
        public static async Task<CurrentCondition> GetCurrentCondition(string cityKey)
        {
            CurrentCondition currentCondion = new CurrentCondition();
            string url = BASE_URL + string.Format(CURRENT_CONDITION_ENDPOINT, cityKey, API_KEY);
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                var json = await response.Content.ReadAsStringAsync();
                currentCondion = (JsonConvert.DeserializeObject<List<CurrentCondition>>(json)).FirstOrDefault();
            }

            return currentCondion;
        }

    }
}
