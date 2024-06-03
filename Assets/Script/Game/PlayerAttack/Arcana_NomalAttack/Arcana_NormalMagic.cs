using FlMr_Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Arcana_NormalMagic : ArcanaBase
{
    public override void ArcanaEffect()
    {
        var _player = GameObject.FindGameObjectWithTag("Player").transform.Find("male00");
        if (_player == null)
        {
            Debug.Log("ÉvÉåÉCÉÑÅ[Ç™Ç¢Ç‹ÇπÇÒ");
            return;
        }
        _prefab = Resources.Load<GameObject>("NormalMagicPrefab/ArcanaEffect_NormalMagic");
        Debug.Log(_prefab.name);
        _effect = _prefab.GetComponent<VisualEffect>();
        _pos = _player.transform.position;
        _shotEffect = Instantiate(_effect, _pos, Quaternion.identity);
        _playerObject = _player.gameObject;
        Debug.Log(_shotEffect.name + ":" + _pos + ":" + _effect.name + ":" + _playerObject.name);
        SetUp();
    }

    private void SetUp()
    {
        var _player = GameObject.FindGameObjectWithTag("Player").transform.Find("male00");
        _rb = _prefab.GetComponent<Rigidbody>();
        GameObject enemy = GameObject.FindWithTag("Enemy");
        if (enemy)
        {
            Debug.Log(enemy.name + "ÇíTímÇµÇ‹ÇµÇΩ|Position:" + enemy.transform.position);
            Debug.Log(_shotEffect.transform.position);
            _shotEffect.transform.forward = enemy.transform.position - _player.transform.position;
            _shotEffect.transform.forward.Normalize();
            Debug.Log(_shotEffect.transform.forward);
        }
        else
        {
            Debug.Log("ìGÇíTímÇ≈Ç´Ç‹ÇπÇÒÇ≈ÇµÇΩ");
            _shotEffect.transform.forward = _player.transform.forward;
        }
        _shotEffect.gameObject.SetActive(true);
        Destroy(_shotEffect.gameObject, 5.0f);
    }
}
