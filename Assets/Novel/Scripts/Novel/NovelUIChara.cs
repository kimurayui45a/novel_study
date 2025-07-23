using System;
using System.Collections;
using UnityEngine;

namespace Novel
{

    /// <summary>
    /// キャラクター画像の表示・切り替えを行うUIクラス
    /// キャラクターをフェードイン／フェードアウトしながら動的に入れ替える
    /// </summary>
    public class NovelUIChara : MonoBehaviour
    {

        /// <summary>
        /// Resources フォルダ内のキャラプレハブのルートパス
        /// </summary>
        static readonly string RootPath = "Chara/";

        /// <summary>
        /// 表示対象のキャラクターを配置する親オブジェクト（空のGameObject）
        /// </summary>
        [SerializeField]
        GameObject parent;

        /// <summary>
        /// 前回表示していたキャラクター（フェードアウト対象）
        /// </summary>
        CharaAsset prevChara;

        /// <summary>
        /// 現在表示中のキャラクター（フェードイン対象）
        /// </summary>
        CharaAsset chara;

        /// <summary>
        /// 現在再生中のコルーチン（再生中かどうかの判定に使用）
        /// </summary>
        Coroutine coroutine;

        /// <summary>
        /// キャラ切り替え処理が進行中かどうかを返す
        /// </summary>
        public bool IsPlaying => coroutine != null;

        /// <summary>
        /// キャラクターを指定名で読み込み、フェード付きで表示を切り替える。
        /// </summary>
        /// <param name="charaName">表示するキャラクターの名前（Resources/Chara/ 内のプレハブ名）</param>
        /// <param name="onEnd">表示完了後に呼び出すコールバック</param>
        /// <param name="duration">フェード時間（秒）</param>
        public void Render(string charaName, Action onEnd, float duration = 1.0f)
        {
            // StartCoroutine：MonoBehaviour が提供するメソッド、コルーチンを開始する
            coroutine = StartCoroutine(PlayAction(charaName, onEnd, duration));
        }

        /// <summary>
        /// 実際の表示切り替え処理（フェードイン・フェードアウト）を行うコルーチン。
        /// </summary>
        /// <param name="charaName">キャラクター名</param>
        /// <param name="onEnd">完了コールバック</param>
        /// <param name="duration">フェード時間</param>
        /// <returns>コルーチン</returns>
        IEnumerator PlayAction(string charaName, Action onEnd, float duration)
        {
            // 指定キャラを Resources からロード（Resources/Chara/....）
            var charaAsset = Resources.Load<CharaAsset>(RootPath + charaName);

            // 現在のキャラを prev に保存し、新しいキャラを生成
            prevChara = chara;
            chara = Instantiate(charaAsset, parent.transform);

            // フェードアウト処理
            var isActiveFadeOut = false;
            if (prevChara != null)
            {
                isActiveFadeOut = true;
                StartCoroutine(prevChara.FadeOut( () =>
                {
                    isActiveFadeOut = false;
                }));
            }

            // フェードイン処理
            var isActiveFadeIn = true;
            StartCoroutine(chara.FadeIn( () =>
            {
                isActiveFadeIn = false;
            }));

            // フェードイン・アウトが完了するまで待機
            while (isActiveFadeIn || isActiveFadeOut)
            {
                yield return null;
            }

            // 前のキャラを削除
            if (prevChara != null)
            {
                Destroy(prevChara.gameObject);
                prevChara = null;
            }

            // 処理終了
            coroutine = null;

            // Invoke()：変数として持ってきた関数（onEnd）を実行するためのメソッド、C#の組み込みデリゲート型 Action（および Func）に用意された標準メソッド
            onEnd.Invoke();
        }
    }
}