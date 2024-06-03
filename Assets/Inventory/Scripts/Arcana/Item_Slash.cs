using FlMr_Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Arcana_Slash", fileName = "Arcana_Slash")]
public class Item_Slash : ItemBase
{
    public bool Check()
    {
        return true;
    }

    public void Use()
    {
        Debug.Log("éaåÇÇê∂ê¨");
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
