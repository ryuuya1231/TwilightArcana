namespace FlMr_Inventory
{
    /// <summary>
    /// 売却可能なアイテムに実装するインターフェース
    /// </summary>
    public interface ICashable
    {
        /// <summary>
        /// 売値
        /// </summary>
        int SellingPrice { get; }
    }
}