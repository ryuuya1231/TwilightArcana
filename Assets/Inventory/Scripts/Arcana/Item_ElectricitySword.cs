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
        Debug.Log("�v���C���[����߂��ɑ΂��ė͋����������s���܂�");
    }
}
