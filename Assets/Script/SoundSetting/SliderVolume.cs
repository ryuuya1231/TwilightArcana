using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderVolume : MonoBehaviour
{
    public void SetBGMVolume(float volume)
    {
        SoundManager.instance.SetBgmVolume(volume);
    }
}
