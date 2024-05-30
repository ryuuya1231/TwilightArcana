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
            Debug.Log("‰Šú‰»");
            // ˆê“x‚¾‚¯Às‚µ‚½‚¢ˆ—
            isInitialized = true;
            /* ‰Šú‰»ˆ— */
            var bag = GameObject.FindGameObjectWithTag("ItemBag").GetComponent<ItemBag>();
            Debug.Log(ItemUtility.Instance.AllItems.Count);
            //for (int i = 1; i <= ItemUtility.Instance.AllItems.Count; ++i)
            //{
            //    bag.AddItem(i, 1);
            //}
            bag.AddItem(2, 1);
            bag.AddItem(1, 1);
            bag.AddItem(3, 1);
        }
    }
}