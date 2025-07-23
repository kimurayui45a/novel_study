using UnityEngine;

/// <summary>
/// �m�x���Q�[���̋N�_�ƂȂ�N���X�B
/// �w�肳�ꂽ�X�N���v�g�iTextAsset�j�� NovelManager �ɓn���čĐ����J�n����
/// </summary>
public class NovelSample : MonoBehaviour
{
    /// <summary>
    /// �Đ�����m�x���X�N���v�g�t�@�C���i.txt �� .csv �Ȃǁj
    /// </summary>
    [SerializeField]
    TextAsset script;

    /// <summary>
    /// �m�x���̐i�s�Ǘ����s�� NovelManager �C���X�^���X
    /// </summary>
    [SerializeField]
    Novel.NovelManager novelManager;

    /// <summary>
    /// �X�N���v�g��n���ăm�x���Đ����J�n����
    /// </summary>
    void Start()
    {
        novelManager.Play(script);
    }
}
