using FlMr_Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Arcana_Enter : ArcanaBase
{
    public override void ArcanaEffect()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        GameObject shotObject = (GameObject)Resources.Load("MagicPrefabs/meteor");
        Vector3 pos = player.transform.position;
        GameObject shot = Instantiate(shotObject, pos, Quaternion.identity);
        GameObject golemGameObj = player.gameObject;
        shot.GetComponent<Player_MagicShot>().SetObject(golemGameObj, shot);
        Debug.Log(shotObject.name + ":" + pos + ":" + shot.name + ":" + golemGameObj.name);
    }
}
