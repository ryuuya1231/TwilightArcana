using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCamera : MonoBehaviour
{
    private GameObject player = null;
    private Vector3 offset;
    void Start()
    {
        player = GameObject.Find("male00");
        offset = transform.position - player.transform.position;
    }

    void Update()
    {
        if (player)
        {
            transform.position = player.transform.position + offset;
        }
        else Debug.Log("ƒvƒŒƒCƒ„[‚ªŒ©‚Â‚©‚è‚Ü‚¹‚ñ");
    }
}
