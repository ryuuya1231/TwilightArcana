using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Slash : MonoBehaviour
{
    [SerializeField] GameObject a;
    [SerializeField] ParticleSystem particle;
    GameObject player;
    Vector3 slashPos;
    Quaternion SlashRot;
    // [SerializeField] Transform player;
    private void Start()
    {
        particle=GetComponent<ParticleSystem>();
    }
    private void Update()
    {
        //slashPos= GameObject.FindGameObjectWithTag("Player").transform.position+new Vector3(0,2.5f,0);
        //Quaternion PlayerRot = GameObject.FindGameObjectWithTag("Player").transform.rotation;
        //Quaternion additionalRotation = Quaternion.Euler(-30, -45, 0);
        //SlashRot = PlayerRot * additionalRotation;
        //particle.transform.position= slashPos;
        //particle.transform.rotation = SlashRot;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("!Enemy!Hit");
        }
    }
}
