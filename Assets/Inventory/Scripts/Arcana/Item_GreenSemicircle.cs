using FlMr_Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Arcana_GreenSemicircle", fileName = "Arcana_GreenSemicircle")]
public class Item_GreenSemicircle : ItemBase
{
    public bool Check()
    {
        return true;
    }

    public void Use()
    {
        Debug.Log("�v���C���[�̑O�ʂɗ΂̔��~�𐶐�");
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
