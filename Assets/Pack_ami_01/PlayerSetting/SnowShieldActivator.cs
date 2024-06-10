using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowShieldActivator : MonoBehaviour
{
    // �o��������V�[���h�I�u�W�F�N�g�i�q�G�����L�[���j
    public GameObject shield;
    public GameObject snowEffect;
    // �g�O���L�[�i��F�X�y�[�X�L�[�j
    public KeyCode toggleKey = KeyCode.Space;

    void Awake()
    {
        // secondShield���q�G�����L�[���̓���̃I�u�W�F�N�g�ɐݒ肵�A��A�N�e�B�u�ɂ���
        GameObject secondShieldObject = GameObject.Find("Shield_02");
        if (secondShieldObject != null)
        {
            secondShieldObject.SetActive(false);
        }
        else
        {
            Debug.LogError("Shield_02 �I�u�W�F�N�g��������܂���");
        }

        // shield���q�G�����L�[���̓���̃I�u�W�F�N�g�ɐݒ�
        GameObject shieldObject = GameObject.Find("Shield_01");
        if (shieldObject != null)
        {
            shield = shieldObject;
            // �Q�[���N�����ɔ�A�N�e�B�u�ɐݒ�
            shield.SetActive(false);
        }
        else
        {
            Debug.LogError("Shield_01 �I�u�W�F�N�g��������܂���");
        }

        // snowEffect���q�G�����L�[���̓���̃I�u�W�F�N�g�ɐݒ�
        GameObject snow = GameObject.Find("Effect_Snow");
        if (snow != null)
        {
            snowEffect = snow;
            // �Q�[���N�����ɔ�A�N�e�B�u�ɐݒ�
            snowEffect.SetActive(false);
        }
        else
        {
            Debug.LogError("Effect_Snow �I�u�W�F�N�g��������܂���");
        }

    }

    void Update()
    {
        if (shield == null || snowEffect == null) return;

        // �g�O���L�[�������ꂽ��
        if (Input.GetKeyDown(toggleKey))
        {
            // shield��snowEffect����A�N�e�B�u�ȏꍇ�A�A�N�e�B�u�ɂ���
            if (!shield.activeSelf && !snowEffect.activeSelf)
            {
                shield.SetActive(true);
                snowEffect.SetActive(true);

                // ShieldRotation�X�N���v�g���A�^�b�`����Ă���ꍇ�̏���
                ShieldRotation shieldRotationScript = shield.GetComponent<ShieldRotation>();
                //if (shieldRotationScript != null && shieldRotationScript.secondaryShield != null)
                {
                  //  shieldRotationScript.secondaryShield.gameObject.SetActive(true);
                }
            }
        }
    }
}