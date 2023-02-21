using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

namespace WeatherSDK
{
    public class LocationTools : MonoBehaviour
    {
        public bool IsLocationReadyToUse { private set; get; } = false;
        public string LocationServiceError { private set; get; } = "";

        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(StartServiceCoroutine());
        }

        public bool GetLatestLocation(out LocationInfo info)
        {
            if (IsLocationReadyToUse)
                info = Input.location.lastData;
            else
                info = default;

            return IsLocationReadyToUse;
        }

        public void RestartService()
        {
            IsLocationReadyToUse = false;

            if (Input.location.status == LocationServiceStatus.Running)
                Input.location.Stop();

            StartCoroutine(StartServiceCoroutine());
        }

        private IEnumerator StartServiceCoroutine()
        {
            // Check if the user has location service enabled.
            if (!Input.location.isEnabledByUser)
            {
                Permission.RequestUserPermission(Permission.FineLocation);
            }

            // Starts the location service.
            Input.location.Start();

            // Waits until the location service initializes
            int maxWait = 20;
            while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
            {
                yield return new WaitForSeconds(1);
                maxWait--;
            }

            // If the service didn't initialize in 20 seconds, this cancels location service use.
            if (maxWait < 1)
            {
                LocationServiceError = "Location timed out";
                yield break;
            }

            // If the connection failed, this cancels location service use.
            if (Input.location.status == LocationServiceStatus.Failed)
            {
                LocationServiceError = "Unable to determine device location";
                yield break;
            }
            else
            {
                IsLocationReadyToUse = true;
            }
        }

    }
}

