using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FlMr_Inventory
{
    public class ItemBox : MonoBehaviour
    {
        /// <summary>
        /// �����̃X���b�g��
        /// </summary>
        [SerializeField] private int slotNumber = 10;

        /// <summary>
        /// �X���b�g�I�u�W�F�N�g�̃v���n�u
        /// </summary>
        [SerializeField] private GameObject slotPrefab;

        /// <summary>
        /// �X���b�g���N���b�N���ꂽ�ۂ̋���
        /// </summary>
        [SerializeField] private ItemDetailBase itemDetail;

        /// <summary>
        /// �S�ẴX���b�g�I�u�W�F�N�g
        /// </summary>
        private List<ItemBoxSlot> AllSlots { get; } = new();

        void Awake()
        {
            for (int i = 0; i < slotNumber; i++)
            {
                //slotNumber �̐������X���b�g�𐶐����AItemBag�̎q�I�u�W�F�N�g�Ƃ��Ĕz�u����
                var slot = Instantiate(slotPrefab, this.transform, false)
                    .GetComponent<ItemBoxSlot>();
                // ItemSlot�̏�����
                slot.Initialize(
                    // �X���b�g���N���b�N���ꂽ�ۂɌĂ΂��֐�
                    (item, number, slotObj) => itemDetail.OnClickCallback_Box(this, item, number, slotObj)
                );
                AllSlots.Add(slot);
            }
            UpdateItem();
        }
        /// <summary>
        /// �A�C�e�����o�b�O�ɒǉ�����
        /// </summary>
        /// <param name="itemBase">�ǉ��������A�C�e����ID</param>
        /// <param name="number">�ǉ���������</param>
        /// <returns>�o�b�O�ւ̒ǉ��ɐ���������</returns>
        public bool AddItem(int itemId, int number)
        {
            if (!Data.Ids.Contains(itemId) && Data.Ids.Count == slotNumber)
            {
                // �X���b�g�����܂��Ă����Ԃł́A�������A�C�e���̒ǉ��͏o���Ȃ�
                return false;
            }

            // �A�C�e�����o�b�O�ɒǉ�����
            Data.Add(itemId, number);
            UpdateItem();
            return true;
        }
        /// <summary>
        /// �j�ƂȂ�f�[�^
        /// </summary>
        [Serializable]
        private class ItemBoxData
        {
            /// <summary>
            /// �������Ă���A�C�e����Id
            /// </summary>
            public List<int> Ids = new List<int>();

            /// <summary>
            /// ������
            /// </summary>
            public List<int> Qty = new List<int>();

            /// <summary>
            /// �o�b�O�ɒǉ�����
            /// </summary>
            /// <param name="id">�ǉ�����A�C�e����id</param>
            /// <param name="number">�ǉ������</param>
            public void Add(int id, int number)
            {
                // �A�C�e���ԍ�=id �̃A�C�e�������Ƀo�b�O���ɑ��݂��邩
                // ���݂���Ȃ牽�Ԗڂ̃X���b�g�ɓ����Ă��邩
                int index = Ids.IndexOf(id);
                if (index < 0)
                {
                    // �������̃A�C�e���̏ꍇ�́A�X���b�g��1�����
                    Ids.Add(id);

                    //���̍X�V
                    Qty.Add(number);
                }
                else
                {
                    // ���ɏ������Ă���A�C�e���̏ꍇ�́A�������݂̂�ǉ�
                    Qty[index] += number;
                }
            }

            /// <summary>
            /// �o�b�O������o��
            /// </summary>
            /// <param name="id">���o�������A�C�e����id</param>
            /// <param name="number">���o����</param>
            public void Remove(int id, int number)
            {
                // �A�C�e���ԍ�=id �̃A�C�e�������Ƀo�b�O���ɑ��݂��邩
                // ���݂���Ȃ牽�Ԗڂ̃X���b�g�ɓ����Ă��邩
                int index = Ids.IndexOf(id);
                if (index < 0)
                {
                    // �������̃A�C�e�����ǂ�o�����Ƃ͏o���Ȃ�
                    throw new Exception($"�A�C�e��(id:{id})�̎��o���Ɏ��s���܂���");
                }
                else
                {
                    if (Qty[index] < number)
                    {
                        // �K�v���������Ă��Ȃ�
                        throw new Exception($"�A�C�e��(id:{id})�̎��o���Ɏ��s���܂���");
                    }
                    else
                    {
                        //���o��
                        Qty[index] -= number;

                        if (Qty[index] == 0)
                        {
                            // 0�ɂȂ����ꍇ�̓��X�g����폜
                            Qty.RemoveAt(index);
                            Ids.RemoveAt(index);
                        }
                    }
                }
            }

            /// <summary>
            /// ����̃A�C�e���������������Ă��邩
            /// </summary>
            /// <param name="id">�A�C�e��id</param>
            /// <returns>������</returns>
            public int GetQty(int id)
            {
                int index = Ids.IndexOf(id);
                return index < 0 ? 0 : Qty[index];
            }

        }

        /// <summary>
        /// ���ݏ������Ă���A�C�e���̏��
        /// </summary>
        private ItemBoxData Data { get; set; } = new();

        /// <summary>
        /// ItemBagData���V���A��������
        /// </summary>
        /// <returns></returns>
        public string ToJson() => JsonUtility.ToJson(Data);

        /// <summary>
        /// �X���b�g�̕\���Ə����A�C�e���̏�����v������
        /// </summary>
        private void UpdateItem()
        {
            for (int i = 0; i < Data.Ids.Count; ++i)
            {
                // �ǉ��������A�C�e����id
                int itemId = Data.Ids[i];

                // �S�A�C�e������itemId�����A�C�e������������
                ItemBase addingItem = ItemUtility.Instance.ItemIdTable[itemId];
                // �A�C�e����\��
                AllSlots[i].UpdateItem(addingItem, Data.Qty[i]);
            }
            for (int i = Data.Ids.Count; i < slotNumber; i++)
            {
                // �c��͋�
                AllSlots[i].UpdateItem(null, -1);
            }
        }

        /// <summary>
        /// �A�C�e�����폜����
        /// </summary>
        /// <param name="itemId">�폜����A�C�e����Id</param>
        /// <param name="number">�폜�����</param>
        /// <returns></returns>
        public bool RemoveItem(int itemId, int number)
        {
            // �\���Ȑ����������Ă��邩
            bool haveEnough = Data.GetQty(itemId) >= number;

            if (haveEnough)
            {
                // �\�������Ă���ꍇ
                Data.Remove(itemId, number);

                UpdateItem();
                return true;
            }
            else
            {
                //�s�����Ă���ꍇ
                return false;
            }
        }

        /// <summary>
        /// �o�b�O���̑S�ẴA�C�e���Ƃ��̌����擾����
        /// </summary>
        /// <returns></returns>
        public Dictionary<ItemBase, int> GetAllItems()
        {
            return Data.Ids
                .ToDictionary(id => ItemUtility.Instance.ItemIdTable[id], id => Data.GetQty(id));

            /***** Linq���g��Ȃ��L�q ******
            Dictionary<ItemBase, int> result = new Dictionary<ItemBase, int>();
            foreach (var id in Data.Ids)
            {
                ItemBase item = ItemUtility.Instance.ItemIdTable[id];
                result.Add(item,Data.GetQty(id));
            }
            return result;
            *****************************/

        }

        /// <summary>
        /// id���w�肵�Č����擾
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Find(int id)
        {
            return Data.GetQty(id);
        }

        /// <summary>
        /// �A�C�e�����w�肵�Č����擾
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int Find(ItemBase item) => Find(item.UniqueId);
    }
}
