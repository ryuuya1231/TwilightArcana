using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentSceneSound : MonoBehaviour
{
    [SerializeField] private int soundNumber = 0;
    void Start()
    {
        if (!SoundManager.instance) return;
        SoundManager.instance.audioSourceBGM.Stop();
        SoundManager.instance.SetPlayBGM(soundNumber);
    }
}
