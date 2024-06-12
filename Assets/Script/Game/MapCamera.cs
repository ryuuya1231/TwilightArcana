using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCamera : MonoBehaviour
{
    private GameObject player = null;
    private Vector3 offset;
    void Start()
    {
        //player = GameObject.Find("male00");
        player= GameObject.FindGameObjectWithTag("Player");
        offset = transform.position - player.transform.position;
    }

    void Update()
    {
        if (player)
        {
            transform.position = player.transform.position + offset;
        }
        else Debug.Log("ÉvÉåÉCÉÑÅ[Ç™å©Ç¬Ç©ÇËÇ‹ÇπÇÒ");
    }
}
