using FlMr_Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotArcanaEffect : MonoBehaviour
{
    bool pushFlg = false;
    void Update()
    {
        var arcanaBag = GameObject.FindGameObjectWithTag("GameSceneArcanaInventory").GetComponent<GameSceneInventory>();
        if (arcanaBag.GetItem(0)) arcanaBag.GetItem(0).GetCoolTime();
        if (arcanaBag.GetItem(1)) arcanaBag.GetItem(1).GetCoolTime();
        if (arcanaBag.GetItem(2)) arcanaBag.GetItem(2).GetCoolTime();
        if (arcanaBag.GetItem(3)) arcanaBag.GetItem(3).GetCoolTime();
        if (Input.GetKeyDown(KeyCode.Mouse0) && !pushFlg)
        {
            pushFlg = true;
            if (arcanaBag.GetItem(0) == null) return;
            if (!arcanaBag.GetItem(0).GetActiveFlg()) return;
            if (arcanaBag.GetItem(0).GetArcana() == null) return;
            arcanaBag.GetItem(0).GetArcana().ArcanaEffect();
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1) && !pushFlg)
        {
            pushFlg = true;
            if (arcanaBag.GetItem(1) == null) return;
            if (!arcanaBag.GetItem(1).GetActiveFlg()) return;
            if (arcanaBag.GetItem(1).GetArcana() == null) return;
            arcanaBag.GetItem(1).GetArcana().ArcanaEffect();
        }
        else if (Input.GetKeyDown(KeyCode.Q) && !pushFlg)
        {
            pushFlg = true;
            if (arcanaBag.GetItem(2) == null) return;
            if (!arcanaBag.GetItem(2).GetActiveFlg()) return;
            if (arcanaBag.GetItem(2).GetArcana() == null) return;
            arcanaBag.GetItem(2).GetArcana().ArcanaEffect();
        }
        else if (Input.GetKeyDown(KeyCode.E) && !pushFlg)
        {
            pushFlg = true;
            if (arcanaBag.GetItem(3) == null) return;
            if (!arcanaBag.GetItem(3).GetActiveFlg()) return;
            if (arcanaBag.GetItem(3).GetArcana() == null) return;
            arcanaBag.GetItem(3).GetArcana().ArcanaEffect();
        }
        else if (Input.GetKeyUp(KeyCode.Q) || Input.GetKeyUp(KeyCode.E) || Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Mouse1) && pushFlg)
        {
            pushFlg = false;
        }
    }
}
