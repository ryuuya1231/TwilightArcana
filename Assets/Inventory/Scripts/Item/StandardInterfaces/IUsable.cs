namespace FlMr_Inventory
{
    /// <summary>
    /// �X���b�g����u�g�p�v���邱�Ƃ��ł���A�C�e���Ɏ�������C���^�[�t�F�[�X
    /// </summary>
    public interface IUsable
    {
        /// <summary>
        /// �A�C�e�����g�p�����Ƃ��ɔ����������
        /// </summary>
        void Use();

        /// <summary>
        /// �A�C�e�����g�p�\�ł��邩�𔻒肷��
        /// </summary>
        /// <returns></returns>
        bool Check();
    }
}