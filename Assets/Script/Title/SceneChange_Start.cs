using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange_Start : MonoBehaviour
{
    public void change_button()
    {
        SceneManager.LoadScene("GameScene");
    }
}
