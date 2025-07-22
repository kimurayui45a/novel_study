using UnityEngine;

public class NovelSample : MonoBehaviour
{
    [SerializeField]
    TextAsset script;
    [SerializeField]
    Novel.NovelManager novelManager;

    void Start()
    {
        novelManager.Play(script);
    }
}
