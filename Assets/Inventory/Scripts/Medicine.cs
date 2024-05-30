using FlMr_Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Medicine", fileName = "Medicine")]
public class Medicine : ItemBase,IUsable,IDeletable
{
    public bool Check()
    {
        return true;
    }

    public void Use()
    {
        Debug.Log("ƒvƒŒƒCƒ„[‚ÌHP‚ğ50‰ñ•œ‚µ‚Ü‚·");
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
