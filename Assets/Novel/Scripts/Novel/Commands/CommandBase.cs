using System.Collections;
using UnityEngine;

namespace Novel
{

    public abstract class CommandBase
    {
        public static T Create<T>(string[] data) where T : CommandBase, new()
        {
            var ret = new T();
            if (!ret.Analysis(data))
            {
                return null;
            }
            return ret;
        }
        public bool IsEnd { get; protected set; }
        public abstract bool Analysis(string[] data);
        public IEnumerator Start(NovelUIManager uiManager)
        {
            IsEnd = false;
            yield return StartAction(uiManager);
        }
        protected virtual IEnumerator StartAction(NovelUIManager uiManager)
        {
            yield break;
        }
        public abstract IEnumerator PlayAction(NovelUIManager uiManager);
    }
}
