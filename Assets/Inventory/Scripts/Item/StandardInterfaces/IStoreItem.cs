namespace FlMr_Inventory
{
    /// <summary>
    /// �V���b�v�Ŕ̔������A�C�e���Ɏ�������C���^�[�t�F�[�X
    /// </summary>
    public interface IStoreItem
    {
        /// <summary>
        /// �A�C�e���̒l�i
        /// </summary>
        int Price { get; }

        /// <summary>
        /// ���݂̃V���b�v�Ŕ̔����Ă悢���𔻒肷��
        /// </summary>
        bool CanBuy { get; }
    }
}