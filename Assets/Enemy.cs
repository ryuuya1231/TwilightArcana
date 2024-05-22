using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    Animator animator;
    [SerializeField] private Charadata data;
    int HP;
    // Start is called before the first frame update
    void Start()
    {
        if(data!=null)
        {
            HP = data.MAXHP;
        }
    }
    void Awake()
    {
        TryGetComponent(out animator);
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag=="Attack")
    //    {
    //        Debug.Log("!Attack!Hit");
    //       // Debug.Log("Hit");
    //    }
    //}

    public void Damage(int value)
    {
        animator.SetTrigger("Damage");
        if(data!=null)
        {
            HP -= value - data.DEF;
        }
        if(HP<=0)
        {
            Death();
        }
    }
    public void Death()
    {
        animator.SetTrigger("Death");
        
    }
    void Extinction()
    {
        Destroy(gameObject);
    }
}
