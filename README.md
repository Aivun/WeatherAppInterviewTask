# WeatherAppInterviewTask

The library is placed in a separate folder under the namespace WeatherSDK. In order to use the weather tools, you need to drag the 'WeatherTools' prefab in the scene. From there, you can drag the functions defined in the 'WeatherTools.cs' script directly as Action Listeners or you can reference them by using 'WeatherTools.Instance.ExampleFunction()'. The library utilizes TextMeshPro (to a small extend) and the DOTween libraries.

Known Issues:
* Display in Fahrenheit degrees is not completed.
* Daily and hourly forcast is skipped due to lack of time.
* On Android, despite all permissions being granted, the location service returns coordinates 0 , 0 - need more time to invastigate.
* Some values (like animation timings and such) may be hardcoded due to lack of time.
