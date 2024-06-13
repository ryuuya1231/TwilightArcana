using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Arcana_TurretAction : ArcanaBase
{
    public override void ArcanaEffect()
    {
        _prefab = Resources.Load<GameObject>("TurretEffect/SelfSupportingTurret");
        _normalEffect = Instantiate(_prefab, _pos, Quaternion.identity);
        _normalEffect.gameObject.SetActive(true);
        Destroy(_normalEffect.gameObject, 10.0f);
    }
}
