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
                    var instances = Resources.LoadAll<ItemUtility>("");

                    // シングルトンなクラスのインスタンスは必ず1つでなければならない
                    instance = instances.Count() switch
                    {
                        0 => throw new System.Exception("ItemUtilityのインスタンスがResourcesフォルダ内に存在しません"),
                        1 => instances.ElementAt(0),
                        _ => throw new System.Exception("ItemUtilityのインスタンスがResourcesフォルダ内に複数存在します")
                    };

                    // 見つけた唯一のインスタンスを初期化
                    instance.Initialize();
                }

                return instance;
            }
        }
        #endregion

        /// <summary>
        /// ゲームに登場させたい全アイテム
        /// </summary>
        [SerializeField] private ItemBase[] allItems;

        /// <summary>
        /// Idにアイテムを結びつける辞書
        /// </summary>
        public ReadOnlyDictionary<int, ItemBase> ItemIdTable { get; private set; }

        /// <summary>
        /// 全てのアイテムを保持した読み取り専用コレクション
        /// </summary>
        public ReadOnlyCollection<ItemBase> AllItems { get; private set; }

        private void Initialize()
        {
            // ItemIdTableの初期化

            Dictionary<int, ItemBase> idItemMap = new Dictionary<int, ItemBase>();
            foreach (var item in allItems)
            {
                Debug.Log("AllItemsの初期化");
                idItemMap.Add(item.UniqueId, item);
            }
            ItemIdTable = new(idItemMap);




            // AllItemsの初期化
            Debug.Log("AllItemsの初期化");
            AllItems = new(allItems);
        }
    }


}