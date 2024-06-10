using FlMr_Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Arcana_Arrow", fileName = "Arcana_Arrow")]
public class Arcana_Arrow : ItemBase
{
    public bool Check()
    {
        return true;
    }

    public void Use()
    {
        Debug.Log("–î‚ð”ò‚Î‚µ‚Ü‚·");
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