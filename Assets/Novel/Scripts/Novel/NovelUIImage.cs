using System;
using System.Collections;
using UnityEngine;

namespace Novel
{

    /// <summary>
    /// 画像の表示・切り替えを行うUIクラス
    /// 画像をフェードイン／フェードアウトしながら動的に入れ替える
    /// </summary>
    public class NovelUIImage : MonoBehaviour
    {

        /// <summary>
        /// Resources フォルダ内の画像プレハブのルートパス
        /// </summary>
        static readonly string RootPath = "Image/";

        /// <summary>
        /// 表示対象の画像を配置する親オブジェクト（空のGameObject）
        /// </summary>
        [SerializeField]
        GameObject parent;

        /// <summary>
        /// 前回表示していた画像（フェードアウト対象）
        /// </summary>
        ImageAsset prevImage;

        /// <summary>
        /// 現在表示中の画像（フェードイン対象）
        /// </summary>
        ImageAsset image;

        /// <summary>
        /// 現在再生中のコルーチン（再生中かどうかの判定に使用）
        /// </summary>
        Coroutine coroutine;

        /// <summary>
        /// 画像切り替え処理が進行中かどうかを返す
        /// </summary>
        public bool IsPlaying => coroutine != null;

        /// <summary>
        /// を指定名で読み込み、フェード付きで表示を切り替える。
        /// </summary>
        /// <param name="imageName">表示する画像の名前（Resources/Image/ 内のプレハブ名）</param>
        /// <param name="onEnd">表示完了後に呼び出すコールバック</param>
        /// <param name="duration">フェード時間（秒）</param>
        public void Render(string imageName, Action onEnd, float duration = 1.0f)
        {
            // StartCoroutine：MonoBehaviour が提供するメソッド、コルーチンを開始する
            coroutine = StartCoroutine(PlayAction(imageName, onEnd, duration));
        }

        /// <summary>
        /// 実際の表示切り替え処理（フェードイン・フェードアウト）を行うコルーチン。
        /// </summary>
        /// <param name="imageName">画像名</param>
        /// <param name="onEnd">完了コールバック</param>
        /// <param name="duration">フェード時間</param>
        /// <returns>コルーチン</returns>
        IEnumerator PlayAction(string imageName, Action onEnd, float duration)
        {
            // 指定画像を Resources からロード（Resources/Image/....）
            var imageAsset = Resources.Load<ImageAsset>(RootPath + imageName);

            // 現在の画像を prev に保存し、新しい画像を生成
            prevImage = image;
            image = Instantiate(imageAsset, parent.transform);

            // フェードアウト処理
            var isActiveFadeOut = false;
            if (prevImage != null)
            {
                isActiveFadeOut = true;
                StartCoroutine(prevImage.FadeOut(() =>
                {
                    isActiveFadeOut = false;
                }));
            }

            // フェードイン処理
            var isActiveFadeIn = true;
            StartCoroutine(image.FadeIn(() =>
            {
                isActiveFadeIn = false;
            }));

            // フェードイン・アウトが完了するまで待機
            while (isActiveFadeIn || isActiveFadeOut)
            {
                yield return null;
            }

            // 前の画像を削除
            if (prevImage != null)
            {
                Destroy(prevImage.gameObject);
                prevImage = null;
            }

            // 処理終了
            coroutine = null;

            // Invoke()：変数として持ってきた関数（onEnd）を実行するためのメソッド、C#の組み込みデリゲート型 Action（および Func）に用意された標準メソッド
            onEnd.Invoke();
        }
    }
}