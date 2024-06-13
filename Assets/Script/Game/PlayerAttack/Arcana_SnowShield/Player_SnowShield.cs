using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_SnowShield : MonoBehaviour,IDamageable
{
    [SerializeField] private ArcanaBase _arcana;
    Transform player = null;
    //�@���݂̊p�x
    private float angle;
    //�@��]����X�s�[�h
    [SerializeField] private float rotateSpeed = 180f;
    //�@�^�[�Q�b�g����̋���
    [SerializeField] private Vector3 distanceFromTarget = new Vector3(0.0f, 0.0f, 2.5f);

    void IDamageable.Damage(int value)
    {
        
    }

    void IDamageable.Death()
    {
        
    }

    void IDamageable.Protect()
    {
        
    }

    private void Update()
    {
        if (_arcana.GetNormalEffect() == null) return;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //�@���j�b�g�̈ʒu = �^�[�Q�b�g�̈ʒu �{ �^�[�Q�b�g���猩�����j�b�g�̊p�x �~�@�^�[�Q�b�g����̋���
        transform.position = player.position + Quaternion.Euler(0f, angle, 0f) * distanceFromTarget;
        //�@���j�b�g���g�̊p�x = �^�[�Q�b�g���猩�����j�b�g�̕����̊p�x���v�Z����������j�b�g�̊p�x�ɐݒ肷��
        transform.rotation = Quaternion.LookRotation(transform.position - new Vector3(player.position.x, transform.position.y, player.position.z), Vector3.up);
        //�@���j�b�g�̊p�x��ύX
        angle += rotateSpeed * Time.deltaTime;
        //�@�p�x��0�`360�x�̊ԂŌJ��Ԃ�
        angle = Mathf.Repeat(angle, 360f);
    }
}
