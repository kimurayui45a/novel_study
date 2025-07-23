using UnityEngine;

/// <summary>
/// ノベルゲームの起点となるクラス。
/// 指定されたスクリプト（TextAsset）を NovelManager に渡して再生を開始する
/// </summary>
public class NovelSample : MonoBehaviour
{
    /// <summary>
    /// 再生するノベルスクリプトファイル（.txt や .csv など）
    /// </summary>
    [SerializeField]
    TextAsset script;

    /// <summary>
    /// ノベルの進行管理を行う NovelManager インスタンス
    /// </summary>
    [SerializeField]
    Novel.NovelManager novelManager;

    /// <summary>
    /// スクリプトを渡してノベル再生を開始する
    /// </summary>
    void Start()
    {
        novelManager.Play(script);
    }
}
