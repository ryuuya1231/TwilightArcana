using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Arcana_MagicArrow : ArcanaBase
{
    public override void ArcanaEffect()
    {
        var _player = GameObject.FindGameObjectWithTag("Player");
        if (_player == null)
        {
            Debug.Log("�v���C���[�����܂���");
            return;
        }
        _prefab = (GameObject)Resources.Load("Arrow/Arrow");
        _effect = _prefab.GetComponent<VisualEffect>();
       // _pos = _player.transform.position;
        _shotEffect = Instantiate(_effect, _pos, Quaternion.identity);
        //_playerObject = _player.gameObject;
        //Debug.Log(_shotEffect.name + ":" + _pos + ":" + _effect.name + ":" + _playerObject.name);
        SetUp();
    }

    private void SetUp()
    {
        //var _player = GameObject.FindGameObjectWithTag("Player");
        //_rb = _prefab.GetComponent<Rigidbody>();
        //GameObject enemy = GameObject.FindWithTag("Enemy");
        //if (enemy)
        //{
        //    Debug.Log(enemy.name + "��T�m���܂���|Position:" + enemy.transform.position);
        //    Debug.Log(_shotEffect.transform.position);
        //    _shotEffect.transform.forward = enemy.transform.position - _player.transform.position;
        //    _shotEffect.transform.forward.Normalize();
        //    Debug.Log(_shotEffect.transform.forward);
        //}
        //else
        //{
        //    Debug.Log("�G��T�m�ł��܂���ł���");
        //    _shotEffect.transform.forward = _player.transform.forward;
        //}
        //_shotEffect.gameObject.SetActive(true);
        Destroy(_shotEffect.gameObject, 3.0f);
    }
}
