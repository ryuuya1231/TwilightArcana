namespace FlMr_Inventory
{
    /// <summary>
    /// ショップで販売されるアイテムに実装するインターフェース
    /// </summary>
    public interface IStoreItem
    {
        /// <summary>
        /// アイテムの値段
        /// </summary>
        int Price { get; }

        /// <summary>
        /// 現在のショップで販売してよいかを判定する
        /// </summary>
        bool CanBuy { get; }
    }
}