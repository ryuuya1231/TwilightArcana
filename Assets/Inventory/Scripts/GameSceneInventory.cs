using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.PlayerSettings;

namespace FlMr_Inventory
{
    public class GameSceneInventory : MonoBehaviour
    {
        /// <summary>
        /// 初期のスロット数
        /// </summary>
        [SerializeField] private int slotNumber = 10;

        /// <summary>
        /// スロットオブジェクトのプレハブ
        /// </summary>
        [SerializeField] private GameObject slotPrefab;

        /// <summary>
        /// スロットがクリックされた際の挙動
        /// </summary>
        [SerializeField] private ItemDetailBase itemDetail;

        /// <summary>
        /// 全てのスロットオブジェクト
        /// </summary>
        private List<GameSceneSlot> AllSlots { get; } = new();

        void Awake()
        {
            for (int i = 0; i < slotNumber; i++)
            {
                //slotNumber の数だけスロットを生成し、ItemBagの子オブジェクトとして配置する
                var slot = Instantiate(slotPrefab, this.transform, false)
                    .GetComponent<GameSceneSlot>();
                // ItemSlotの初期化
                slot.Initialize(
                    // スロットがクリックされた際に呼ばれる関数
                    (item, number, slotObj) => itemDetail.OnClickCallback_Game(this, item, number, slotObj)
                );
                AllSlots.Add(slot);
            }
            UpdateItem();
        }

        /// <summary>
        /// アイテムをバッグに追加する
        /// </summary>
        /// <param name="itemBase">追加したいアイテムのID</param>
        /// <param name="number">追加したい個数</param>
        /// <returns>バッグへの追加に成功したか</returns>
        public bool AddItem(int itemId, int number)
        {
            if (!Data.Ids.Contains(itemId) && Data.Ids.Count == slotNumber)
            {
                // スロットが埋まっている状態では、未所持アイテムの追加は出来ない
                return false;
            }

            // アイテムをバッグに追加する
            Data.Add(itemId, number);
            UpdateItem();
            return true;
        }

        /// <summary>
        /// 核となるデータ
        /// </summary>
        [Serializable]
        private class ItemBagData
        {
            /// <summary>
            /// 所持しているアイテムのId
            /// </summary>
            public List<int> Ids = new List<int>();

            /// <summary>
            /// 所持数
            /// </summary>
            public List<int> Qty = new List<int>();

            /// <summary>
            /// バッグに追加する
            /// </summary>
            /// <param name="id">追加するアイテムのid</param>
            /// <param name="number">追加する個数</param>
            public void Add(int id, int number)
            {
                // アイテム番号=id のアイテムが既にバッグ内に存在するか
                // 存在するなら何番目のスロットに入っているか
                int index = Ids.IndexOf(id);
                if (index < 0)
                {
                    // 未所持のアイテムの場合は、スロットを1つ消費する
                    Ids.Add(id);

                    //個数の更新
                    Qty.Add(number);
                }
                else
                {
                    // 既に所持しているアイテムの場合は、所持数のみを追加
                    Qty[index] += number;
                }
            }

            /// <summary>
            /// バッグから取り出す
            /// </summary>
            /// <param name="id">取り出したいアイテムのid</param>
            /// <param name="number">取り出す個数</param>
            public void Remove(int id, int number)
            {
                // アイテム番号=id のアイテムが既にバッグ内に存在するか
                // 存在するなら何番目のスロットに入っているか
                int index = Ids.IndexOf(id);
                if (index < 0)
                {
                    // 未所持のアイテムをどり出すことは出来ない
                    throw new Exception($"アイテム(id:{id})の取り出しに失敗しました");
                }
                else
                {
                    if (Qty[index] < number)
                    {
                        // 必要数所持していない
                        throw new Exception($"アイテム(id:{id})の取り出しに失敗しました");
                    }
                    else
                    {
                        //取り出す
                        Qty[index] -= number;

                        if (Qty[index] == 0)
                        {
                            // 0個になった場合はリストから削除
                            Qty.RemoveAt(index);
                            Ids.RemoveAt(index);
                        }
                    }
                }
            }

            /// <summary>
            /// 特定のアイテムをいくつ所持しているか
            /// </summary>
            /// <param name="id">アイテムid</param>
            /// <returns>所持数</returns>
            public int GetQty(int id)
            {
                int index = Ids.IndexOf(id);
                return index < 0 ? 0 : Qty[index];
            }
        }

        /// <summary>
        /// 現在所持しているアイテムの情報
        /// </summary>
        private ItemBagData Data { get; set; } = new();

        public ItemBase GetItem(int count)
        {
            if (Data != null && count >= 0 && count < Data.Ids.Count)
            {
                int itemId = Data.Ids[count];
                ItemBase addingItem = ItemUtility.Instance.ItemIdTable[itemId];
                return addingItem;
            }
            else return null;
        }

        /// <summary>
        /// ItemBagDataをシリアル化する
        /// </summary>
        /// <returns></returns>
        public string ToJson() => JsonUtility.ToJson(Data);

        /// <summary>
        /// スロットの表示と所持アイテムの情報を一致させる
        /// </summary>
        private void UpdateItem()
        {
            Debug.Log("更新");
            for (int i = 0; i < Data.Ids.Count; ++i)
            {
                Debug.Log("アイテム追加");
                // 追加したいアイテムのid
                int itemId = Data.Ids[i];

                // 全アイテムからitemIdをもつアイテムを検索する
                ItemBase addingItem = ItemUtility.Instance.ItemIdTable[itemId];
                // アイテムを表示
                AllSlots[i].UpdateItem(addingItem, Data.Qty[i]);
            }
            for (int i = Data.Ids.Count; i < slotNumber; ++i)
            {
                //Debug.Log("スロットナンバー" + slotNumber + "空きスロット生成" + i);
                // 残りは空
                AllSlots[i].UpdateItem(null, -1);
            }
            Debug.Log("更新終了");
        }

        /// <summary>
        /// アイテムを削除する
        /// </summary>
        /// <param name="itemId">削除するアイテムのId</param>
        /// <param name="number">削除する個数</param>
        /// <returns></returns>
        public bool RemoveItem(int itemId, int number)
        {
            // 十分な数を所持しているか
            bool haveEnough = Data.GetQty(itemId) >= number;

            if (haveEnough)
            {
                Debug.Log("アイテム削除");
                // 十分持っている場合
                Data.Remove(itemId, number);

                UpdateItem();
                return true;
            }
            else
            {
                //不足している場合
                return false;
            }
        }

        /// <summary>
        /// バッグ内の全てのアイテムとその個数を取得する
        /// </summary>
        /// <returns></returns>
        public Dictionary<ItemBase, int> GetAllItems()
        {
            return Data.Ids
                .ToDictionary(id => ItemUtility.Instance.ItemIdTable[id], id => Data.GetQty(id));

            /***** Linqを使わない記述 ******
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
        /// idを指定して個数を取得
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Find(int id)
        {
            return Data.GetQty(id);
        }
        /// <summary>
        /// アイテムを指定して個数を取得
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int Find(ItemBase item) => Find(item.UniqueId);
    }
}
