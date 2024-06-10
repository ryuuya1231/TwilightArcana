using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class GolemController : MonoBehaviour
{

    [SerializeField]
    private NavMeshAgent _navMeshAgent;
    //追いかける対象
    [SerializeField]
    private Transform _player;
    [SerializeField]
    private Animator _animator;
    //プレイヤーとのあたり判定フラグ
    bool hitFlg = false;

    void Update()
    {
        if (_player)
        {
            _navMeshAgent.SetDestination(_player.position);
        }
        _animator.SetFloat("Speed", _navMeshAgent.velocity.sqrMagnitude);
        if (!(_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack01")))
            hitFlg = false;
    }
    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("TriggerON");
            _animator.SetTrigger("PlayerHit");
            var player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack01"))
            {
                if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.4 &&
                    _animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 0.5)
                {
                    if (!hitFlg)
                    {
                        Debug.Log(gameObject.name + "から攻撃を受けました");
                        player.SetPlayerHPDamage(10);
                        hitFlg = true;
                    }
                }
            }
        }
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
        {
            _navMeshAgent.updatePosition = false;
            _navMeshAgent.updateRotation = false;
            _navMeshAgent.speed = 0.0f;
        }
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log(_animator.GetCurrentAnimatorClipInfo(0)[0].clip.name);
            Debug.Log("TriggerOFF");
            _animator.ResetTrigger("PlayerHit");
        }
        if (!(_animator.GetCurrentAnimatorStateInfo(0).IsName("Walk")))
        {
            _navMeshAgent.updatePosition = true;
            _navMeshAgent.updateRotation = true;
            _navMeshAgent.speed = 3.5f;
            Debug.Log("歩行開始");
        }
    }

    public NavMeshAgent GetNavMesh()
    {
        return _navMeshAgent;
    }
    public Animator GetAnimator()
    {
        return _animator;
    }
}
