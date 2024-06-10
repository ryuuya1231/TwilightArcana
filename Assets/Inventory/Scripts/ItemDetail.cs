using FlMr_Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ItemDetail : ItemDetailBase
{
    [SerializeField] private TextMeshProUGUI itemName, description;
    //[SerializeField] private Image itemIcon;
    //[SerializeField] private Button buttonPrefab;
    //[SerializeField] private Transform buttonsTrn;

    protected internal override void OnClickCallback(ItemBag itemBag, ItemBase item, int number, GameObject slotObj)
    {
        itemName.text = item.ItemName;
        description.text = item.Description;
        //itemIcon.sprite = item.Icon;

        if (item is IUsable usable)
        {
            AddButton("Use", () =>
            {
                if (usable.Check())
                {
                    usable.Use();
                    itemBag.RemoveItem(item.UniqueId, 1);
                }
            });
        }

        if (item is IDeletable)
        {
            AddButton("Delete", () => itemBag.RemoveItem(item.UniqueId, 1));
        }
    }

    protected internal override void OnClickCallback_Box(ItemBox itemBox, ItemBase item, int number, GameObject slotObj)
    {
        itemName.text = item.ItemName;
        description.text = item.Description;
        //itemIcon.sprite = item.Icon;

        if (item is IUsable usable)
        {
            AddButton("Use", () =>
            {
                if (usable.Check())
                {
                    usable.Use();
                    itemBox.RemoveItem(item.UniqueId, 1);
                }
            });
        }

        if (item is IDeletable)
        {
            AddButton("Delete", () => itemBox.RemoveItem(item.UniqueId, 1));
        }
    }

    protected internal override void OnClickCallback_Select(SelectItemBox itemBox, ItemBase item, int number, GameObject slotObj)
    {
        itemName.text = item.ItemName;
        description.text = item.Description;
        //itemIcon.sprite = item.Icon;

        if (item is IUsable usable)
        {
            AddButton("Use", () =>
            {
                if (usable.Check())
                {
                    usable.Use();
                    itemBox.RemoveItem(item.UniqueId, 1);
                }
            });
        }

        if (item is IDeletable)
        {
            AddButton("Delete", () => itemBox.RemoveItem(item.UniqueId, 1));
        }
    }

    protected internal override void OnClickCallback_Game
            (GameSceneInventory itemGameBox, ItemBase item, int number, GameObject slotObj)
    {
        itemName.text = item.ItemName;
        description.text = item.Description;
        //itemIcon.sprite = item.Icon;

        if (item is IUsable usable)
        {
            AddButton("Use", () =>
            {
                if (usable.Check())
                {
                    usable.Use();
                    itemGameBox.RemoveItem(item.UniqueId, 1);
                }
            });
        }

        if (item is IDeletable)
        {
            AddButton("Delete", () => itemGameBox.RemoveItem(item.UniqueId, 1));
        }
    }

    private void AddButton(string buttonName, UnityAction function)
    {
        //var button = Instantiate(buttonPrefab, buttonsTrn);
       // button.onClick.AddListener(function);
       // button.GetComponentInChildren<TextMeshProUGUI>().text = buttonName;
    }


}