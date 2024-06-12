using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_SnowShield : MonoBehaviour,IDamageable
{
    [SerializeField] private ArcanaBase _arcana;
    Transform player = null;
    //　現在の角度
    private float angle;
    //　回転するスピード
    [SerializeField] private float rotateSpeed = 180f;
    //　ターゲットからの距離
    [SerializeField] private Vector3 distanceFromTarget = new Vector3(0.0f, 0.0f, 2.5f);

    void IDamageable.Damage(int value)
    {
        
    }

    void IDamageable.Death()
    {
        
    }

    void IDamageable.Protect()
    {
        
    }

    private void Update()
    {
        if (_arcana.GetNormalEffect() == null) return;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //　ユニットの位置 = ターゲットの位置 ＋ ターゲットから見たユニットの角度 ×　ターゲットからの距離
        transform.position = player.position + Quaternion.Euler(0f, angle, 0f) * distanceFromTarget;
        //　ユニット自身の角度 = ターゲットから見たユニットの方向の角度を計算しそれをユニットの角度に設定する
        transform.rotation = Quaternion.LookRotation(transform.position - new Vector3(player.position.x, transform.position.y, player.position.z), Vector3.up);
        //　ユニットの角度を変更
        angle += rotateSpeed * Time.deltaTime;
        //　角度を0～360度の間で繰り返す
        angle = Mathf.Repeat(angle, 360f);
    }
}
