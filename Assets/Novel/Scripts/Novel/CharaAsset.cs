using System;
using System.Collections;
using UnityEngine;

namespace Novel
{

    public class CharaAsset : MonoBehaviour
    {
        [SerializeField]
        CanvasGroup canvasGroup;

        public IEnumerator FadeIn(Action onEnd, float duration = 0.5f)
        {
            float time = 0;
            canvasGroup.alpha = 0;
            while (time < duration)
            {
                time += Time.deltaTime;
                canvasGroup.alpha = Mathf.Min(1.0f, time / duration);
                yield return null;
            }
            canvasGroup.alpha = 1.0f;
            onEnd();
        }
        public IEnumerator FadeOut(Action onEnd, float duration = 0.5f)
        {
            float time = 0;
            canvasGroup.alpha = 1.0f;
            while (time < duration)
            {
                time += Time.deltaTime;
                canvasGroup.alpha = Mathf.Max(0.0f, 1.0f - time / duration);
                yield return null;
            }
            canvasGroup.alpha = 0.0f;
            onEnd();
        }

    }
}