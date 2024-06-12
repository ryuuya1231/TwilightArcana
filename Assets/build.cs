using FlMr_Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class build : MonoBehaviour
{
    private static bool isInitialized = false;
    private void Start()
    {
        var itemBag = GameObject.FindGameObjectWithTag("ItemBag").GetComponent<ItemBag>();
        var box = GameObject.FindGameObjectWithTag("ItemBox").GetComponent<ItemBox>();
        if (!isInitialized)
        {
            isInitialized = true;
            for (int i = 1; i <= ItemUtility.Instance.AllItems.Count; ++i)
            {
                if (!itemBag.AddItem(i, 1))
                    box.AddItem(i, 1);
            }
        }
    }
    private void Update()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
