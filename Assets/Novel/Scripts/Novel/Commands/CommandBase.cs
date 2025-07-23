using System.Collections;
using UnityEngine;

namespace Novel
{

    /// <summary>
    /// ノベルコマンドの基底抽象クラス
    /// 各コマンド（例：Message, Chara など）はこれを継承して定義する
    /// </summary>
    public abstract class CommandBase
    {

        /// <summary>
        /// ジェネリックファクトリーメソッド
        /// 指定された CommandBase 派生型のインスタンスを生成し、データを解析する
        /// </summary>
        /// <typeparam name="T">CommandBase を継承した型（例：Message, Chara）</typeparam>
        /// <param name="data">スクリプト1行をカンマで分割した文字列配列</param>
        /// <returns>解析に成功したコマンドのインスタンス、失敗時は null</returns>
        public static T Create<T>(string[] data) where T : CommandBase, new()
        {
            var ret = new T();

            // データの解析が失敗した場合は null を返す
            if (!ret.Analysis(data))
            {
                return null;
            }
            return ret;
        }

        /// <summary>
        /// コマンドが完了したかどうかを示すフラグ
        /// PlayAction の最後で true に設定される
        /// </summary>
        public bool IsEnd { get; protected set; }

        /// <summary>
        /// スクリプト1行のデータ（文字列配列）を解析し、内部状態を設定する抽象メソッド
        /// 派生クラスで必ず実装が必要
        /// </summary>
        /// <param name="data">スクリプトのカンマ分割データ</param>
        /// <returns>解析が成功したかどうか（true/false）</returns>
        public abstract bool Analysis(string[] data);

        /// <summary>
        /// コマンドの実行前処理、StartAction を実行する
        /// 通常は StartCoroutine() で呼び出される
        /// </summary>
        /// <param name="uiManager">UI マネージャーの参照</param>
        /// <returns>IEnumerator コルーチン</returns>
        public IEnumerator Start(NovelUIManager uiManager)
        {
            IsEnd = false;
            yield return StartAction(uiManager);
        }

        /// <summary>
        /// コマンドの初期化処理（フェードなど）、必要な場合にオーバーライドする
        /// </summary>
        /// <param name="uiManager">UI マネージャーの参照</param>
        /// <returns>IEnumerator コルーチン</returns>
        protected virtual IEnumerator StartAction(NovelUIManager uiManager)
        {
            yield break;
        }

        /// <summary>
        /// コマンドのメイン処理を行う抽象コルーチン
        /// 各派生クラスで必ず実装されるべき本体処理（メッセージ表示、キャラ表示など）
        /// </summary>
        /// <param name="uiManager">UI マネージャーの参照</param>
        /// <returns>IEnumerator コルーチン</returns>
        public abstract IEnumerator PlayAction(NovelUIManager uiManager);
    }
}
