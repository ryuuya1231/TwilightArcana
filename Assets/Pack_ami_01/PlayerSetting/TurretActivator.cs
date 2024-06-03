using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretActivator : MonoBehaviour
{
    // 出現させるタレットオブジェクト（ヒエラルキー内）
    public GameObject turret;
    // トグルキー（例：Eキー）
    public KeyCode toggleKey = KeyCode.E;

    void Awake()
    {
        // タレットをヒエラルキー内の特定のオブジェクトに設定
        GameObject turretObject = GameObject.Find("SelfSupportingTurret");
        if (turretObject != null)
        {
            turret = turretObject;
            // ゲーム起動時に非アクティブに設定
            turret.SetActive(false);
        }
        else
        {
            Debug.LogError("SelfSupportingTurret オブジェクトが見つかりません");
        }
    }

    void Update()
    {
        if (turret == null) return;

        // トグルキーが押されたら
        if (Input.GetKeyDown(toggleKey))
        {
            // タレットがアクティブかどうかをチェックして、トグル
            if (!turret.activeSelf)
            {
                turret.SetActive(true);
                // `AliveTime`スクリプトがタレットの生存時間を管理するので、ここでは何もしない
            }
        }
    }
}