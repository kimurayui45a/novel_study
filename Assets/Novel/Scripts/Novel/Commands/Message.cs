using System.Collections;
using UnityEngine;

namespace Novel
{

    public class Message: CommandBase
    {
        string displayName = string.Empty;
        string displayMessage= string.Empty;
        public override bool Analysis(string[] data)
        {

            if (data.Length != 3)
            {
                Debug.LogError($"[Message] command item : {data}");
                return false;
            }
            displayName = data[1].Trim().Replace("\"", "");
            displayMessage = data[2].Trim().Replace("\"", "");
            return true;
        }

        public override IEnumerator PlayAction(NovelUIManager uiManager)
        {
            uiManager.UIMessage.Play(displayName, displayMessage, () => {
                IsEnd = true;
            });
            yield break;
        }
    }
}
