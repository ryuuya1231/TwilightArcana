using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrans : MonoBehaviour
{
    [SerializeField] Transform _player;
    //public GameObject Target;
    // Start is called before the first frame update
    void Start()
    {
        _player=
        _player = GameObject.FindGameObjectWithTag("Player").transform.Find("Gaze");
        CinemachineVirtualCamera virtualCamera = GetComponent<CinemachineVirtualCamera>();
        virtualCamera.Follow=_player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
