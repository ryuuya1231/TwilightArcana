using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderKeyOperation : MonoBehaviour
{
    [SerializeField] public Slider bgmSlider;
    private float count;
    private float timeReset = 0.1f;
    private float time;

    private void Start()
    {
        BgmLoadSlider();
    }

    void Update()
    {
        time += Time.deltaTime;

        if (time > timeReset)
        {
            if (Input.GetKey(KeyCode.RightArrow) && count < 1)
            {
                count += 0.1f;
                time = 0;
            }

            if (Input.GetKey(KeyCode.LeftArrow) && count > 0)
            {
                count -= 0.1f;
                time = 0;
            }
        }
        bgmSlider.value = count;
        BgmVolume();

    }

    public void BgmVolume()
    {
        float a = bgmSlider.value;
        SoundManager.instance.SetBgmVolume(a);
        BgmSave();
    }

    public void BgmSave()
    {
        PlayerPrefs.SetFloat("SoundVolume", bgmSlider.value);
    }

    public void BgmLoadSlider()
    {
        //bgmSlider.value = PlayerPrefs.GetFloat("SoundVolume", 1.0f);
        count = PlayerPrefs.GetFloat("SoundVolume", 1.0f);
        float a = bgmSlider.value;
        SoundManager.instance.SetBgmVolume(a);
        print(a);
    }
}
