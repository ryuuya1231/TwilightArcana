using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowEffectFollower : MonoBehaviour
{
    // �Ǐ]����^�[�Q�b�g�I�u�W�F�N�g
    private Transform snowEffectTarget;

    void Awake()
    {
        // �q�G�����L�[���̓���̃I�u�W�F�N�g��ݒ�
        snowEffectTarget = GameObject.Find("Player").transform.Find("male00"); ;
    }

    void Update()
    {
        if (snowEffectTarget == null) return;

        // ���݂�Y���̈ʒu��ێ�
        float currentY = transform.position.y;

        // �^�[�Q�b�g��X����Z���̈ʒu��Ǐ]���AY���͌��݂̈ʒu���ێ�
        transform.position = new Vector3(snowEffectTarget.position.x, currentY, snowEffectTarget.position.z);
    }
}