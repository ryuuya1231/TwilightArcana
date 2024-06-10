using UnityEngine;

namespace FlMr_Inventory
{
    /// <summary>
    /// �X���b�g���N���b�N���ꂽ�Ƃ��̋����𐧌䂷�钊�ۃN���X
    /// </summary>
    public abstract class ItemDetailBase : MonoBehaviour
    {
        /// <summary>
        /// �X���b�g���N���b�N���ꂽ�ۂɌĂ΂��R�[���o�b�N���\�b�h
        /// </summary>
        /// <param name="itemBag">�A�C�e���o�b�O</param>
        /// <param name="item">�X���b�g�ɓ����Ă���A�C�e��</param>
        /// <param name="number">���̃A�C�e���̏�����</param>
        /// <param name="slotObj">�X���b�g�̃Q�[���I�u�W�F�N�g</param>
        protected internal abstract void OnClickCallback
            (ItemBag itemBag, ItemBase item, int number, GameObject slotObj);
        protected internal abstract void OnClickCallback_Box
            (ItemBox itemBog, ItemBase item, int number, GameObject slotObj);
        protected internal abstract void OnClickCallback_Select
     (SelectItemBox itemBog, ItemBase item, int number, GameObject slotObj);
        protected internal abstract void OnClickCallback_Game
            (GameSceneInventory itemGameBox, ItemBase item, int number, GameObject slotObj);
    }
}