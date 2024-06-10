using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Arcana_Slash : ArcanaBase
{
    Quaternion SlashRot;
    public override void ArcanaEffect()
    {
        _prefab = Resources.Load<GameObject>("Slash/Stone slash");
        _pos = GameObject.FindGameObjectWithTag("Player").transform.position + new Vector3(0.0f, 2.5f, 0.0f);
        Quaternion PlayerRot = GameObject.FindGameObjectWithTag("Player").transform.rotation;


        Quaternion additionalRotation = Quaternion.Euler(0, 0, -35);
        SlashRot = PlayerRot * additionalRotation;
        _normalEffect = Instantiate(_prefab, _pos, SlashRot);
        //_normalEffect.gameObject.SetActive(true);
        Destroy(_normalEffect.gameObject, 1.0f);

        Quaternion additionalRotation1 = Quaternion.Euler(0, 0, 35);
        SlashRot = PlayerRot * additionalRotation1;
        _normalEffect = Instantiate(_prefab, _pos, SlashRot);
        //_normalEffect.gameObject.SetActive(true);
        Destroy(_normalEffect.gameObject, 1.0f);
    }
}
