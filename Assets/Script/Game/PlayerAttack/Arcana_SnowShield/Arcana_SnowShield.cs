using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Arcana_SnowShield : ArcanaBase
{
    public Transform snowEffectTarget;
    public override void ArcanaEffect()
    {
        _prefab = (GameObject)Resources.Load("SnowShield/shield");
        snowEffectTarget = GameObject.FindGameObjectWithTag("Player").transform;
        SetUp();
    }

    private void SetUp()
    {
        if (snowEffectTarget == null) return;
        // ���݂�Y���̈ʒu��ێ�
        Debug.Log("setup");
        float currentY = transform.position.y;

        // �^�[�Q�b�g��X����Z���̈ʒu��Ǐ]���AY���͌��݂̈ʒu���ێ�
        transform.position = new Vector3(snowEffectTarget.position.x, currentY, snowEffectTarget.position.z);
    }
}
