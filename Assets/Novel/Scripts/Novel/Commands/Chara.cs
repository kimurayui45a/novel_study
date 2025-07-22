using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Novel
{

    public class Chara : CommandBase
    {
        enum Position
        {
            Left,
            Right,
            Center,
        }

        string assetName = string.Empty;
        Position position = Position.Center;
        float duration = 0.0f;
        public override bool Analysis(string[] data)
        {

            if (data.Length != 4)
            {
                Debug.LogError($"[Chara] command item : {data}");
                return false;
            }
            assetName = data[1].Trim().Replace("\"", "");
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
            if (!float.TryParse(data[3].Trim(), out duration))
            {
                Debug.LogError($"[Chara] command  data[3] : {data[3]}");
            }
            return true;
        }

        public override IEnumerator PlayAction(NovelUIManager uiManager)
        {
            NovelUIChara target = null;
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

            target.Render(assetName, () => {
                IsEnd = true;
            }, duration);
            yield break;
        }
    }
}
