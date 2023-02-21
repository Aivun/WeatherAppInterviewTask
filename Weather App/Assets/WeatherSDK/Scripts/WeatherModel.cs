using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeatherSDK
{
    [Serializable]
    public class WeatherModel
    {
        public float latitude;
        public float longitude;
        public float generationtime_ms;
        public int utc_offset_seconds;
        public string timezone;
        public string timezone_abbreviation;
        public float elevation;
        public CurrentWeatherModel current_weather;
    }

    [Serializable]
    public class CurrentWeatherModel
    {
        public float temperature;
        public float windspeed;
        public float winddirection;
        public int weathercode;
        public string time;
    }
}
