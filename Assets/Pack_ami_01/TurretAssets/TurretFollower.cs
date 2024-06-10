using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretFollower : MonoBehaviour
{
    // 追従するターゲットオブジェクト
    public Transform turretTarget;
    // ゆらゆら動く動作の振幅
    public float amplitude = 0.05f;
    // ゆらゆら動く動作の速度
    public float frequency = 1.0f;
    // ターゲットからのオフセット
    public Vector3 turretOffset;
    // 遅延速度
    public float delaySpeed = 5.0f;

    void Awake()
    {
        // turretTargetをヒエラルキー内の特定のオブジェクトに設定
        // turretTarget = GameObject.Find("Player").transform;
    }

    void Update()
    {
        if (turretTarget == null) return;

        // サイン波を使ってY軸の位置を計算
        float moveY = Mathf.Sin(Time.time * frequency) * amplitude;

        // 目標位置を計算
        Vector3 targetPosition = new Vector3(turretTarget.position.x + turretOffset.x, turretTarget.position.y + turretOffset.y + moveY, turretTarget.position.z + turretOffset.z);

        // 線形補間を使って遅延を追加
        transform.position = Vector3.Lerp(transform.position, targetPosition, delaySpeed * Time.deltaTime);
    }
}