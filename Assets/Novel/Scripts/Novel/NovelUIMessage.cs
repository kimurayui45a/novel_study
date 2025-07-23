using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Novel
{

    /// <summary>
    /// ノベルゲームにおけるメッセージ表示UIを制御するクラス
    /// 名前とメッセージの表示、ボタンでの早送りや次への進行を管理する
    /// </summary>
    public class NovelUIMessage : MonoBehaviour
    {
        /// <summary>
        /// 表示するキャラクター名のテキスト
        /// </summary>
        [SerializeField]
        TextMeshProUGUI textName;

        /// <summary>
        /// 表示するメッセージ本文のテキスト
        /// </summary>
        [SerializeField]
        TextMeshProUGUI textMessage;

        /// <summary>
        /// メッセージ早送りや「次へ」を行うボタン
        /// </summary>
        [SerializeField]
        Button buttonNext;

        /// <summary>
        /// 完全なメッセージ内容を保持する（早送り後に一括表示するため）
        /// </summary>
        string displayMessage;

        /// <summary>
        /// 表示前に保存しておくメッセージの色（フェードなどに対応するため）
        /// </summary>
        Color messageColor;

        /// <summary>
        /// メッセージ表示のコルーチン参照（再生中フラグや停止に使用）
        /// </summary>
        Coroutine coroutine;

        /// <summary>
        /// 表示完了後に呼び出すコールバック
        /// </summary>
        Action handleEnd;

        /// <summary>
        /// 現在メッセージ表示中かどうか（コルーチンが動いているか）
        /// </summary>
        public bool IsPlaying => coroutine != null;

        /// <summary>
        /// 初期化処理、ボタンのクリック時の挙動を設定する
        /// </summary>
        private void Awake()
        {
            buttonNext.onClick.AddListener(() =>
            {
                if (IsPlaying)
                {
                    // 表示中なら強制的に全表示
                    Stop();

                } else
                {
                    // 表示完了後なら次の処理へ
                    handleEnd?.Invoke();

                }
            });
        }

        /// <summary>
        /// UI状態のリセット処理、必要に応じてメッセージの初期化も行う
        /// </summary>
        /// <param name="isCleanMessage">名前とメッセージを空にするかどうか</param>
        public void Reset(bool isCleanMessage = false)
        {
            Stop();
            if (isCleanMessage)
            {
                // 各UIに空をセットする
                textName.text = string.Empty;
                textMessage.text = string.Empty;
            }
            handleEnd = null;
        }


        /// <summary>
        /// メッセージを指定の文字送りスピードで表示開始する
        /// </summary>
        /// <param name="name">キャラクター名</param>
        /// <param name="message">表示するメッセージ</param>
        /// <param name="onEnd">表示完了時のコールバック</param>
        /// <param name="duration">1文字あたりの表示間隔（秒）</param>
        public void Play(string name, string message, Action onEnd, float duration = 0.1f)
        {
            Stop();
            displayMessage = message;
            messageColor = textMessage.color;
            handleEnd = onEnd;
            coroutine = StartCoroutine(PlayAction(name, message, duration));

        }

        /// <summary>
        /// 表示中のメッセージを即座に全表示に切り替える
        /// コルーチンを停止し、メッセージを一気に出す
        /// </summary>
        public void Stop()
        {
            if (coroutine == null) return;

            // StopCoroutine：Unityのメソッド、実行中のコルーチン処理を途中で止める
            StopCoroutine(coroutine);

            coroutine = null;
            textMessage.text = displayMessage;
            textMessage.color = messageColor;
            displayMessage = string.Empty;

        }


        /// <summary>
        /// メッセージを1文字ずつ表示する演出コルーチン
        /// 自動改行の計算や行頭禁則処理にも対応
        /// </summary>
        /// <param name="name">キャラクター名</param>
        /// <param name="message">メッセージ本文</param>
        /// <param name="duration">1文字あたりの表示間隔</param>
        /// <returns>IEnumerator（コルーチン）</returns>
        IEnumerator PlayAction(string name, string message, float duration)
        {
            Debug.Log(" Message PlayAction:" + message);
            textName.text = name;

            // TextMeshProによる行頭行末禁則の自動対応（参考：https://qiita.com/yuki-moroto/items/4f7f0a34f4941c699675）
            textMessage.text = message;
            var tempColor = textMessage.color;
            tempColor.a = 0;
            textMessage.color = tempColor;

            // 自動改行の計算のため1フレーム待つ
            yield return null;

            // 改行コードを挿入する
            // 自動改行の処理：各行の visible character を拾い、明示的な \n を追加
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

            // 文字を1つずつ追加表示（文字送り演出）
            string tempMessage = string.Empty;
            foreach (var c in text)
            {
                tempMessage += c;
                textMessage.text = tempMessage;

                // WaitForSeconds：指定した秒数だけ待つ、Unityのメソッド
                yield return new WaitForSeconds(duration);
            }

            coroutine = null;
        }
    }
}