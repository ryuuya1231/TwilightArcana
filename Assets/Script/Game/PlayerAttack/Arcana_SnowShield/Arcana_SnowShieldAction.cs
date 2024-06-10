using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arcana_SnowShieldAction : ArcanaBase
{
    public override void ArcanaEffect()
    {
        _prefab = Resources.Load<GameObject>("SnowShieldEffect/SnowShield");
        _normalEffect = Instantiate(_prefab, _pos, Quaternion.identity);
        _normalEffect.gameObject.SetActive(true);
        Destroy(_normalEffect.gameObject, 2.0f);
    }
}
