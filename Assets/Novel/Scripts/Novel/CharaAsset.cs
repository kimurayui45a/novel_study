using System;
using System.Collections;
using UnityEngine;

namespace Novel
{

    /// <summary>
    /// キャラクターの表示／非表示アニメーション（フェード）を担当するコンポーネント
    /// CanvasGroup を使って透明度（alpha）を制御する
    /// </summary>
    public class CharaAsset : MonoBehaviour
    {
        /// <summary>
        /// フェード演出用の CanvasGroup、alpha 値を使って表示の濃さを調整する
        /// </summary>
        [SerializeField]
        CanvasGroup canvasGroup;

        /// <summary>
        /// キャラクターをフェードイン（徐々に表示）させるコルーチン
        /// </summary>
        /// <param name="onEnd">フェード完了時に呼ばれるコールバック</param>
        /// <param name="duration">フェードにかける時間（秒）</param>
        /// <returns>IEnumerator コルーチン</returns>
        public IEnumerator FadeIn(Action onEnd, float duration = 0.5f)
        {
            float time = 0;

            // 初期状態：透明
            canvasGroup.alpha = 0;

            // 指定時間が経過するまで、alpha を徐々に増やす
            while (time < duration)
            {
                // 経過時間を加算
                time += Time.deltaTime;

                // 最大1.0まで
                canvasGroup.alpha = Mathf.Min(1.0f, time / duration);

                // 1フレーム待機
                yield return null;
            }

            // 最終的に完全に表示（透明度1.0）
            canvasGroup.alpha = 1.0f;

            // 終了時にコールバック実行
            onEnd();
        }

        /// <summary>
        /// キャラクターをフェードアウト（徐々に非表示）させるコルーチン
        /// </summary>
        /// <param name="onEnd">フェード完了時に呼ばれるコールバック</param>
        /// <param name="duration">フェードにかける時間（秒）</param>
        /// <returns>IEnumerator コルーチン</returns>
        public IEnumerator FadeOut(Action onEnd, float duration = 0.5f)
        {
            float time = 0;

            // 初期状態：完全表示
            canvasGroup.alpha = 1.0f;

            // 指定時間が経過するまで、alpha を徐々に減らす（最小0.0まで）
            while (time < duration)
            {
                time += Time.deltaTime;
                canvasGroup.alpha = Mathf.Max(0.0f, 1.0f - time / duration);
                yield return null;
            }

            // 最終的に完全に非表示（透明度0.0）
            canvasGroup.alpha = 0.0f;

            // 終了時にコールバック実行
            onEnd();
        }

    }
}