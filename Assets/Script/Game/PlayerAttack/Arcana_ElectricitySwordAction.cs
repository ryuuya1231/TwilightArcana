using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Arcana_ElectricitySwordAction : ArcanaBase
{
    public override void ArcanaEffect()
    {
        var _player = GameObject.FindGameObjectWithTag("Player").transform.Find("male00");
        if (_player == null)
        {
            Debug.Log("プレイヤーがいません");
            return;
        }
        _prefab = Resources.Load<GameObject>("ElectricitySwordEffect/ElectricitySword");
        _pos = _player.transform.position;
        _normalEffect = Instantiate(_prefab, _pos, Quaternion.identity);
        _playerObject = _player.gameObject;
        _rb = _prefab.GetComponent<Rigidbody>();
        GameObject enemy = GameObject.FindWithTag("Enemy");
        if (enemy)
        {
            _normalEffect.transform.forward = enemy.transform.position - _player.transform.position;
            _normalEffect.transform.forward.Normalize();
            Debug.Log(_normalEffect.transform.forward);
        }
        else
        {
            Debug.Log("敵を探知できませんでした");
            _normalEffect.transform.forward = _player.transform.forward;
        }
        _normalEffect.gameObject.SetActive(true);
        Destroy(_normalEffect.gameObject, 10.0f);
    }
}
