using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Novel
{

    /// <summary>
    /// キャラクター表示コマンドを実装するクラス
    /// CommandBase を継承し、CSVからの解析とキャラの表示を制御する
    /// </summary>
    public class Chara : CommandBase
    {
        /// <summary>
        /// キャラクター表示位置（左・中央・右）を定義する列挙型
        /// </summary>
        enum Position
        {
            Left,
            Right,
            Center,
        }

        /// <summary>
        /// 表示するキャラクタープレハブの名前（Resources/Chara/内にある）
        /// </summary>
        string assetName = string.Empty;

        /// <summary>
        /// キャラクターの表示位置（列挙型）
        /// </summary>
        Position position = Position.Center;

        /// <summary>
        /// フェード表示にかける時間（秒）
        /// </summary>
        float duration = 0.0f;


        /// <summary>
        /// コマンドのスクリプト行を解析して、キャラクター情報を取り出す
        /// </summary>
        /// <param name="data">CSVで分割されたスクリプトの1行</param>
        /// <returns>解析が成功したか（true/false）</returns>
        public override bool Analysis(string[] data)
        {
            // フォーマットチェック： Chara,"キャラ名",位置,時間
            if (data.Length != 4)
            {
                Debug.LogError($"[Chara] command item : {data}");
                return false;
            }

            // キャラ名（" を除去）
            assetName = data[1].Trim().Replace("\"", "");

            // 位置を文字列で判定し、列挙型に変換
            var dir = data[2].Trim();
            switch (dir)
            {
                case "Left":
                    position = Position.Left; break;
                case "Center":
                    position = Position.Center; break;
                case "Right":
                    position = Position.Right; break;
                default:
                    Debug.LogError($"[Chara] command  data[2] : {data[2]}");
                    return false;
            }

            // 時間（文字列 → float）に変換
            if (!float.TryParse(data[3].Trim(), out duration))
            {
                Debug.LogError($"[Chara] command  data[3] : {data[3]}");
            }
            return true;
        }

        /// <summary>
        /// キャラクター表示の実行処理
        /// UIの指定位置にキャラをフェードイン表示させる
        /// </summary>
        /// <param name="uiManager">UI全体を管理する NovelUIManager</param>
        /// <returns>コルーチン</returns>
        public override IEnumerator PlayAction(NovelUIManager uiManager)
        {
            NovelUIChara target = null;

            // 表示位置に応じて表示先の UIChara を取得
            switch (position)
            {
                case Position.Left:
                    target = uiManager.UICharaLeft;
                    break;
                case Position.Center:
                    target = uiManager.UICharaCenter;
                    break;
                case Position.Right:
                    target = uiManager.UICharaRight;
                    break;
            }

            //  target は NovelUIChara 型のオブジェクト
            // target の中にあるRenderメソッドを呼び出している
            // 引数：
            //    assetName：表示するキャラ名（Resources/Chara/ からロード）
            //    () => { IsEnd = true; }：表示完了時のコールバック（終了フラグ）
            //    duration：フェード時間
            target.Render(assetName, () => {
                IsEnd = true;  // NovelManager 側で待機解除される
            }, duration);

            // 自身のコルーチンはここで終了（Render 内で完了を待つ）
            yield break;
        }
    }
}
