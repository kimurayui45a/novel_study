using System;
using System.Collections;
using UnityEngine;

namespace Novel
{

    /// <summary>
    /// �L�����N�^�[�摜�̕\���E�؂�ւ����s��UI�N���X
    /// �L�����N�^�[���t�F�[�h�C���^�t�F�[�h�A�E�g���Ȃ��瓮�I�ɓ���ւ���
    /// </summary>
    public class NovelUIChara : MonoBehaviour
    {

        /// <summary>
        /// Resources �t�H���_���̃L�����v���n�u�̃��[�g�p�X
        /// </summary>
        static readonly string RootPath = "Chara/";

        /// <summary>
        /// �\���Ώۂ̃L�����N�^�[��z�u����e�I�u�W�F�N�g�i���GameObject�j
        /// </summary>
        [SerializeField]
        GameObject parent;

        /// <summary>
        /// �O��\�����Ă����L�����N�^�[�i�t�F�[�h�A�E�g�Ώہj
        /// </summary>
        CharaAsset prevChara;

        /// <summary>
        /// ���ݕ\�����̃L�����N�^�[�i�t�F�[�h�C���Ώہj
        /// </summary>
        CharaAsset chara;

        /// <summary>
        /// ���ݍĐ����̃R���[�`���i�Đ������ǂ����̔���Ɏg�p�j
        /// </summary>
        Coroutine coroutine;

        /// <summary>
        /// �L�����؂�ւ��������i�s�����ǂ�����Ԃ�
        /// </summary>
        public bool IsPlaying => coroutine != null;

        /// <summary>
        /// �L�����N�^�[���w�薼�œǂݍ��݁A�t�F�[�h�t���ŕ\����؂�ւ���B
        /// </summary>
        /// <param name="charaName">�\������L�����N�^�[�̖��O�iResources/Chara/ ���̃v���n�u���j</param>
        /// <param name="onEnd">�\��������ɌĂяo���R�[���o�b�N</param>
        /// <param name="duration">�t�F�[�h���ԁi�b�j</param>
        public void Render(string charaName, Action onEnd, float duration = 1.0f)
        {
            // StartCoroutine�FMonoBehaviour ���񋟂��郁�\�b�h�A�R���[�`�����J�n����
            coroutine = StartCoroutine(PlayAction(charaName, onEnd, duration));
        }

        /// <summary>
        /// ���ۂ̕\���؂�ւ������i�t�F�[�h�C���E�t�F�[�h�A�E�g�j���s���R���[�`���B
        /// </summary>
        /// <param name="charaName">�L�����N�^�[��</param>
        /// <param name="onEnd">�����R�[���o�b�N</param>
        /// <param name="duration">�t�F�[�h����</param>
        /// <returns>�R���[�`��</returns>
        IEnumerator PlayAction(string charaName, Action onEnd, float duration)
        {
            // �w��L������ Resources ���烍�[�h�iResources/Chara/....�j
            var charaAsset = Resources.Load<CharaAsset>(RootPath + charaName);

            // ���݂̃L������ prev �ɕۑ����A�V�����L�����𐶐�
            prevChara = chara;
            chara = Instantiate(charaAsset, parent.transform);

            // �t�F�[�h�A�E�g����
            var isActiveFadeOut = false;
            if (prevChara != null)
            {
                isActiveFadeOut = true;
                StartCoroutine(prevChara.FadeOut( () =>
                {
                    isActiveFadeOut = false;
                }));
            }

            // �t�F�[�h�C������
            var isActiveFadeIn = true;
            StartCoroutine(chara.FadeIn( () =>
            {
                isActiveFadeIn = false;
            }));

            // �t�F�[�h�C���E�A�E�g����������܂őҋ@
            while (isActiveFadeIn || isActiveFadeOut)
            {
                yield return null;
            }

            // �O�̃L�������폜
            if (prevChara != null)
            {
                Destroy(prevChara.gameObject);
                prevChara = null;
            }

            // �����I��
            coroutine = null;

            // Invoke()�F�ϐ��Ƃ��Ď����Ă����֐��ionEnd�j�����s���邽�߂̃��\�b�h�AC#�̑g�ݍ��݃f���Q�[�g�^ Action�i����� Func�j�ɗp�ӂ��ꂽ�W�����\�b�h
            onEnd.Invoke();
        }
    }
}