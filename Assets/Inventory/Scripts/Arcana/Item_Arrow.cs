using FlMr_Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Arcana_Arrow", fileName = "Arcana_Arrow")]
public class Item_Arrow : ItemBase
{
    public bool Check()
    {
        return true;
    }

    public void Use()
    {
        Debug.Log("ƒvƒŒƒCƒ„[‚©‚ç‹ß‚¢‚É‘Î‚µ‚Ä–î‚ğ”ò‚Î‚·");
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
