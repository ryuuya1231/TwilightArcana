using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

namespace FlMr_Inventory
{
    [CreateAssetMenu(menuName = "ItemUtility", fileName = "ItemUtility")]
    public class ItemUtility : ScriptableObject
    {
        #region Singleton
        private static ItemUtility instance;
        public static ItemUtility Instance
        {
            get
            {
                if (instance == null)
                {
                    ItemUtility[] instances = Resources.LoadAll<ItemUtility>("");

                    // �V���O���g���ȃN���X�̃C���X�^���X�͕K��1�łȂ���΂Ȃ�Ȃ�
                    instance = instances.Count() switch
                    {
                        0 => throw new System.Exception("ItemUtility�̃C���X�^���X��Resources�t�H���_���ɑ��݂��܂���"),
                        1 => instances.ElementAt(0),
                        _ => throw new System.Exception("ItemUtility�̃C���X�^���X��Resources�t�H���_���ɕ������݂��܂�")
                    };

                    // �������B��̃C���X�^���X��������
                    instance.Initialize();
                }

                return instance;
            }
        }
        #endregion

        /// <summary>
        /// �Q�[���ɓo�ꂳ�������S�A�C�e��
        /// </summary>
        [SerializeField] private ItemBase[] allItems;

        /// <summary>
        /// Id�ɃA�C�e�������т��鎫��
        /// </summary>
        public ReadOnlyDictionary<int, ItemBase> ItemIdTable { get; private set; }

        /// <summary>
        /// �S�ẴA�C�e����ێ������ǂݎ���p�R���N�V����
        /// </summary>
        public ReadOnlyCollection<ItemBase> AllItems { get; private set; }

        private void Initialize()
        {
            // ItemIdTable�̏�����
            Dictionary<int, ItemBase> idItemMap = new Dictionary<int, ItemBase>();
            foreach (var item in allItems)
            {
                idItemMap.Add(item.UniqueId, item);
            }
            ItemIdTable = new(idItemMap);
            // AllItems�̏�����
            AllItems = new(allItems);
        }
    }


}