using FlMr_Inventory;
using UnityEngine;

public class BoxInventory : MonoBehaviour
{
    void Start()
    {
        var bag = this.GetComponent<ItemBox>();
        Debug.Log(ItemUtility.Instance.AllItems.Count);
        for (int i = 1; i <= ItemUtility.Instance.AllItems.Count; ++i)
        {
            bag.AddItem(i, 1);
        }
    }
}
