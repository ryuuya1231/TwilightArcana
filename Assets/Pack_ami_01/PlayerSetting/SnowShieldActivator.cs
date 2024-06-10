using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowShieldActivator : MonoBehaviour
{
    // 出現させるシールドオブジェクト（ヒエラルキー内）
    public GameObject shield;
    public GameObject snowEffect;
    // トグルキー（例：スペースキー）
    public KeyCode toggleKey = KeyCode.Space;

    void Awake()
    {
        // secondShieldをヒエラルキー内の特定のオブジェクトに設定し、非アクティブにする
        GameObject secondShieldObject = GameObject.Find("Shield_02");
        if (secondShieldObject != null)
        {
            secondShieldObject.SetActive(false);
        }
        else
        {
            Debug.LogError("Shield_02 オブジェクトが見つかりません");
        }

        // shieldをヒエラルキー内の特定のオブジェクトに設定
        GameObject shieldObject = GameObject.Find("Shield_01");
        if (shieldObject != null)
        {
            shield = shieldObject;
            // ゲーム起動時に非アクティブに設定
            shield.SetActive(false);
        }
        else
        {
            Debug.LogError("Shield_01 オブジェクトが見つかりません");
        }

        // snowEffectをヒエラルキー内の特定のオブジェクトに設定
        GameObject snow = GameObject.Find("Effect_Snow");
        if (snow != null)
        {
            snowEffect = snow;
            // ゲーム起動時に非アクティブに設定
            snowEffect.SetActive(false);
        }
        else
        {
            Debug.LogError("Effect_Snow オブジェクトが見つかりません");
        }

    }

    void Update()
    {
        if (shield == null || snowEffect == null) return;

        // トグルキーが押されたら
        if (Input.GetKeyDown(toggleKey))
        {
            // shieldとsnowEffectが非アクティブな場合、アクティブにする
            if (!shield.activeSelf && !snowEffect.activeSelf)
            {
                shield.SetActive(true);
                snowEffect.SetActive(true);

                // ShieldRotationスクリプトがアタッチされている場合の処理
                ShieldRotation shieldRotationScript = shield.GetComponent<ShieldRotation>();
                //if (shieldRotationScript != null && shieldRotationScript.secondaryShield != null)
                {
                  //  shieldRotationScript.secondaryShield.gameObject.SetActive(true);
                }
            }
        }
    }
}