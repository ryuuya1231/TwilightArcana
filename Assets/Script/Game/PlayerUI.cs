using FlMr_Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private Image PlayerHPBar;
    // Start is called before the first frame update
    void Start()
    {
        var player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (player)
        {
            Debug.Log("�v���C���[�ƌq����܂���");
            Debug.Log(player.GetPlayerHP());
        }
        else Debug.Log("�v���C���[�ƌq����܂���ł���");
    }

    // Update is called once per frame
    void Update()
    {
        var player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (player)
        {
            if (PlayerHPBar)
            {
                PlayerHPBar.rectTransform.sizeDelta = new Vector2(player.GetPlayerHP(), PlayerHPBar.rectTransform.rect.height);
            }
        }
    }
}
