using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange_Title : MonoBehaviour
{
    public void change_button()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
