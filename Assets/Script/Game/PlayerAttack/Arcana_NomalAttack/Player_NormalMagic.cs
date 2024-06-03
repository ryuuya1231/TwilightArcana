using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Player_NormalMagic : MonoBehaviour
{
    [SerializeField] private ArcanaBase _arcana;
    [SerializeField] private Rigidbody _rb;
    private void Update()
    {
        if (_arcana.GetVisualEffect() == null) return;
        if (_arcana.GetVisualEffect().gameObject == null) return;
        if (_rb != null)
        {
            _rb.velocity = _arcana.GetVisualEffect().gameObject.transform.forward.normalized * 10.0f;
        }
        else Debug.Log("éQè∆é∏îs");
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
                Debug.Log(enemy.name + "Ç∆è’ìÀ");
            }
            if (_arcana.GetVisualEffect() == null) return;
            if (_arcana.GetVisualEffect().gameObject == null) return;
            Destroy(_arcana.GetVisualEffect().gameObject);
        }
    }
}
