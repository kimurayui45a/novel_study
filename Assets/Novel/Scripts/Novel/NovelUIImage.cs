using System;
using System.Collections;
using UnityEngine;

namespace Novel
{

    /// <summary>
    /// �摜�̕\���E�؂�ւ����s��UI�N���X
    /// �摜���t�F�[�h�C���^�t�F�[�h�A�E�g���Ȃ��瓮�I�ɓ���ւ���
    /// </summary>
    public class NovelUIImage : MonoBehaviour
    {

        /// <summary>
        /// Resources �t�H���_���̉摜�v���n�u�̃��[�g�p�X
        /// </summary>
        static readonly string RootPath = "Image/";

        /// <summary>
        /// �\���Ώۂ̉摜��z�u����e�I�u�W�F�N�g�i���GameObject�j
        /// </summary>
        [SerializeField]
        GameObject parent;

        /// <summary>
        /// �O��\�����Ă����摜�i�t�F�[�h�A�E�g�Ώہj
        /// </summary>
        ImageAsset prevImage;

        /// <summary>
        /// ���ݕ\�����̉摜�i�t�F�[�h�C���Ώہj
        /// </summary>
        ImageAsset image;

        /// <summary>
        /// ���ݍĐ����̃R���[�`���i�Đ������ǂ����̔���Ɏg�p�j
        /// </summary>
        Coroutine coroutine;

        /// <summary>
        /// �摜�؂�ւ��������i�s�����ǂ�����Ԃ�
        /// </summary>
        public bool IsPlaying => coroutine != null;

        /// <summary>
        /// ���w�薼�œǂݍ��݁A�t�F�[�h�t���ŕ\����؂�ւ���B
        /// </summary>
        /// <param name="imageName">�\������摜�̖��O�iResources/Image/ ���̃v���n�u���j</param>
        /// <param name="onEnd">�\��������ɌĂяo���R�[���o�b�N</param>
        /// <param name="duration">�t�F�[�h���ԁi�b�j</param>
        public void Render(string imageName, Action onEnd, float duration = 1.0f)
        {
            // StartCoroutine�FMonoBehaviour ���񋟂��郁�\�b�h�A�R���[�`�����J�n����
            coroutine = StartCoroutine(PlayAction(imageName, onEnd, duration));
        }

        /// <summary>
        /// ���ۂ̕\���؂�ւ������i�t�F�[�h�C���E�t�F�[�h�A�E�g�j���s���R���[�`���B
        /// </summary>
        /// <param name="imageName">�摜��</param>
        /// <param name="onEnd">�����R�[���o�b�N</param>
        /// <param name="duration">�t�F�[�h����</param>
        /// <returns>�R���[�`��</returns>
        IEnumerator PlayAction(string imageName, Action onEnd, float duration)
        {
            // �w��摜�� Resources ���烍�[�h�iResources/Image/....�j
            var imageAsset = Resources.Load<ImageAsset>(RootPath + imageName);

            // ���݂̉摜�� prev �ɕۑ����A�V�����摜�𐶐�
            prevImage = image;
            image = Instantiate(imageAsset, parent.transform);

            // �t�F�[�h�A�E�g����
            var isActiveFadeOut = false;
            if (prevImage != null)
            {
                isActiveFadeOut = true;
                StartCoroutine(prevImage.FadeOut(() =>
                {
                    isActiveFadeOut = false;
                }));
            }

            // �t�F�[�h�C������
            var isActiveFadeIn = true;
            StartCoroutine(image.FadeIn(() =>
            {
                isActiveFadeIn = false;
            }));

            // �t�F�[�h�C���E�A�E�g����������܂őҋ@
            while (isActiveFadeIn || isActiveFadeOut)
            {
                yield return null;
            }

            // �O�̉摜���폜
            if (prevImage != null)
            {
                Destroy(prevImage.gameObject);
                prevImage = null;
            }

            // �����I��
            coroutine = null;

            // Invoke()�F�ϐ��Ƃ��Ď����Ă����֐��ionEnd�j�����s���邽�߂̃��\�b�h�AC#�̑g�ݍ��݃f���Q�[�g�^ Action�i����� Func�j�ɗp�ӂ��ꂽ�W�����\�b�h
            onEnd.Invoke();
        }
    }
}