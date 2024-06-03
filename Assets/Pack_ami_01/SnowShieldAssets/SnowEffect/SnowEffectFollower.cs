using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowEffectFollower : MonoBehaviour
{
    // 追従するターゲットオブジェクト
    private Transform snowEffectTarget;

    void Awake()
    {
        // ヒエラルキー内の特定のオブジェクトを設定
        snowEffectTarget = GameObject.Find("Player").transform.Find("male00"); ;
    }

    void Update()
    {
        if (snowEffectTarget == null) return;

        // 現在のY軸の位置を保持
        float currentY = transform.position.y;

        // ターゲットのX軸とZ軸の位置を追従し、Y軸は現在の位置を維持
        transform.position = new Vector3(snowEffectTarget.position.x, currentY, snowEffectTarget.position.z);
    }
}