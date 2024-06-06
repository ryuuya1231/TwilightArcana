using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Arcana_GreenSemicircle : ArcanaBase
{
    Quaternion SlashRot;
    private void Start()
    {
        

        Quaternion additionalRotation = Quaternion.Euler(180, 0, 0);
        Quaternion PlayerRot = GameObject.FindGameObjectWithTag("Player").transform.rotation;
        SlashRot = PlayerRot * additionalRotation;
    }
    public override void ArcanaEffect()
    {
        _prefab = Resources.Load<GameObject>("GreenSemicircle/SwordEffect");
        _pos = GameObject.FindGameObjectWithTag("Player").transform.position + new Vector3(0.0f, 2.5f, 0.0f);
        _normalEffect = Instantiate(_prefab, _pos, SlashRot);
        Destroy(_normalEffect.gameObject, 1.0f);
    }
}
