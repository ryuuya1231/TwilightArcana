using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.VFX;

public class Player_ElectricitySword : MonoBehaviour
{
    [SerializeField] private ArcanaBase _arcana;
    private int effectCount = 0;
    Transform player = null;
    Transform enemy = null;
    //�@���݂̊p�x
    private float angle;
    //�@��]����X�s�[�h
    [SerializeField] private float rotateSpeed = 180f;
    //�@�^�[�Q�b�g����̋���
    [SerializeField] private Vector3 distanceFromTarget = new Vector3(0f, 1f, 2f);

    private Vector3 lockonRotate;

    //���ꂼ��̈ʒu��ۑ�����ϐ�
    //���p�n�_
    private Vector3 greenPos;
    private Vector3 startPos;
    private Vector3 goalPos;
    //�i�ފ������Ǘ�����ϐ�
    float time;

    private void Update()
    {
        if (_arcana.gameObject == null) return;
        if (effectCount < 500)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform.Find("male00");
            enemy = GameObject.FindGameObjectWithTag("Enemy").transform;
            //�@���j�b�g�̈ʒu = �^�[�Q�b�g�̈ʒu �{ �^�[�Q�b�g���猩�����j�b�g�̊p�x �~�@�^�[�Q�b�g����̋���
            transform.position = player.position + Quaternion.Euler(0f, angle, 0f) * distanceFromTarget;
            //�@���j�b�g���g�̊p�x = �^�[�Q�b�g���猩�����j�b�g�̕����̊p�x���v�Z����������j�b�g�̊p�x�ɐݒ肷��
            transform.rotation = Quaternion.LookRotation(transform.position - new Vector3(player.position.x, transform.position.y, player.position.z), Vector3.up);
            //�@���j�b�g�̊p�x��ύX
            angle += rotateSpeed * Time.deltaTime;
            //�@�p�x��0�`360�x�̊ԂŌJ��Ԃ�
            angle = Mathf.Repeat(angle, 360f);
            ++effectCount;
            //else if (effectCount < 510)
            //{
            //    //++effectCount;
            //    //greenPos.Set(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
            //    //if (enemy)
            //    //{
            //    //    goalPos = enemy.gameObject.transform.position;
            //    //}
            //    //startPos = gameObject.transform.position;
            //}
        }
        else
        {
            ////�e�̐i�ފ�����Time.deltaTime�Ō��߂�
            //time += Time.deltaTime;
            ////�񎟃x�W�F�Ȑ�
            ////�X�^�[�g�n�_���璆�p�n�_�܂ł̃x�N�g�����ʂ�_�̌��݂̈ʒu
            //var a = Vector3.Lerp(startPos, greenPos, time);
            ////���p�n�_����^�[�Q�b�g�܂ł̃x�N�g�����ʂ�_�̌��݂̈ʒu
            //var b = Vector3.Lerp(greenPos, goalPos, time);
            ////��̓�̓_�����񂾃x�N�g�����ʂ�_�̌��݂̈ʒu�i�e�̈ʒu�j
            //gameObject.transform.position = Vector3.Lerp(a, b, time);
            //gameObject.transform.Rotate(gameObject.transform.forward);
            lockonRotate = enemy.transform.position - gameObject.transform.position;
            gameObject.transform.eulerAngles = lockonRotate;
            //Debug.Log();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            var enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<GolemController>();
            if (enemy)
            {
                enemy.GetAnimator().SetTrigger("Damage");
                enemy.GetNavMesh().updatePosition = false;
                enemy.GetNavMesh().updateRotation = false;
                enemy.GetNavMesh().speed = 0.0f;
                Debug.Log(enemy.name + "�ƏՓ�");
            }
            if (_arcana.GetNormalEffect() == null) return;
            Destroy(_arcana.GetNormalEffect());
        }
    }
}
