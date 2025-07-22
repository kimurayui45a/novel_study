
using UnityEngine;

namespace Novel
{

    public class NovelUIManager : MonoBehaviour
    {
        [SerializeField]
        NovelUIMessage uiMessage;
        [SerializeField]
        NovelUIChara uiCharaLeft;
        [SerializeField]
        NovelUIChara uiCharaCenter;
        [SerializeField]
        NovelUIChara uiCharaRight;



        public NovelUIMessage UIMessage => uiMessage;
        public NovelUIChara UICharaLeft => uiCharaLeft;
        public NovelUIChara UICharaCenter => uiCharaCenter;
        public NovelUIChara UICharaRight => uiCharaRight;
    }
}