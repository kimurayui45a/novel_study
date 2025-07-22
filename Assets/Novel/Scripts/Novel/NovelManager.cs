using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Novel
{

    public class NovelManager : MonoBehaviour
    {
        [SerializeField]
        NovelUIManager uiManager;

        public void Play(TextAsset script)
        {
            Debug.Log(" script:" + script.text);
            var lines = script.text.Split('\n');
            var commands = new List<CommandBase>();
            foreach (var line in lines)
            {
                if (line.Length <= 1) { continue; }

                var data = line.Split(',');
                Debug.Log(" data[0]:" + data[0]);

                CommandBase command = null;
                switch (data[0].Trim())
                {
                    case "Message":
                        command = CommandBase.Create<Message>(data);
                        break;
                    case "Chara":
                        command = CommandBase.Create<Chara>(data);
                        break;

                    default:
                        Debug.LogError("command is unknown :"+ data[0].Trim());
                        break;
                }
                if (command != null)
                {
                    commands.Add(command);
                }

            }

            StartCoroutine(PlayScript(commands));
        }
        public IEnumerator PlayScript(List<CommandBase> commands)
        {
            foreach (var command in commands)
            {
                yield return command.Start(uiManager);
                yield return command.PlayAction(uiManager);

                while (!command.IsEnd)
                {
                    yield return null;
                }
            }


        }
    }
}