using FlMr_Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Arcana_LightningAura", fileName = "Arcana_LightningAura")]
public class Arcana_LightningAura : ItemBase
{
    public bool Check()
    {
        return true;
    }

    public void Use()
    {
        Debug.Log("プレイヤーに雷のオーラを付与します");
    }
}
