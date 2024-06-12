using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Player_GreenSemicircle : MonoBehaviour
{

    [SerializeField] VisualEffect effect;
    // Start is called before the first frame update
    void Start()
    {
        //effect=GetComponent<VisualEffect>();
        Debug.Log("GreenSemicircle");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("!Enemy!Hit");
        }
    }
}
