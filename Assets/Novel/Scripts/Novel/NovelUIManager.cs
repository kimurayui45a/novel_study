
using UnityEngine;

namespace Novel
{

    /// <summary>
    /// �m�x���Q�[����UI�𓝊�����}�l�[�W���[�N���X
    /// ���b�Z�[�W�E�B���h�E��L�����N�^�[�\��UI�i���E�����j�ւ̃A�N�Z�X��񋟂���
    /// </summary>
    public class NovelUIManager : MonoBehaviour
    {
        /// <summary>
        /// ���b�Z�[�W�\���pUI�A�L�����N�^�[�̃Z���t�Ȃǂ�\������E�B���h�E
        /// </summary>
        [SerializeField]
        NovelUIMessage uiMessage;

        /// <summary>
        /// �����ɕ\�������L�����N�^�[UI
        /// </summary>
        [SerializeField]
        NovelUIChara uiCharaLeft;

        /// <summary>
        /// �����ɕ\�������L�����N�^�[UI
        /// </summary>
        [SerializeField]
        NovelUIChara uiCharaCenter;

        /// <summary>
        /// �E���ɕ\�������L�����N�^�[UI
        /// </summary>
        [SerializeField]
        NovelUIChara uiCharaRight;

        /// <summary>
        /// �\�������摜UI
        /// </summary>
        [SerializeField]
        NovelUIImage uiImage;

        // �eUI�̎Q�Ƃ��擾����
        public NovelUIMessage UIMessage => uiMessage;
        public NovelUIChara UICharaLeft => uiCharaLeft;
        public NovelUIChara UICharaCenter => uiCharaCenter;
        public NovelUIChara UICharaRight => uiCharaRight;
        public NovelUIImage UIImage => uiImage;
    }
}