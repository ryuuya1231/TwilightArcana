using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_LightningAura : MonoBehaviour
{
    [SerializeField] private ArcanaBase _arcana;
    [SerializeField] private Rigidbody _rb;
    Transform _player = null;
    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform.Find("male00");
        if (_player == null)
        {
            Debug.Log("プレイヤーがいません");
            return;
        }
    }
    private void Update()
    {
        if (_arcana.GetParticleSystem() == null) return;
        if (_arcana.GetParticleSystem().gameObject == null) return;
        if (_rb != null)
        {
            _rb.MovePosition(_player.gameObject.transform.position);
        }
        else Debug.Log("参照失敗");
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
            if (_arcana.GetVisualEffect() == null) return;
            if (_arcana.GetVisualEffect().gameObject == null) return;
            Destroy(_arcana.GetVisualEffect().gameObject);
        }
    }
}
