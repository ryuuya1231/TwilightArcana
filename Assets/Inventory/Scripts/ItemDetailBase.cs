using UnityEngine;

namespace FlMr_Inventory
{
    /// <summary>
    /// スロットがクリックされたときの挙動を制御する抽象クラス
    /// </summary>
    public abstract class ItemDetailBase : MonoBehaviour
    {
        /// <summary>
        /// スロットがクリックされた際に呼ばれるコールバックメソッド
        /// </summary>
        /// <param name="itemBag">アイテムバッグ</param>
        /// <param name="item">スロットに入っているアイテム</param>
        /// <param name="number">そのアイテムの所持数</param>
        /// <param name="slotObj">スロットのゲームオブジェクト</param>
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