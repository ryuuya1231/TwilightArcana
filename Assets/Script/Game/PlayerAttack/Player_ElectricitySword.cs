using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.VFX;

public class Player_ElectricitySword : MonoBehaviour
{
    [SerializeField] private ArcanaBase _arcana;
    private int effectCount = 0;
    Transform player = null;
    Transform enemy = null;
    //　現在の角度
    private float angle;
    //　回転するスピード
    [SerializeField] private float rotateSpeed = 180f;
    //　ターゲットからの距離
    [SerializeField] private Vector3 distanceFromTarget = new Vector3(0f, 1f, 2f);

    private Vector3 lockonRotate;

    //それぞれの位置を保存する変数
    //中継地点
    private Vector3 greenPos;
    private Vector3 startPos;
    private Vector3 goalPos;
    //進む割合を管理する変数
    float time;

    private void Update()
    {
        if (_arcana.gameObject == null) return;
        if (effectCount < 500)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform.Find("male00");
            enemy = GameObject.FindGameObjectWithTag("Enemy").transform;
            //　ユニットの位置 = ターゲットの位置 ＋ ターゲットから見たユニットの角度 ×　ターゲットからの距離
            transform.position = player.position + Quaternion.Euler(0f, angle, 0f) * distanceFromTarget;
            //　ユニット自身の角度 = ターゲットから見たユニットの方向の角度を計算しそれをユニットの角度に設定する
            transform.rotation = Quaternion.LookRotation(transform.position - new Vector3(player.position.x, transform.position.y, player.position.z), Vector3.up);
            //　ユニットの角度を変更
            angle += rotateSpeed * Time.deltaTime;
            //　角度を0～360度の間で繰り返す
            angle = Mathf.Repeat(angle, 360f);
            ++effectCount;
            //else if (effectCount < 510)
            //{
            //    //++effectCount;
            //    //greenPos.Set(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
            //    //if (enemy)
            //    //{
            //    //    goalPos = enemy.gameObject.transform.position;
            //    //}
            //    //startPos = gameObject.transform.position;
            //}
        }
        else
        {
            ////弾の進む割合をTime.deltaTimeで決める
            //time += Time.deltaTime;
            ////二次ベジェ曲線
            ////スタート地点から中継地点までのベクトル上を通る点の現在の位置
            //var a = Vector3.Lerp(startPos, greenPos, time);
            ////中継地点からターゲットまでのベクトル上を通る点の現在の位置
            //var b = Vector3.Lerp(greenPos, goalPos, time);
            ////上の二つの点を結んだベクトル上を通る点の現在の位置（弾の位置）
            //gameObject.transform.position = Vector3.Lerp(a, b, time);
            //gameObject.transform.Rotate(gameObject.transform.forward);
            lockonRotate = enemy.transform.position - gameObject.transform.position;
            gameObject.transform.eulerAngles = lockonRotate;
            //Debug.Log();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            var enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<GolemController>();
            if (enemy)
            {
                enemy.GetAnimator().SetTrigger("Damage");
                enemy.GetNavMesh().updatePosition = false;
                enemy.GetNavMesh().updateRotation = false;
                enemy.GetNavMesh().speed = 0.0f;
                Debug.Log(enemy.name + "と衝突");
            }
            if (_arcana.GetNormalEffect() == null) return;
            Destroy(_arcana.GetNormalEffect());
        }
    }
}
