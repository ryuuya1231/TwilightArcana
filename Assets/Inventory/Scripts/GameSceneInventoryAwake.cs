using FlMr_Inventory;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneInventoryAwake : MonoBehaviour
{
    private Camera _camera;
    void Start()
    {
        _camera = Camera.main;

        if (GameObject.FindGameObjectWithTag("ItemBag"))
        {
            var itemBag = GameObject.FindGameObjectWithTag("ItemBag").GetComponent<ItemBag>();
            var gameSceneInventory = GameObject.FindGameObjectWithTag("GameSceneArcanaInventory").GetComponent<GameSceneInventory>();
            for (int i = 0; i < itemBag.GetAllItems().Count; ++i)
            {
                gameSceneInventory.AddItem(itemBag.GetItemData(i), 1);
                Debug.Log(gameSceneInventory.GetItem(i).name);
            }
        }
        if (_camera)
        {
            Debug.Log("_camera");
            _camera.gameObject.SetActive(false);
            _camera.gameObject.SetActive(true);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
