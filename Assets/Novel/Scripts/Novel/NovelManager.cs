using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Novel
{

    /// <summary>
    /// ノベルスクリプトを解析・再生する管理クラス
    /// TextAsset（テキスト形式のスクリプト）を読み込み、
    /// 各種コマンド（Message, Chara など）を順に実行する
    /// </summary>
    public class NovelManager : MonoBehaviour
    {
        /// <summary>
        /// UI操作を管理する NovelUIManager の参照
        /// 各コマンドのUI操作（テキスト表示など）に使用する
        /// </summary>
        [SerializeField]
        NovelUIManager uiManager;

        /// <summary>
        /// スクリプトの再生処理を開始する
        /// スクリプトを行単位で読み取り、コマンドオブジェクトを生成し、再生コルーチンを開始する
        /// </summary>
        /// <param name="script">TextAsset 型のノベルスクリプト（CSVやTXT形式）</param>
        public void Play(TextAsset script)
        {
            Debug.Log(" script:" + script.text);

            // スクリプトを行単位で分割
            var lines = script.text.Split('\n');

            // CommandBaseを継承したインスタンスを入れるリスト
            var commands = new List<CommandBase>();

            // 各行をコマンドに変換
            foreach (var line in lines)
            {
                // 空行・短すぎる行（1文字以下）はスキップ
                if (line.Length <= 1) { continue; }

                // 行をカンマで分割
                var data = line.Split(',');

                // コマンドの種類をログに出力
                Debug.Log(" data[0]:" + data[0]);

                // 生成されたコマンドを一時的に入れておく変数
                CommandBase command = null;

                // コマンドの種類に応じてインスタンス生成
                switch (data[0].Trim())
                {
                    case "Message":
                        command = CommandBase.Create<Message>(data);
                        break;
                    case "Chara":
                        command = CommandBase.Create<Chara>(data);
                        break;
                    case "Image":
                        command = CommandBase.Create<Image>(data);
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

            // コマンドの再生処理を開始
            StartCoroutine(PlayScript(commands));
        }

        /// <summary>
        /// コマンドリストを順番に実行するコルーチン
        /// 各コマンドの Start → PlayAction を順に実行し、IsEnd が true になるまで待機する
        /// </summary>
        /// <param name="commands">コマンドリスト</param>
        /// <returns>コルーチン</returns>
        public IEnumerator PlayScript(List<CommandBase> commands)
        {
            foreach (var command in commands)
            {
                // 前処理
                yield return command.Start(uiManager);

                // メインアクション
                yield return command.PlayAction(uiManager);

                // 終了フラグが立つまで待機
                while (!command.IsEnd)
                {
                    yield return null;
                }
            }


        }
    }
}