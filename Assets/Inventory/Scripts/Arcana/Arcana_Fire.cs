using FlMr_Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

[CreateAssetMenu(menuName = "Item/Arcana_Fire", fileName = "Arcana_Fire")]
public class Arcana_Fire : ItemBase//, IUsable, IDeletable
{
    public bool Check()
    {
        return true;
    }

    public void Use()
    {
        Debug.Log("�v���C���[�̌����ɉΑ����̖��@�e���΂��܂�");
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
