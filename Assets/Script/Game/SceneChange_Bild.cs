using FlMr_Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange_Bild : MonoBehaviour
{
    public void OnClickEvent()
    {
        Debug.Log("�r���h�V�[���ֈړ�");
        SceneManager.LoadScene("BildScene");
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            Debug.Log("�r���h�V�[���ֈړ�");
            SceneManager.LoadScene("BildScene");
        }
    }
}
