using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FlMr_Inventory
{
    public class ItemBag : MonoBehaviour
    {
        /// �����̃X���b�g��
        [SerializeField] private int slotNumber = 10;
        /// �X���b�g�I�u�W�F�N�g�̃v���n�u
        [SerializeField] private GameObject slotPrefab;
        /// �X���b�g���N���b�N���ꂽ�ۂ̋���
        [SerializeField] private ItemDetailBase itemDetail;
        /// �S�ẴX���b�g�I�u�W�F�N�g
        private List<ItemSlot> AllSlots { get; } = new();
        void Awake()
        {
            for (int i = 0; i < slotNumber; i++)
            {
                //slotNumber �̐������X���b�g�𐶐����AItemBag�̎q�I�u�W�F�N�g�Ƃ��Ĕz�u����
                var slot = Instantiate(slotPrefab, this.transform, false)
                    .GetComponent<ItemSlot>();
                // ItemSlot�̏�����
                slot.Initialize(
                    // �X���b�g���N���b�N���ꂽ�ۂɌĂ΂��֐�
                    (item, number, slotObj) => itemDetail.OnClickCallback(this, item, number, slotObj)
                );
                AllSlots.Add(slot);
            }
            UpdateItem();
        }
        /// �A�C�e�����o�b�O�ɒǉ�����
        /// <param name="itemBase">�ǉ��������A�C�e����ID</param>
        /// <param name="number">�ǉ���������</param>
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
        /// �j�ƂȂ�f�[�^
        [Serializable]
        private class ItemBagData
        {
            /// �������Ă���A�C�e����Id
            public List<int> Ids = new List<int>();
            /// ������
            public List<int> Qty = new List<int>();
            /// �o�b�O�ɒǉ�����
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
            /// �o�b�O������o��
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
            /// ����̃A�C�e���������������Ă��邩
            /// <param name="id">�A�C�e��id</param>
            /// <returns>������</returns>
            public int GetQty(int id)
            {
                int index = Ids.IndexOf(id);
                return index < 0 ? 0 : Qty[index];
            }

        }
        /// ���ݏ������Ă���A�C�e���̏��
        private ItemBagData Data { get; set; } = new();

        public int GetItemData(int count)
        {
            int itemId = 0;
            itemId = Data.Ids[count];
            return itemId;
        }

        /// ItemBagData���V���A��������
        public string ToJson() => JsonUtility.ToJson(Data);
        /// �X���b�g�̕\���Ə����A�C�e���̏�����v������
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
            for (int i = Data.Ids.Count; i < slotNumber; ++i)
            {
                //Debug.Log("�X���b�g�i���o�[" + slotNumber + "�󂫃X���b�g����" + i);
                // �c��͋�
                AllSlots[i].UpdateItem(null, -1);
            }
        }
        /// �A�C�e�����폜����
        /// <param name="itemId">�폜����A�C�e����Id</param>
        /// <param name="number">�폜�����</param>
        public bool RemoveItem(int itemId, int number)
        {
            // �\���Ȑ����������Ă��邩
            bool haveEnough = Data.GetQty(itemId) >= number;
            if (haveEnough)
            {
                Debug.Log("�A�C�e���폜");
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
        /// �o�b�O���̑S�ẴA�C�e���Ƃ��̌����擾����
        public Dictionary<ItemBase, int> GetAllItems()
        {
            return Data.Ids
                .ToDictionary(id => ItemUtility.Instance.ItemIdTable[id], id => Data.GetQty(id));
        }

        /// id���w�肵�Č����擾
        /// <param name="id"></param>
        public int Find(int id)
        {
            return Data.GetQty(id);
        }
        /// �A�C�e�����w�肵�Č����擾
        /// <param name="item"></param>
        public int Find(ItemBase item) => Find(item.UniqueId);
    }
}