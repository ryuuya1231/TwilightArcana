using FlMr_Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Arcana_SnowShield", fileName = "Arcana_SnowShield")]
public class Item_SnowShield : ItemBase
{
    public bool Check()
    {
        return true;
    }
    public void Use()
    {
        Debug.Log("プレイヤーに防御力バフを与えます");
    }
}
