using FlMr_Inventory;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NotResetInventory : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Boss")
        {
            Canvas canvas = GetComponent<Canvas>();
            canvas.enabled = false;
        }
        else
        {
            Canvas canvas = GetComponent<Canvas>();
            canvas.enabled = true;
        }
    }
}

//public class EventSystemDestroyer
//{
//    [InitializeOnLoadMethod]
//    private static void Initialize()
//    {
//        EditorApplication.hierarchyChanged += () =>
//        {
//            var activeScene = SceneManager.GetActiveScene();
//            var go = activeScene.GetRootGameObjects().LastOrDefault();
//            if (go == null || go.name != "EventSystem") return;
//
//            GameObject.DestroyImmediate(go);
//            Debug.unityLogger.LogWarning("GameObject with EventSystem in root was destroyed", activeScene.name);
//        };
//    }
//}