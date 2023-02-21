using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace WeatherSDK
{
    public class ServerComm : MonoBehaviour
    {
        [SerializeField] private string API_Address = "https://api.open-meteo.com/v1/forecast?";

        public async Task<WeatherModel> RefreshCurrentWeatherData(float latitude, float longitude)
        {
            var ci = new CultureInfo("en-US");
            string url = $"{API_Address}latitude={latitude.ToString(ci)}&longitude={longitude.ToString(ci)}&current_weather=true";
            using var www = UnityWebRequest.Get(url);
            var operation = www.SendWebRequest();

            while (!operation.isDone)
                await Task.Yield();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Refresh Current Weather Data Failed: {www.error}");
                Debug.Log(url);
                return null;
            }

            return JsonUtility.FromJson<WeatherModel>(www.downloadHandler.text);
        }
    }
}
