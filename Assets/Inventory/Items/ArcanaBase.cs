using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.VFX;

public class ArcanaBase : MonoBehaviour
{
    protected PlayerAttacker _player = null;
    protected GameObject _prefab = null;
    protected VisualEffect _effect = null;
    protected ParticleSystem _particle = null;
    protected Vector3 _pos;
    protected VisualEffect _shotEffect = null;
    protected ParticleSystem _particleEffect = null;
    protected GameObject _normalEffect = null;
    protected GameObject _playerObject = null;
    protected Rigidbody _rb = null;
    protected float _speed = 10.0f;
    public virtual void ArcanaEffect()
    {

    }

    public VisualEffect GetVisualEffect()
    {
        return _shotEffect;
    }

    public ParticleSystem GetParticleSystem()
    {
        return _particleEffect;
    }

    public GameObject GetNormalEffect()
    {
        return _normalEffect;
    }

    public float GetSpeed()
    {
        return _speed;
    }
}
