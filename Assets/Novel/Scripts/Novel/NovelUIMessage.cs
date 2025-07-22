using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Novel
{

    public class NovelUIMessage : MonoBehaviour
    {
        [SerializeField]
        TextMeshProUGUI textName;
        [SerializeField]
        TextMeshProUGUI textMessage;
        [SerializeField]
        Button buttonNext;

        string displayMessage;
        Color messageColor;
        Coroutine coroutine;
        Action handleEnd;

        public bool IsPlaying => coroutine != null;

        private void Awake()
        {
            buttonNext.onClick.AddListener(() =>
            {
                if (IsPlaying)
                {
                    Stop();
                } else
                {
                    handleEnd?.Invoke();
                }
            });
        }
        public void Reset(bool isCleanMessage = false)
        {
            Stop();
            if (isCleanMessage)
            {
                textName.text = string.Empty;
                textMessage.text = string.Empty;
            }
            handleEnd = null;
        }

        public void Play(string name, string message, Action onEnd, float duration = 0.1f)
        {
            Stop();
            displayMessage = message;
            messageColor = textMessage.color;
            handleEnd = onEnd;
            coroutine = StartCoroutine(PlayAction(name, message, duration));

        }
        public void Stop()
        {
            if (coroutine == null) return;
            StopCoroutine(coroutine);
            coroutine = null;
            textMessage.text = displayMessage;
            textMessage.color = messageColor;
            displayMessage = string.Empty;

        }
        IEnumerator PlayAction(string name, string message, float duration)
        {
            Debug.Log(" Message PlayAction:" + message);
            textName.text = name;
            // TextMeshPro�ɂ��s���s���֑��̎����Ή��i�Q�l�Fhttps://qiita.com/yuki-moroto/items/4f7f0a34f4941c699675�j
            textMessage.text = message;
            var tempColor = textMessage.color;
            tempColor.a = 0;
            textMessage.color = tempColor;
            // �������s�̌v�Z�̂���1�t���[���҂�
            yield return null;
            // ���s�R�[�h��}������
            var lineInfo = textMessage.textInfo.lineInfo;
            string text = string.Empty;
            foreach (var info in lineInfo)
            {
                if (info.visibleCharacterCount > 0)
                {
                    text += textMessage.text.Substring(info.firstVisibleCharacterIndex, info.visibleCharacterCount) + "\n";
                }
            }
            displayMessage = text;
            tempColor.a = 1;
            textMessage.color = tempColor;
            string tempMessage = string.Empty;
            foreach (var c in text)
            {
                tempMessage += c;
                textMessage.text = tempMessage;
                yield return new WaitForSeconds(duration);
            }

            coroutine = null;
        }
    }
}