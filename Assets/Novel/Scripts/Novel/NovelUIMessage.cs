using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Novel
{

    /// <summary>
    /// �m�x���Q�[���ɂ����郁�b�Z�[�W�\��UI�𐧌䂷��N���X
    /// ���O�ƃ��b�Z�[�W�̕\���A�{�^���ł̑�����⎟�ւ̐i�s���Ǘ�����
    /// </summary>
    public class NovelUIMessage : MonoBehaviour
    {
        /// <summary>
        /// �\������L�����N�^�[���̃e�L�X�g
        /// </summary>
        [SerializeField]
        TextMeshProUGUI textName;

        /// <summary>
        /// �\�����郁�b�Z�[�W�{���̃e�L�X�g
        /// </summary>
        [SerializeField]
        TextMeshProUGUI textMessage;

        /// <summary>
        /// ���b�Z�[�W�������u���ցv���s���{�^��
        /// </summary>
        [SerializeField]
        Button buttonNext;

        /// <summary>
        /// ���S�ȃ��b�Z�[�W���e��ێ�����i�������Ɉꊇ�\�����邽�߁j
        /// </summary>
        string displayMessage;

        /// <summary>
        /// �\���O�ɕۑ����Ă������b�Z�[�W�̐F�i�t�F�[�h�ȂǂɑΉ����邽�߁j
        /// </summary>
        Color messageColor;

        /// <summary>
        /// ���b�Z�[�W�\���̃R���[�`���Q�Ɓi�Đ����t���O���~�Ɏg�p�j
        /// </summary>
        Coroutine coroutine;

        /// <summary>
        /// �\��������ɌĂяo���R�[���o�b�N
        /// </summary>
        Action handleEnd;

        /// <summary>
        /// ���݃��b�Z�[�W�\�������ǂ����i�R���[�`���������Ă��邩�j
        /// </summary>
        public bool IsPlaying => coroutine != null;

        /// <summary>
        /// �����������A�{�^���̃N���b�N���̋�����ݒ肷��
        /// </summary>
        private void Awake()
        {
            buttonNext.onClick.AddListener(() =>
            {
                if (IsPlaying)
                {
                    // �\�����Ȃ狭���I�ɑS�\��
                    Stop();

                } else
                {
                    // �\��������Ȃ玟�̏�����
                    handleEnd?.Invoke();

                }
            });
        }

        /// <summary>
        /// UI��Ԃ̃��Z�b�g�����A�K�v�ɉ����ă��b�Z�[�W�̏��������s��
        /// </summary>
        /// <param name="isCleanMessage">���O�ƃ��b�Z�[�W����ɂ��邩�ǂ���</param>
        public void Reset(bool isCleanMessage = false)
        {
            Stop();
            if (isCleanMessage)
            {
                // �eUI�ɋ���Z�b�g����
                textName.text = string.Empty;
                textMessage.text = string.Empty;
            }
            handleEnd = null;
        }


        /// <summary>
        /// ���b�Z�[�W���w��̕�������X�s�[�h�ŕ\���J�n����
        /// </summary>
        /// <param name="name">�L�����N�^�[��</param>
        /// <param name="message">�\�����郁�b�Z�[�W</param>
        /// <param name="onEnd">�\���������̃R�[���o�b�N</param>
        /// <param name="duration">1����������̕\���Ԋu�i�b�j</param>
        public void Play(string name, string message, Action onEnd, float duration = 0.1f)
        {
            Stop();
            displayMessage = message;
            messageColor = textMessage.color;
            handleEnd = onEnd;
            coroutine = StartCoroutine(PlayAction(name, message, duration));

        }

        /// <summary>
        /// �\�����̃��b�Z�[�W�𑦍��ɑS�\���ɐ؂�ւ���
        /// �R���[�`�����~���A���b�Z�[�W����C�ɏo��
        /// </summary>
        public void Stop()
        {
            if (coroutine == null) return;

            // StopCoroutine�FUnity�̃��\�b�h�A���s���̃R���[�`��������r���Ŏ~�߂�
            StopCoroutine(coroutine);

            coroutine = null;
            textMessage.text = displayMessage;
            textMessage.color = messageColor;
            displayMessage = string.Empty;

        }


        /// <summary>
        /// ���b�Z�[�W��1�������\�����鉉�o�R���[�`��
        /// �������s�̌v�Z��s���֑������ɂ��Ή�
        /// </summary>
        /// <param name="name">�L�����N�^�[��</param>
        /// <param name="message">���b�Z�[�W�{��</param>
        /// <param name="duration">1����������̕\���Ԋu</param>
        /// <returns>IEnumerator�i�R���[�`���j</returns>
        IEnumerator PlayAction(string name, string message, float duration)
        {
            Debug.Log(" Message PlayAction:" + message);
            textName.text = name;

            // TextMeshPro�ɂ��s���s���֑��̎����Ή��i�Q�l�Fhttps://qiita.com/yuki-moroto/items/4f7f0a34f4941c699675�j
            textMessage.text = message;
            var tempColor = textMessage.color;
            tempColor.a = 0;
            textMessage.color = tempColor;

            // �������s�̌v�Z�̂���1�t���[���҂�
            yield return null;

            // ���s�R�[�h��}������
            // �������s�̏����F�e�s�� visible character ���E���A�����I�� \n ��ǉ�
            var lineInfo = textMessage.textInfo.lineInfo;
            string text = string.Empty;
            foreach (var info in lineInfo)
            {
                if (info.visibleCharacterCount > 0)
                {
                    text += textMessage.text.Substring(info.firstVisibleCharacterIndex, info.visibleCharacterCount) + "\n";
                }
            }

            displayMessage = text;
            tempColor.a = 1;
            textMessage.color = tempColor;

            // ������1���ǉ��\���i�������艉�o�j
            string tempMessage = string.Empty;
            foreach (var c in text)
            {
                tempMessage += c;
                textMessage.text = tempMessage;

                // WaitForSeconds�F�w�肵���b�������҂AUnity�̃��\�b�h
                yield return new WaitForSeconds(duration);
            }

            coroutine = null;
        }
    }
}