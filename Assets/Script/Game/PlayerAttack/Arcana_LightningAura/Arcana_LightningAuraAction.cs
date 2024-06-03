using FlMr_Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Arcana_LightningAuraAction : ArcanaBase
{
    public override void ArcanaEffect()
    {
        var _player = GameObject.FindGameObjectWithTag("Player").transform.Find("male00");
        if (_player == null)
        {
            Debug.Log("プレイヤーがいません");
            return;
        }
        _prefab = Resources.Load<GameObject>("AuraEffect/LightningAura");
        ParticleSystem _effect = _prefab.GetComponent<ParticleSystem>();
        _pos = _player.transform.position;
        _particleEffect = Instantiate(_effect, _pos, Quaternion.identity);
        _playerObject = _player.gameObject;
        _rb = _prefab.GetComponent<Rigidbody>();
        GameObject enemy = GameObject.FindWithTag("Enemy");
        if (enemy)
        {
            _particleEffect.transform.forward = enemy.transform.position - _player.transform.position;
            _particleEffect.transform.forward.Normalize();
        }
        else
        {
            Debug.Log("敵を探知できませんでした");
            _particleEffect.transform.forward = _player.transform.forward;
        }
        _particleEffect.gameObject.SetActive(true);
        Destroy(_particleEffect.gameObject, 5.0f);
    }
}
