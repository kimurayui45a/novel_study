using System.Collections;
using UnityEngine;

namespace Novel
{

    /// <summary>
    /// メッセージ表示コマンド
    /// キャラクターの名前とセリフを解析し、メッセージUIに表示させる
    /// </summary>
    public class Message: CommandBase
    {

        /// <summary>
        /// 表示するキャラクターの名前
        /// </summary>
        string displayName = string.Empty;

        /// <summary>
        /// 表示するメッセージ内容（セリフ）
        /// </summary>
        string displayMessage = string.Empty;

        /// <summary>
        /// スクリプト1行を解析して、名前とセリフを取り出す
        /// </summary>
        /// <param name="data">カンマ区切りのコマンドデータ（例: Message,"名前","メッセージ"）</param>
        /// <returns>解析に成功した場合は true、失敗した場合は false</returns>
        public override bool Analysis(string[] data)
        {

            // フォーマットチェック：Message,名前,メッセージ
            if (data.Length != 3)
            {
                Debug.LogError($"[Message] command item : {data}");
                return false;
            }

            // ダブルクォートを除去しつつ、名前とセリフを取得
            displayName = data[1].Trim().Replace("\"", "");
            displayMessage = data[2].Trim().Replace("\"", "");

            return true;
        }

        /// <summary>
        /// メッセージUIに対して、指定された名前とセリフを表示させる
        /// フェードや文字送りなどの演出も含む
        /// </summary>
        /// <param name="uiManager">UIを統括する NovelUIManager の参照</param>
        /// <returns>IEnumerator コルーチン（演出本体は UI 側に委譲）</returns>
        public override IEnumerator PlayAction(NovelUIManager uiManager)
        {

            // UIMessage.Play によりメッセージ表示開始
            // 終了時のコールバックで IsEnd フラグを true にする
            uiManager.UIMessage.Play(displayName, displayMessage, () => {
                IsEnd = true;
            });

            // 自分自身の処理は即終了（UI 側で完了を待たせる）
            yield break;
        }
    }
}
