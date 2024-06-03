using FlMr_Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Arcana_Turret", fileName = "Arcana_Turret")]
public class Item_Turret : ItemBase
{
    public bool Check()
    {
        return true;
    }
    public void Use()
    {
        Debug.Log("タレットを出します");
    }
}
