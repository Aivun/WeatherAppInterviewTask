using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Schema;
using UnityEngine;

namespace WeatherSDK
{
    public class WeatherTools : MonoBehaviour
    {
        public enum TemperatureUnits { Celsius = 0, Fahrenheit = 1 }

        [SerializeField] private LocationTools m_LocationTools;
        [SerializeField] private ServerComm m_ServerComm;
        [SerializeField] private CodesDatabase m_CodesDB;
        [SerializeField] private WeatherUIController m_UiController;
        [SerializeField] private TemperatureUnits m_WeatherUnits;

        public static WeatherTools Instance { private set; get; }

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        public async Task<WeatherModel> GetCurrentWeatherDetails()
        {
#if UNITY_EDITOR // for Testing
           
                var result = await m_ServerComm.RefreshCurrentWeatherData(19.125f, 72.875f);

                if (result == null)
                {
                    m_UiController.ShowToastMessage("Data not available.");
                }
                return result;

#else
            LocationInfo location;
            if (m_LocationTools.GetLatestLocation(out location))
            {

                var result = await m_ServerComm.RefreshCurrentWeatherData(location.latitude, location.longitude);

                if (result == null)
                {
                    m_UiController.ShowToastMessage("Data not available.");
                }
                return result;
            }
            else
            {
                m_UiController.ShowToastMessage("Location Could not be retrieved.");
                Debug.LogError(m_LocationTools.LocationServiceError);
            }
            return null;
#endif
        }

        public async void GetCurrentTemperature()
        {
#if UNITY_EDITOR // for Testing
            var result = await m_ServerComm.RefreshCurrentWeatherData(19.125f, 72.875f);

            if (result != null)
            {
                string output = $"{GetCorrectDegrees(result.current_weather.temperature)} {GetCorrectSuffix()} {GetTextForWeatherCode(result.current_weather.weathercode)}";
                Debug.Log(output);
                m_UiController.ShowToastMessage(output);
            }
            else
            {
                m_UiController.ShowToastMessage("Data not available.");
            }

            return;
#else


            LocationInfo location;
            if (m_LocationTools.GetLatestLocation(out location))
            {

                var result = await m_ServerComm.RefreshCurrentWeatherData(location.latitude, location.longitude);

                if (result != null)
                {
                    string output = $"{GetCorrectDegrees(result.current_weather.temperature)} {GetCorrectSuffix()} {m_CodesDB.GetTextForCode(result.current_weather.weathercode)}";
                    Debug.Log(output);
                    m_UiController.ShowToastMessage(output);
                }
                else
                {
                    m_UiController.ShowToastMessage("Data not available.");
                }
            }
            else
            {
                m_UiController.ShowToastMessage("Location Could not be retrieved.");
                Debug.LogError(m_LocationTools.LocationServiceError);
            }
#endif
        }

        public float GetCorrectDegrees(float degrees)
        {
            return m_WeatherUnits == TemperatureUnits.Celsius ? degrees : ((degrees * (1.8f)) + 32);
        }

        public string GetCorrectSuffix()
        {
            return m_WeatherUnits == TemperatureUnits.Celsius ? "°C" : "°F";
        }

        public string GetTextForWeatherCode(int code)
        {
            return m_CodesDB.GetTextForCode(code);
        }
    }
}
