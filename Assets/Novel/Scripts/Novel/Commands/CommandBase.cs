using System.Collections;
using UnityEngine;

namespace Novel
{

    /// <summary>
    /// �m�x���R�}���h�̊�ꒊ�ۃN���X
    /// �e�R�}���h�i��FMessage, Chara �Ȃǁj�͂�����p�����Ē�`����
    /// </summary>
    public abstract class CommandBase
    {

        /// <summary>
        /// �W�F�l���b�N�t�@�N�g���[���\�b�h
        /// �w�肳�ꂽ CommandBase �h���^�̃C���X�^���X�𐶐����A�f�[�^����͂���
        /// </summary>
        /// <typeparam name="T">CommandBase ���p�������^�i��FMessage, Chara�j</typeparam>
        /// <param name="data">�X�N���v�g1�s���J���}�ŕ�������������z��</param>
        /// <returns>��͂ɐ��������R�}���h�̃C���X�^���X�A���s���� null</returns>
        public static T Create<T>(string[] data) where T : CommandBase, new()
        {
            var ret = new T();

            // �f�[�^�̉�͂����s�����ꍇ�� null ��Ԃ�
            if (!ret.Analysis(data))
            {
                return null;
            }
            return ret;
        }

        /// <summary>
        /// �R�}���h�������������ǂ����������t���O
        /// PlayAction �̍Ō�� true �ɐݒ肳���
        /// </summary>
        public bool IsEnd { get; protected set; }

        /// <summary>
        /// �X�N���v�g1�s�̃f�[�^�i������z��j����͂��A������Ԃ�ݒ肷�钊�ۃ��\�b�h
        /// �h���N���X�ŕK���������K�v
        /// </summary>
        /// <param name="data">�X�N���v�g�̃J���}�����f�[�^</param>
        /// <returns>��͂������������ǂ����itrue/false�j</returns>
        public abstract bool Analysis(string[] data);

        /// <summary>
        /// �R�}���h�̎��s�O�����AStartAction �����s����
        /// �ʏ�� StartCoroutine() �ŌĂяo�����
        /// </summary>
        /// <param name="uiManager">UI �}�l�[�W���[�̎Q��</param>
        /// <returns>IEnumerator �R���[�`��</returns>
        public IEnumerator Start(NovelUIManager uiManager)
        {
            IsEnd = false;
            yield return StartAction(uiManager);
        }

        /// <summary>
        /// �R�}���h�̏����������i�t�F�[�h�Ȃǁj�A�K�v�ȏꍇ�ɃI�[�o�[���C�h����
        /// </summary>
        /// <param name="uiManager">UI �}�l�[�W���[�̎Q��</param>
        /// <returns>IEnumerator �R���[�`��</returns>
        protected virtual IEnumerator StartAction(NovelUIManager uiManager)
        {
            yield break;
        }

        /// <summary>
        /// �R�}���h�̃��C���������s�����ۃR���[�`��
        /// �e�h���N���X�ŕK�����������ׂ��{�̏����i���b�Z�[�W�\���A�L�����\���Ȃǁj
        /// </summary>
        /// <param name="uiManager">UI �}�l�[�W���[�̎Q��</param>
        /// <returns>IEnumerator �R���[�`��</returns>
        public abstract IEnumerator PlayAction(NovelUIManager uiManager);
    }
}
