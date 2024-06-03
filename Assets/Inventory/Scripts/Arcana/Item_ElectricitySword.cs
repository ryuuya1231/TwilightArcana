using FlMr_Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Arcana_ElectricitySword", fileName = "Arcana_ElectricitySword")]
public class Item_ElectricitySword : ItemBase
{
    public bool Check()
    {
        return true;
    }
    public void Use()
    {
        Debug.Log("プレイヤーから近いに対して力強い剣戟を行います");
    }
}
