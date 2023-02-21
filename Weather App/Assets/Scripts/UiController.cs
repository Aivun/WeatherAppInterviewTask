using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    [SerializeField] private Text m_TempText;
    [SerializeField] private Text m_WindSpText;
    [SerializeField] private Text m_windDirText;
    [SerializeField] private Text m_DescText;
    [SerializeField] private Text m_LocationText;
    [SerializeField] private Button m_WeatherDetailsButton;

    private void Start()
    {
        m_WeatherDetailsButton.onClick.AddListener(UpdateWeatherDetails);
    }

    private async void UpdateWeatherDetails()
    {
        var result = await WeatherSDK.WeatherTools.Instance.GetCurrentWeatherDetails();

        if (result != null && result.current_weather != null)
        {
            m_TempText.text = result.current_weather.temperature + " °C";
            m_WindSpText.text = result.current_weather.windspeed + " m/s";
            m_windDirText.text = result.current_weather.winddirection + "°";
            m_DescText.text = WeatherSDK.WeatherTools.Instance.GetTextForWeatherCode(result.current_weather.weathercode);
            m_LocationText.text = Input.location.lastData.latitude + " | " + Input.location.lastData.longitude;
        }
    }
}
