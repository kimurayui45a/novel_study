using System;
using System.Collections;
using UnityEngine;

namespace Novel
{

    public class NovelUIChara : MonoBehaviour
    {
        static readonly string RootPath = "Chara/";

        [SerializeField]
        GameObject parent;

        CharaAsset prevChara;
        CharaAsset chara;
        Coroutine coroutine;
        public bool IsPlaying => coroutine != null;

        public void Render(string charaName, Action onEnd, float duration = 1.0f)
        {
            coroutine = StartCoroutine(PlayAction(charaName, onEnd, duration));

        }
        IEnumerator PlayAction(string charaName, Action onEnd, float duration)
        {
            var charaAsset = Resources.Load<CharaAsset>(RootPath + charaName);

            prevChara = chara;
            chara = Instantiate(charaAsset, parent.transform);

            var isActiveFadeOut = false;
            if (prevChara != null)
            {
                isActiveFadeOut = true;
                StartCoroutine(prevChara.FadeOut( () =>
                {
                    isActiveFadeOut = false;
                }));
            }
            var isActiveFadeIn = true;
            StartCoroutine(chara.FadeIn( () =>
            {
                isActiveFadeIn = false;
            }));

            while (isActiveFadeIn || isActiveFadeOut)
            {
                yield return null;
            }

            if (prevChara != null)
            {
                Destroy(prevChara.gameObject);
                prevChara = null;
            }

            coroutine = null;

            onEnd.Invoke();
        }
    }
}