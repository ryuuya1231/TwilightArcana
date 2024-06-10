using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange_Sound : MonoBehaviour
{
    public void change_button()
    {
        SceneManager.LoadScene("SoundSettingScene");
    }
}
