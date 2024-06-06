using FlMr_Inventory;
using UnityEngine;

//public class GameSceneInventoryAwake : MonoBehaviour
//{

//    // Start is called before the first frame update
//    void Start()
//    {
//        // ˆê“x‚¾‚¯Às‚µ‚½‚¢ˆ—
//        /* ‰Šú‰»ˆ— */
//        var gameSceneInventory = GameObject.FindGameObjectWithTag("GameSceneArcanaInventory").GetComponent<GameSceneInventory>();
//        var itemBag = GameObject.FindGameObjectWithTag("ItemBag").GetComponent<ItemBag>();
//        for (int i = 0; i < itemBag.GetAllItems().Count; ++i)
//        {
//            gameSceneInventory.AddItem(itemBag.GetItemData(i), 1);
//        }
//    }
//}
using FlMr_Inventory;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneInventoryAwake : MonoBehaviour
{
    private static bool isInitialized = false;
    [SerializeField] private Camera _camera;
    void Start()
    {
        _camera = Camera.main;
        if (!isInitialized)
        {
            isInitialized = true;
            SceneManager.LoadScene("DontDestroyOnLoadObjectScene");
            Debug.Log("DontDestroyOnLoadObjectScene‚É“ü‚è‚Ü‚µ‚½");
        }
        else
        {
            var gameSceneInventory = GameObject.FindGameObjectWithTag("GameSceneArcanaInventory").GetComponent<GameSceneInventory>();
            var itemBag = GameObject.FindGameObjectWithTag("ItemBag").GetComponent<ItemBag>();
            for (int i = 0; i < itemBag.GetAllItems().Count; ++i)
            {
                gameSceneInventory.AddItem(itemBag.GetItemData(i), 1);
                Debug.Log(gameSceneInventory.GetItem(i).name);
            }
            if (_camera)
            {
                Debug.Log("_camera");
                _camera.gameObject.SetActive(false);
                _camera.gameObject.SetActive(true);
            }
        }
    }
}
