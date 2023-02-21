using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace WeatherSDK
{
    public class ToastMessage : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_ToastText;

        public void SetProperties(string message = "")
        {
            m_ToastText.text = message;
            m_ToastText.gameObject.SetActive(!string.IsNullOrEmpty(message));
        }
    }
}
