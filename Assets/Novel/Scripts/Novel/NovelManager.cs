using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Novel
{

    /// <summary>
    /// �m�x���X�N���v�g����́E�Đ�����Ǘ��N���X
    /// TextAsset�i�e�L�X�g�`���̃X�N���v�g�j��ǂݍ��݁A
    /// �e��R�}���h�iMessage, Chara �Ȃǁj�����Ɏ��s����
    /// </summary>
    public class NovelManager : MonoBehaviour
    {
        /// <summary>
        /// UI������Ǘ����� NovelUIManager �̎Q��
        /// �e�R�}���h��UI����i�e�L�X�g�\���Ȃǁj�Ɏg�p����
        /// </summary>
        [SerializeField]
        NovelUIManager uiManager;

        /// <summary>
        /// �X�N���v�g�̍Đ��������J�n����
        /// �X�N���v�g���s�P�ʂœǂݎ��A�R�}���h�I�u�W�F�N�g�𐶐����A�Đ��R���[�`�����J�n����
        /// </summary>
        /// <param name="script">TextAsset �^�̃m�x���X�N���v�g�iCSV��TXT�`���j</param>
        public void Play(TextAsset script)
        {
            Debug.Log(" script:" + script.text);

            // �X�N���v�g���s�P�ʂŕ���
            var lines = script.text.Split('\n');

            // CommandBase���p�������C���X�^���X�����郊�X�g
            var commands = new List<CommandBase>();

            // �e�s���R�}���h�ɕϊ�
            foreach (var line in lines)
            {
                // ��s�E�Z������s�i1�����ȉ��j�̓X�L�b�v
                if (line.Length <= 1) { continue; }

                // �s���J���}�ŕ���
                var data = line.Split(',');

                // �R�}���h�̎�ނ����O�ɏo��
                Debug.Log(" data[0]:" + data[0]);

                // �������ꂽ�R�}���h���ꎞ�I�ɓ���Ă����ϐ�
                CommandBase command = null;

                // �R�}���h�̎�ނɉ����ăC���X�^���X����
                switch (data[0].Trim())
                {
                    case "Message":
                        command = CommandBase.Create<Message>(data);
                        break;
                    case "Chara":
                        command = CommandBase.Create<Chara>(data);
                        break;
                    case "Image":
                        command = CommandBase.Create<Image>(data);
                        break;

                    default:
                        Debug.LogError("command is unknown :"+ data[0].Trim());
                        break;
                }
                if (command != null)
                {
                    commands.Add(command);
                }

            }

            // �R�}���h�̍Đ��������J�n
            StartCoroutine(PlayScript(commands));
        }

        /// <summary>
        /// �R�}���h���X�g�����ԂɎ��s����R���[�`��
        /// �e�R�}���h�� Start �� PlayAction �����Ɏ��s���AIsEnd �� true �ɂȂ�܂őҋ@����
        /// </summary>
        /// <param name="commands">�R�}���h���X�g</param>
        /// <returns>�R���[�`��</returns>
        public IEnumerator PlayScript(List<CommandBase> commands)
        {
            foreach (var command in commands)
            {
                // �O����
                yield return command.Start(uiManager);

                // ���C���A�N�V����
                yield return command.PlayAction(uiManager);

                // �I���t���O�����܂őҋ@
                while (!command.IsEnd)
                {
                    yield return null;
                }
            }


        }
    }
}