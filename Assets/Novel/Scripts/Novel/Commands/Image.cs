using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Novel
{

    /// <summary>
    /// キャラクター表示コマンドを実装するクラス
    /// CommandBase を継承し、CSVからの解析とキャラの表示を制御する
    /// </summary>
    public class Image : CommandBase
    {

        /// <summary>
        /// 表示するキャラクタープレハブの名前（Resources/Chara/内にある）
        /// </summary>
        string assetName = string.Empty;

        /// <summary>
        /// 画像の表示位置
        /// </summary>
        float posX = 0f;
        float posY = 0f;

        /// <summary>
        /// フェード表示にかける時間（秒）
        /// </summary>
        float duration = 0.0f;

        /// <summary>
        /// コマンドのスクリプト行を解析して、画像情報を取り出す
        /// </summary>
        /// <param name="data">CSVで分割されたスクリプトの1行</param>
        /// <returns>解析が成功したか（true/false）</returns>
        public override bool Analysis(string[] data)
        {
            // フォーマットチェック： Chara,"キャラ名",位置,時間
            if (data.Length != 5)
            {
                Debug.LogError($"[Chara] command item : {data}");
                return false;
            }

            // キャラ名（" を除去）
            assetName = data[1].Trim().Replace("\"", "");

            // 座標X
            if (!float.TryParse(data[2].Trim(), out posX))
            {
                Debug.LogError($"[Image] invalid posX : {data[2]}");
                return false;
            }

            // 座標Y
            if (!float.TryParse(data[3].Trim(), out posY))
            {
                Debug.LogError($"[Image] invalid posY : {data[3]}");
                return false;
            }

            // 時間（文字列 → float）に変換
            if (!float.TryParse(data[4].Trim(), out duration))
            {
                Debug.LogError($"[Chara] command  data[3] : {data[3]}");
            }
            return true;
        }

        /// <summary>
        /// 画像表示の実行処理
        /// UIの指定位置にキャラをフェードイン表示させる
        /// </summary>
        /// <param name="uiManager">UI全体を管理する NovelUIManager</param>
        /// <returns>コルーチン</returns>
        public override IEnumerator PlayAction(NovelUIManager uiManager)
        {
            // NovelUIImage を取得
            NovelUIImage imageUI = uiManager.UIImage;

            // 表示を開始（完了時に IsEnd = true を設定）
            imageUI.Render(assetName, () => {
                IsEnd = true;
            }, duration);

            // 表示された画像の位置を posX, posY に設定
            if (imageUI.transform.childCount > 0)
            {
                var lastImage = imageUI.transform.GetChild(imageUI.transform.childCount - 1);
                var rect = lastImage.GetComponent<RectTransform>();
                if (rect != null)
                {
                    rect.anchoredPosition = new Vector2(posX, posY);
                }
                else
                {
                    lastImage.transform.localPosition = new Vector3(posX, posY, 0);
                }
            }

            // 自身のコルーチンはここで終了（Render 内で完了を待つ）
            yield break;

        }

    }
}
