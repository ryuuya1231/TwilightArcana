using FlMr_Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange_Bild : MonoBehaviour
{
    public void OnClickEvent()
    {
        Debug.Log("ビルドシーンへ移動");
        SceneManager.LoadScene("BildScene");
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            Debug.Log("ビルドシーンへ移動");
            SceneManager.LoadScene("BildScene");
        }
    }
}
