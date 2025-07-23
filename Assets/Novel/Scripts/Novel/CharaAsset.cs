using System;
using System.Collections;
using UnityEngine;

namespace Novel
{

    /// <summary>
    /// �L�����N�^�[�̕\���^��\���A�j���[�V�����i�t�F�[�h�j��S������R���|�[�l���g
    /// CanvasGroup ���g���ē����x�ialpha�j�𐧌䂷��
    /// </summary>
    public class CharaAsset : MonoBehaviour
    {
        /// <summary>
        /// �t�F�[�h���o�p�� CanvasGroup�Aalpha �l���g���ĕ\���̔Z���𒲐�����
        /// </summary>
        [SerializeField]
        CanvasGroup canvasGroup;

        /// <summary>
        /// �L�����N�^�[���t�F�[�h�C���i���X�ɕ\���j������R���[�`��
        /// </summary>
        /// <param name="onEnd">�t�F�[�h�������ɌĂ΂��R�[���o�b�N</param>
        /// <param name="duration">�t�F�[�h�ɂ����鎞�ԁi�b�j</param>
        /// <returns>IEnumerator �R���[�`��</returns>
        public IEnumerator FadeIn(Action onEnd, float duration = 0.5f)
        {
            float time = 0;

            // ������ԁF����
            canvasGroup.alpha = 0;

            // �w�莞�Ԃ��o�߂���܂ŁAalpha �����X�ɑ��₷
            while (time < duration)
            {
                // �o�ߎ��Ԃ����Z
                time += Time.deltaTime;

                // �ő�1.0�܂�
                canvasGroup.alpha = Mathf.Min(1.0f, time / duration);

                // 1�t���[���ҋ@
                yield return null;
            }

            // �ŏI�I�Ɋ��S�ɕ\���i�����x1.0�j
            canvasGroup.alpha = 1.0f;

            // �I�����ɃR�[���o�b�N���s
            onEnd();
        }

        /// <summary>
        /// �L�����N�^�[���t�F�[�h�A�E�g�i���X�ɔ�\���j������R���[�`��
        /// </summary>
        /// <param name="onEnd">�t�F�[�h�������ɌĂ΂��R�[���o�b�N</param>
        /// <param name="duration">�t�F�[�h�ɂ����鎞�ԁi�b�j</param>
        /// <returns>IEnumerator �R���[�`��</returns>
        public IEnumerator FadeOut(Action onEnd, float duration = 0.5f)
        {
            float time = 0;

            // ������ԁF���S�\��
            canvasGroup.alpha = 1.0f;

            // �w�莞�Ԃ��o�߂���܂ŁAalpha �����X�Ɍ��炷�i�ŏ�0.0�܂Łj
            while (time < duration)
            {
                time += Time.deltaTime;
                canvasGroup.alpha = Mathf.Max(0.0f, 1.0f - time / duration);
                yield return null;
            }

            // �ŏI�I�Ɋ��S�ɔ�\���i�����x0.0�j
            canvasGroup.alpha = 0.0f;

            // �I�����ɃR�[���o�b�N���s
            onEnd();
        }

    }
}