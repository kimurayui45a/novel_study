
using UnityEngine;

namespace Novel
{

    /// <summary>
    /// ノベルゲームのUIを統括するマネージャークラス
    /// メッセージウィンドウやキャラクター表示UI（左右中央）へのアクセスを提供する
    /// </summary>
    public class NovelUIManager : MonoBehaviour
    {
        /// <summary>
        /// メッセージ表示用UI、キャラクターのセリフなどを表示するウィンドウ
        /// </summary>
        [SerializeField]
        NovelUIMessage uiMessage;

        /// <summary>
        /// 左側に表示されるキャラクターUI
        /// </summary>
        [SerializeField]
        NovelUIChara uiCharaLeft;

        /// <summary>
        /// 中央に表示されるキャラクターUI
        /// </summary>
        [SerializeField]
        NovelUIChara uiCharaCenter;

        /// <summary>
        /// 右側に表示されるキャラクターUI
        /// </summary>
        [SerializeField]
        NovelUIChara uiCharaRight;

        /// <summary>
        /// 表示される画像UI
        /// </summary>
        [SerializeField]
        NovelUIImage uiImage;

        // 各UIの参照を取得する
        public NovelUIMessage UIMessage => uiMessage;
        public NovelUIChara UICharaLeft => uiCharaLeft;
        public NovelUIChara UICharaCenter => uiCharaCenter;
        public NovelUIChara UICharaRight => uiCharaRight;
        public NovelUIImage UIImage => uiImage;
    }
}