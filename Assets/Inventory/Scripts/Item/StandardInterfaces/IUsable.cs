namespace FlMr_Inventory
{
    /// <summary>
    /// スロットから「使用」することができるアイテムに実装するインターフェース
    /// </summary>
    public interface IUsable
    {
        /// <summary>
        /// アイテムを使用したときに発動する効果
        /// </summary>
        void Use();

        /// <summary>
        /// アイテムが使用可能であるかを判定する
        /// </summary>
        /// <returns></returns>
        bool Check();
    }
}