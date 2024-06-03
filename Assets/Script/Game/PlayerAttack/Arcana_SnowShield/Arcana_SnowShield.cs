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
        // 現在のY軸の位置を保持
        Debug.Log("setup");
        float currentY = transform.position.y;

        // ターゲットのX軸とZ軸の位置を追従し、Y軸は現在の位置を維持
        transform.position = new Vector3(snowEffectTarget.position.x, currentY, snowEffectTarget.position.z);
    }
}
