using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public AudioSource audioSourceBGM;
    public AudioClip[] audioClipsBGM;

    public void SetBgmVolume(float bgmVolume)
    {
        audioSourceBGM.volume = bgmVolume;
    }

    public float GetBgmVolume()
    { return audioSourceBGM.volume; }

    public void SetPlayBGM(int bgmNumber)
    {
        audioSourceBGM.PlayOneShot(audioClipsBGM[bgmNumber]);
    }
}
