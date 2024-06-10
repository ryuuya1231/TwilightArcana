using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Turret : MonoBehaviour
{
    [SerializeField] private ArcanaBase _arcana;
    Transform player = null;
    //　ターゲットからの距離
    [SerializeField] private Vector3 distanceFromTarget = new Vector3(0.0f, 0.0f, 2.5f);
    private void Update()
    {
        //if (_arcana.GetNormalEffect() == null) return;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        gameObject.transform.parent = player.transform;
        gameObject.transform.position = player.transform.position;
        gameObject.transform.localPosition = distanceFromTarget;
    }
}
