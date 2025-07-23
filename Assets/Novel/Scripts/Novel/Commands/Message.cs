using System.Collections;
using UnityEngine;

namespace Novel
{

    /// <summary>
    /// ���b�Z�[�W�\���R�}���h
    /// �L�����N�^�[�̖��O�ƃZ���t����͂��A���b�Z�[�WUI�ɕ\��������
    /// </summary>
    public class Message: CommandBase
    {

        /// <summary>
        /// �\������L�����N�^�[�̖��O
        /// </summary>
        string displayName = string.Empty;

        /// <summary>
        /// �\�����郁�b�Z�[�W���e�i�Z���t�j
        /// </summary>
        string displayMessage = string.Empty;

        /// <summary>
        /// �X�N���v�g1�s����͂��āA���O�ƃZ���t�����o��
        /// </summary>
        /// <param name="data">�J���}��؂�̃R�}���h�f�[�^�i��: Message,"���O","���b�Z�[�W"�j</param>
        /// <returns>��͂ɐ��������ꍇ�� true�A���s�����ꍇ�� false</returns>
        public override bool Analysis(string[] data)
        {

            // �t�H�[�}�b�g�`�F�b�N�FMessage,���O,���b�Z�[�W
            if (data.Length != 3)
            {
                Debug.LogError($"[Message] command item : {data}");
                return false;
            }

            // �_�u���N�H�[�g���������A���O�ƃZ���t���擾
            displayName = data[1].Trim().Replace("\"", "");
            displayMessage = data[2].Trim().Replace("\"", "");

            return true;
        }

        /// <summary>
        /// ���b�Z�[�WUI�ɑ΂��āA�w�肳�ꂽ���O�ƃZ���t��\��������
        /// �t�F�[�h�╶������Ȃǂ̉��o���܂�
        /// </summary>
        /// <param name="uiManager">UI�𓝊����� NovelUIManager �̎Q��</param>
        /// <returns>IEnumerator �R���[�`���i���o�{�̂� UI ���ɈϏ��j</returns>
        public override IEnumerator PlayAction(NovelUIManager uiManager)
        {

            // UIMessage.Play �ɂ�胁�b�Z�[�W�\���J�n
            // �I�����̃R�[���o�b�N�� IsEnd �t���O�� true �ɂ���
            uiManager.UIMessage.Play(displayName, displayMessage, () => {
                IsEnd = true;
            });

            // �������g�̏����͑��I���iUI ���Ŋ�����҂�����j
            yield break;
        }
    }
}
