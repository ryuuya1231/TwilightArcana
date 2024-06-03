using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldRotation : MonoBehaviour
{
    // 指定した対象を中心に円を描くような動きをするスクリプト
    // 中心点
    private Transform shieldTarget;
    // 対象のオブジェクト
    private Transform secondaryShield;
    // 半径
    public float shieldRadius = 0.8f;
    // 回転速度
    public float shieldSpeed = 300.0f;
    // 回転軸（例：Vector3.upはY軸周りの回転を意味します）
    public Vector3 shieldRotationAxis = Vector3.up;
    // 中心に向けるローカル軸（例：Vector3.forwardはZ軸を中心に向けることを意味します）
    public Vector3 shieldLocalForward = Vector3.down;

    // 内部変数
    private float shieldAngle;

    void Awake()
    {
        // secondaryShieldをヒエラルキー内の特定のオブジェクトに設定
        secondaryShield = GameObject.Find("Shield_02").transform;
    }

    void Update()
    {
        shieldTarget = GameObject.FindGameObjectWithTag("Player").transform.Find("male00");
        if (secondaryShield == null || shieldTarget == null) return; // secondaryShieldまたはshieldTargetが設定されていない場合は何もしない

        // 回転速度に応じて角度を更新
        shieldAngle += shieldSpeed * Time.deltaTime;

        //// 回転軸に沿った新しい位置を計算
        //Vector3 offset = Quaternion.AngleAxis(shieldAngle, shieldRotationAxis) * new Vector3(shieldRadius, 0, 0);
        //transform.position = shieldTarget.position + offset;

        //// オブジェクトの向きを中心点に向ける
        //Vector3 direction = transform.position - shieldTarget.position;
        //Quaternion lookRotation = Quaternion.LookRotation(direction, shieldRotationAxis);

        //// ローカル軸を考慮して回転を調整
        //transform.rotation = lookRotation * Quaternion.FromToRotation(shieldLocalForward, Vector3.forward);

        // 真反対の位置を計算して配置
        if (secondaryShield != null)
        {
            Vector3 oppositeOffset = Quaternion.AngleAxis(shieldAngle + 180, shieldRotationAxis) * new Vector3(shieldRadius, 0, 0);
            secondaryShield.position = shieldTarget.position + oppositeOffset;

            // 反対側のオブジェクトの向きを中心点に向ける
            Vector3 oppositeDirection = secondaryShield.position - shieldTarget.position;
            Quaternion oppositeLookRotation = Quaternion.LookRotation(oppositeDirection, shieldRotationAxis);

            // ローカル軸を考慮して回転を調整
            secondaryShield.rotation = oppositeLookRotation * Quaternion.FromToRotation(shieldLocalForward, Vector3.forward);
        }
        Debug.Log(shieldTarget.transform.position);
        Debug.Log(gameObject.transform.position);
        Debug.Log(secondaryShield.transform.position);
    }

    void OnDisable()
    {
        // shieldTargetが非アクティブになるとき、secondaryShieldも同じく非アクティブにする
        if (secondaryShield != null)
        {
            secondaryShield.gameObject.SetActive(false);
        }
    }
}