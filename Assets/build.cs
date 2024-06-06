using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class build : MonoBehaviour
{
    // Start is called before the first frame update
    private static bool isInitialized = false;
    void Start()
    {
        if (!isInitialized)
        {
            isInitialized = true;
            //SceneManager.LoadScene("DontDestroyOnLoadObjectScene");
            //Debug.Log("DontDestroyOnLoadObjectScene‚É“ü‚è‚Ü‚µ‚½");
        }
    }
    private void Update()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
