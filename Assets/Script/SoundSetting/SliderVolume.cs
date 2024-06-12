using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderVolume : MonoBehaviour
{
    public void SetBGMVolume(float volume)
    {
        SoundManager.instance.SetBgmVolume(volume);
    }

    private void Start()
    {
        gameObject.GetComponent<Slider>().value = SoundManager.instance.GetBgmVolume();
    }
}
