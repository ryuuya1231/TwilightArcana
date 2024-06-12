using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange_Exit : MonoBehaviour
{
    public void change_button()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // ゲームプレイ終了
#else
        Application.Quit(); // ゲームプレイ終了
#endif
    }
}
