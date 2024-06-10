using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace FlMr_Inventory
{
    internal class ItemBoxSlot : MonoBehaviour
    {
        /// <summary>
        /// UI�摜�̕\�����i��N���X
        /// �������Ă���A�C�e���̃A�C�R����\������
        /// </summary>
        [SerializeField] private Image icon;
        /// <summary>
        /// ���̃X���b�g�ɓ����Ă���A�C�e��
        /// </summary>
        internal ItemBase Item { get; private set; }
        /// <summary>
        /// �A�C�e���̃A�C�R����\������
        /// </summary>
        /// <param name="item"></param>
        /// <param name="number"></param>
        internal void UpdateItem(ItemBase item, int number)
        {
            if (number > 0 && item != null)
            {
                // �A�C�e������ł͂Ȃ��ꍇ
                Item = item;
                icon.sprite = item.Icon;
                icon.color = Color.white;

                // �A�C�R���̕\��
                icon.sprite = item.Icon;
                icon.color = Color.white;

                // ���ʂ̕\��
                numberText.gameObject.SetActive(number > 1);
                numberText.text = number.ToString();
                Number = number;
            }
            else
            {
                Item = null;
                Number = 0;
                icon.sprite = null;
                icon.color = new Color(0, 0, 0, 0);
                numberText.gameObject.SetActive(false);
            }
        }
        /// <summary>
        /// ���̃X���b�g�ɓ����Ă���A�C�e���̌���\������e�L�X�g
        /// </summary>
        [SerializeField] private TextMeshProUGUI numberText;
        /// <summary>
        /// ����
        /// </summary>
        private int Number { get; set; }
        /// <summary>
        /// �X���b�g���N���b�N���ꂽ�ۂɎ��s���郁�\�b�h
        /// [ ���� ]
        /// ItemBase : �X���b�g�ɓ����Ă���A�C�e��
        /// int : �A�C�e���̌�
        /// GameObject : ���̃X���b�g�̃I�u�W�F�N�g
        /// </summary>
        private Action<ItemBase, int, GameObject> OnClickCallback { get; set; }
        /// <summary>
        /// ���̃N���X�̃C���X�^���X���������ꂽ�ۂɌĂԃ��\�b�h
        /// </summary>
        /// <param name="onClickCallback"></param>
        internal void Initialize(Action<ItemBase, int, GameObject> onClickCallback)
        {
            OnClickCallback = onClickCallback;
        }
        /// <summary>
        /// �X���b�g���N���b�N���ꂽ�Ƃ��ɌĂ΂�郁�\�b�h
        /// </summary>
        public void OnClicked()
        {
            //���̃X���b�g�ɃA�C�e�������݂��Ă���ꍇ
            if (Item != null)
            {
                // �R�[���o�b�N���\�b�h�����s
                OnClickCallback(Item, Number, this.gameObject);
                if (SceneManager.GetActiveScene().name == "BildScene")
                {
                    var s_slot = GameObject.FindGameObjectWithTag("SelectSlot").GetComponent<SelectItemSlot>();
                    if (!(s_slot.Item))
                    {
                        Debug.Log("�I��");
                        var select = GameObject.FindGameObjectWithTag("SelectBox").GetComponent<SelectItemBox>();
                        select.AddItem(Item.UniqueId, 1);
                        var box = GameObject.FindGameObjectWithTag("ItemBox").GetComponent<ItemBox>();
                        box.RemoveItem(Item.UniqueId, 1);
                    }
                }
            }
            else
            {
                if (SceneManager.GetActiveScene().name == "BildScene")
                {
                    var select = GameObject.FindGameObjectWithTag("SelectBox").GetComponent<SelectItemBox>();
                    if (select.GetAllItems().Count > 0)
                    {
                        var box = GameObject.FindGameObjectWithTag("ItemBox").GetComponent<ItemBox>();
                        var s_slot = GameObject.FindGameObjectWithTag("SelectSlot").GetComponent<SelectItemSlot>();
                        Debug.Log("�󂫃X���b�g��" + s_slot.Item.name + "�����܂�");
                        box.AddItem(s_slot.Item.UniqueId, 1);
                        select.RemoveItem(s_slot.Item.UniqueId, 1);
                    }
                }
            }
        }
        //==============================================
    }
}
