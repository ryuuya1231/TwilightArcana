using FlMr_Inventory;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InventoryTest : MonoBehaviour
{
    private static bool isInitialized = false;
    private void Start()
    {
        if (!isInitialized)
        {
            // ��x�������s����������
            isInitialized = true;
            /* ���������� */
            var bag = GameObject.FindGameObjectWithTag("ItemBag").GetComponent<ItemBag>();
            var box = GameObject.FindGameObjectWithTag("ItemBox").GetComponent<ItemBox>();
            Debug.Log("ItemUtility�ɂ͌���" + ItemUtility.Instance.AllItems.Count+"��̃A�C�e�����o�^����Ă��܂�");
            for (int i = 1; i <= ItemUtility.Instance.AllItems.Count; ++i)
            {
                if (!bag.AddItem(i, 1))
                {
                    box.AddItem(i, 1);
                }
            }
            //bag.AddItem(6, 1);
            //bag.AddItem(7, 1);
        }
    }
}