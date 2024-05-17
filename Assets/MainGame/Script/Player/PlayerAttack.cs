using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacker : MonoBehaviour
{
    [SerializeField] private Charadata data;
    public void HitTrigger(Collider other)
    {
        //otherのゲームオブジェクトのインターフェースを呼び出す
        IDamageable damageable = other.GetComponent<IDamageable>();

        //damageableにnull値が入っていないかチェック
        if (damageable != null)
        {

            //damageableのダメージ処理メソッドを呼び出す。引数としてPlayer1のATKを指定
            damageable.Damage(data.ATK);
        }
    }
}
