using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldRotation : MonoBehaviour
{
    // �w�肵���Ώۂ𒆐S�ɉ~��`���悤�ȓ���������X�N���v�g
    // ���S�_
    private Transform shieldTarget;
    // �Ώۂ̃I�u�W�F�N�g
    private Transform secondaryShield;
    // ���a
    public float shieldRadius = 0.8f;
    // ��]���x
    public float shieldSpeed = 300.0f;
    // ��]���i��FVector3.up��Y������̉�]���Ӗ����܂��j
    public Vector3 shieldRotationAxis = Vector3.up;
    // ���S�Ɍ����郍�[�J�����i��FVector3.forward��Z���𒆐S�Ɍ����邱�Ƃ��Ӗ����܂��j
    public Vector3 shieldLocalForward = Vector3.down;

    // �����ϐ�
    private float shieldAngle;

    void Awake()
    {
        // secondaryShield���q�G�����L�[���̓���̃I�u�W�F�N�g�ɐݒ�
        secondaryShield = GameObject.Find("Shield_02").transform;
    }

    void Update()
    {
        shieldTarget = GameObject.FindGameObjectWithTag("Player").transform.Find("male00");
        if (secondaryShield == null || shieldTarget == null) return; // secondaryShield�܂���shieldTarget���ݒ肳��Ă��Ȃ��ꍇ�͉������Ȃ�

        // ��]���x�ɉ����Ċp�x���X�V
        shieldAngle += shieldSpeed * Time.deltaTime;

        //// ��]���ɉ������V�����ʒu���v�Z
        //Vector3 offset = Quaternion.AngleAxis(shieldAngle, shieldRotationAxis) * new Vector3(shieldRadius, 0, 0);
        //transform.position = shieldTarget.position + offset;

        //// �I�u�W�F�N�g�̌����𒆐S�_�Ɍ�����
        //Vector3 direction = transform.position - shieldTarget.position;
        //Quaternion lookRotation = Quaternion.LookRotation(direction, shieldRotationAxis);

        //// ���[�J�������l�����ĉ�]�𒲐�
        //transform.rotation = lookRotation * Quaternion.FromToRotation(shieldLocalForward, Vector3.forward);

        // �^���΂̈ʒu���v�Z���Ĕz�u
        if (secondaryShield != null)
        {
            Vector3 oppositeOffset = Quaternion.AngleAxis(shieldAngle + 180, shieldRotationAxis) * new Vector3(shieldRadius, 0, 0);
            secondaryShield.position = shieldTarget.position + oppositeOffset;

            // ���Α��̃I�u�W�F�N�g�̌����𒆐S�_�Ɍ�����
            Vector3 oppositeDirection = secondaryShield.position - shieldTarget.position;
            Quaternion oppositeLookRotation = Quaternion.LookRotation(oppositeDirection, shieldRotationAxis);

            // ���[�J�������l�����ĉ�]�𒲐�
            secondaryShield.rotation = oppositeLookRotation * Quaternion.FromToRotation(shieldLocalForward, Vector3.forward);
        }
        Debug.Log(shieldTarget.transform.position);
        Debug.Log(gameObject.transform.position);
        Debug.Log(secondaryShield.transform.position);
    }

    void OnDisable()
    {
        // shieldTarget����A�N�e�B�u�ɂȂ�Ƃ��AsecondaryShield����������A�N�e�B�u�ɂ���
        if (secondaryShield != null)
        {
            secondaryShield.gameObject.SetActive(false);
        }
    }
}