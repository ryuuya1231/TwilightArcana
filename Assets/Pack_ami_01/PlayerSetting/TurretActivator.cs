using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretActivator : MonoBehaviour
{
    // �o��������^���b�g�I�u�W�F�N�g�i�q�G�����L�[���j
    public GameObject turret;
    // �g�O���L�[�i��FE�L�[�j
    public KeyCode toggleKey = KeyCode.E;

    void Awake()
    {
        // �^���b�g���q�G�����L�[���̓���̃I�u�W�F�N�g�ɐݒ�
        GameObject turretObject = GameObject.Find("SelfSupportingTurret");
        if (turretObject != null)
        {
            turret = turretObject;
            // �Q�[���N�����ɔ�A�N�e�B�u�ɐݒ�
            turret.SetActive(false);
        }
        else
        {
            Debug.LogError("SelfSupportingTurret �I�u�W�F�N�g��������܂���");
        }
    }

    void Update()
    {
        if (turret == null) return;

        // �g�O���L�[�������ꂽ��
        if (Input.GetKeyDown(toggleKey))
        {
            // �^���b�g���A�N�e�B�u���ǂ������`�F�b�N���āA�g�O��
            if (!turret.activeSelf)
            {
                turret.SetActive(true);
                // `AliveTime`�X�N���v�g���^���b�g�̐������Ԃ��Ǘ�����̂ŁA�����ł͉������Ȃ�
            }
        }
    }
}