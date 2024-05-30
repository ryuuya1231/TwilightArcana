using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Awake()
    {
        TryGetComponent(out animator);
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag=="Attack")
        {
            Debug.Log("Hit");
           // Debug.Log("Hit");
        }
    }

    public void Damege()
    {
        animator.SetTrigger("Damage");
    }
}
