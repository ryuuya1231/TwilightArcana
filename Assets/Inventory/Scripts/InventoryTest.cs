using FlMr_Inventory;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InventoryTest : MonoBehaviour
{
    private static bool isInitialized = false;
    private void Start()
    {
        if (!isInitialized)
        {
            // 一度だけ実行したい処理
            isInitialized = true;
            /* 初期化処理 */
            var bag = GameObject.FindGameObjectWithTag("ItemBag").GetComponent<ItemBag>();
            var box = GameObject.FindGameObjectWithTag("ItemBox").GetComponent<ItemBox>();
            Debug.Log("ItemUtilityには現在" + ItemUtility.Instance.AllItems.Count+"種のアイテムが登録されています");
            for (int i = 1; i <= ItemUtility.Instance.AllItems.Count; ++i)
            {
                if (!bag.AddItem(i, 1))
                {
                    box.AddItem(i, 1);
                }
            }
            //bag.AddItem(6, 1);
            //bag.AddItem(7, 1);
        }
    }
}