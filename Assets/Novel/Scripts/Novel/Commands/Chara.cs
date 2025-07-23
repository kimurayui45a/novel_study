using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Novel
{

    /// <summary>
    /// �L�����N�^�[�\���R�}���h����������N���X
    /// CommandBase ���p�����ACSV����̉�͂ƃL�����̕\���𐧌䂷��
    /// </summary>
    public class Chara : CommandBase
    {
        /// <summary>
        /// �L�����N�^�[�\���ʒu�i���E�����E�E�j���`����񋓌^
        /// </summary>
        enum Position
        {
            Left,
            Right,
            Center,
        }

        /// <summary>
        /// �\������L�����N�^�[�v���n�u�̖��O�iResources/Chara/���ɂ���j
        /// </summary>
        string assetName = string.Empty;

        /// <summary>
        /// �L�����N�^�[�̕\���ʒu�i�񋓌^�j
        /// </summary>
        Position position = Position.Center;

        /// <summary>
        /// �t�F�[�h�\���ɂ����鎞�ԁi�b�j
        /// </summary>
        float duration = 0.0f;


        /// <summary>
        /// �R�}���h�̃X�N���v�g�s����͂��āA�L�����N�^�[�������o��
        /// </summary>
        /// <param name="data">CSV�ŕ������ꂽ�X�N���v�g��1�s</param>
        /// <returns>��͂������������itrue/false�j</returns>
        public override bool Analysis(string[] data)
        {
            // �t�H�[�}�b�g�`�F�b�N�F Chara,"�L������",�ʒu,����
            if (data.Length != 4)
            {
                Debug.LogError($"[Chara] command item : {data}");
                return false;
            }

            // �L�������i" �������j
            assetName = data[1].Trim().Replace("\"", "");

            // �ʒu�𕶎���Ŕ��肵�A�񋓌^�ɕϊ�
            var dir = data[2].Trim();
            switch (dir)
            {
                case "Left":
                    position = Position.Left; break;
                case "Center":
                    position = Position.Center; break;
                case "Right":
                    position = Position.Right; break;
                default:
                    Debug.LogError($"[Chara] command  data[2] : {data[2]}");
                    return false;
            }

            // ���ԁi������ �� float�j�ɕϊ�
            if (!float.TryParse(data[3].Trim(), out duration))
            {
                Debug.LogError($"[Chara] command  data[3] : {data[3]}");
            }
            return true;
        }

        /// <summary>
        /// �L�����N�^�[�\���̎��s����
        /// UI�̎w��ʒu�ɃL�������t�F�[�h�C���\��������
        /// </summary>
        /// <param name="uiManager">UI�S�̂��Ǘ����� NovelUIManager</param>
        /// <returns>�R���[�`��</returns>
        public override IEnumerator PlayAction(NovelUIManager uiManager)
        {
            NovelUIChara target = null;

            // �\���ʒu�ɉ����ĕ\����� UIChara ���擾
            switch (position)
            {
                case Position.Left:
                    target = uiManager.UICharaLeft;
                    break;
                case Position.Center:
                    target = uiManager.UICharaCenter;
                    break;
                case Position.Right:
                    target = uiManager.UICharaRight;
                    break;
            }

            //  target �� NovelUIChara �^�̃I�u�W�F�N�g
            // target �̒��ɂ���Render���\�b�h���Ăяo���Ă���
            // �����F
            //    assetName�F�\������L�������iResources/Chara/ ���烍�[�h�j
            //    () => { IsEnd = true; }�F�\���������̃R�[���o�b�N�i�I���t���O�j
            //    duration�F�t�F�[�h����
            target.Render(assetName, () => {
                IsEnd = true;  // NovelManager ���őҋ@���������
            }, duration);

            // ���g�̃R���[�`���͂����ŏI���iRender ���Ŋ�����҂j
            yield break;
        }
    }
}
