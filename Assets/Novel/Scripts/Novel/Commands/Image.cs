using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Novel
{

    /// <summary>
    /// �L�����N�^�[�\���R�}���h����������N���X
    /// CommandBase ���p�����ACSV����̉�͂ƃL�����̕\���𐧌䂷��
    /// </summary>
    public class Image : CommandBase
    {

        /// <summary>
        /// �\������L�����N�^�[�v���n�u�̖��O�iResources/Chara/���ɂ���j
        /// </summary>
        string assetName = string.Empty;

        /// <summary>
        /// �摜�̕\���ʒu
        /// </summary>
        float posX = 0f;
        float posY = 0f;

        /// <summary>
        /// �t�F�[�h�\���ɂ����鎞�ԁi�b�j
        /// </summary>
        float duration = 0.0f;

        /// <summary>
        /// �R�}���h�̃X�N���v�g�s����͂��āA�摜�������o��
        /// </summary>
        /// <param name="data">CSV�ŕ������ꂽ�X�N���v�g��1�s</param>
        /// <returns>��͂������������itrue/false�j</returns>
        public override bool Analysis(string[] data)
        {
            // �t�H�[�}�b�g�`�F�b�N�F Chara,"�L������",�ʒu,����
            if (data.Length != 5)
            {
                Debug.LogError($"[Chara] command item : {data}");
                return false;
            }

            // �L�������i" �������j
            assetName = data[1].Trim().Replace("\"", "");

            // ���WX
            if (!float.TryParse(data[2].Trim(), out posX))
            {
                Debug.LogError($"[Image] invalid posX : {data[2]}");
                return false;
            }

            // ���WY
            if (!float.TryParse(data[3].Trim(), out posY))
            {
                Debug.LogError($"[Image] invalid posY : {data[3]}");
                return false;
            }

            // ���ԁi������ �� float�j�ɕϊ�
            if (!float.TryParse(data[4].Trim(), out duration))
            {
                Debug.LogError($"[Chara] command  data[3] : {data[3]}");
            }
            return true;
        }

        /// <summary>
        /// �摜�\���̎��s����
        /// UI�̎w��ʒu�ɃL�������t�F�[�h�C���\��������
        /// </summary>
        /// <param name="uiManager">UI�S�̂��Ǘ����� NovelUIManager</param>
        /// <returns>�R���[�`��</returns>
        public override IEnumerator PlayAction(NovelUIManager uiManager)
        {
            // NovelUIImage ���擾
            NovelUIImage imageUI = uiManager.UIImage;

            // �\�����J�n�i�������� IsEnd = true ��ݒ�j
            imageUI.Render(assetName, () => {
                IsEnd = true;
            }, duration);

            // �\�����ꂽ�摜�̈ʒu�� posX, posY �ɐݒ�
            if (imageUI.transform.childCount > 0)
            {
                var lastImage = imageUI.transform.GetChild(imageUI.transform.childCount - 1);
                var rect = lastImage.GetComponent<RectTransform>();
                if (rect != null)
                {
                    rect.anchoredPosition = new Vector2(posX, posY);
                }
                else
                {
                    lastImage.transform.localPosition = new Vector3(posX, posY, 0);
                }
            }

            // ���g�̃R���[�`���͂����ŏI���iRender ���Ŋ�����҂j
            yield break;

        }

    }
}
