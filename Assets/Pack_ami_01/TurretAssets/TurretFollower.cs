using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretFollower : MonoBehaviour
{
    // �Ǐ]����^�[�Q�b�g�I�u�W�F�N�g
    public Transform turretTarget;
    // ����瓮������̐U��
    public float amplitude = 0.05f;
    // ����瓮������̑��x
    public float frequency = 1.0f;
    // �^�[�Q�b�g����̃I�t�Z�b�g
    public Vector3 turretOffset;
    // �x�����x
    public float delaySpeed = 5.0f;

    void Awake()
    {
        // turretTarget���q�G�����L�[���̓���̃I�u�W�F�N�g�ɐݒ�
        // turretTarget = GameObject.Find("Player").transform;
    }

    void Update()
    {
        if (turretTarget == null) return;

        // �T�C���g���g����Y���̈ʒu���v�Z
        float moveY = Mathf.Sin(Time.time * frequency) * amplitude;

        // �ڕW�ʒu���v�Z
        Vector3 targetPosition = new Vector3(turretTarget.position.x + turretOffset.x, turretTarget.position.y + turretOffset.y + moveY, turretTarget.position.z + turretOffset.z);

        // ���`��Ԃ��g���Ēx����ǉ�
        transform.position = Vector3.Lerp(transform.position, targetPosition, delaySpeed * Time.deltaTime);
    }
}