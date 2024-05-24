using Effekseer;
using UnityEngine;

public class EffekseerTest : MonoBehaviour
{

    bool nowBtnPresd = false;

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Q))
        {
            nowBtnPresd = true;
        }
    }

    void FixedUpdate()
    {
        // エフェクトを取得する。
        EffekseerEffectAsset effect = Resources.Load<EffekseerEffectAsset>("FireBall02");

        if (nowBtnPresd)
        {
            // transformの位置でエフェクトを再生する
            EffekseerHandle handle = EffekseerSystem.PlayEffect(effect, transform.position);
            // transformの回転を設定する。
            handle.SetRotation(transform.rotation);

            nowBtnPresd = false;

        }
    }
}
