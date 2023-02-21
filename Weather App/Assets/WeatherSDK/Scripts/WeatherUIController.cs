using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeatherSDK
{
    public class WeatherUIController : MonoBehaviour
    {
        [SerializeField] private ToastMessage m_ToastPrefab;

        private bool m_ShowToast = true;

        public void ShowToastMessage(string message, float duration = 2f)
        {
            if (!m_ShowToast)
                return;

            m_ShowToast = false;
            var go = Instantiate(m_ToastPrefab, transform);
            go.SetProperties(message);
            var canvasGroup = go.GetComponent<CanvasGroup>();
            Sequence seq = DOTween.Sequence().SetAutoKill(true);

            seq.Append(go.transform.DOScale(1, 0.5f).From(0))
                .Join(canvasGroup.DOFade(0.9f, 0.5f).From(0));

            seq.AppendInterval(duration);

            seq.Append(go.transform.DOScale(0, 0.5f))
                .Join(canvasGroup.DOFade(0f, 0.5f));

            seq.AppendCallback(() => m_ShowToast = true);
        }

    }
}
