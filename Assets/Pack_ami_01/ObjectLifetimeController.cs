using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectLifetimeController : MonoBehaviour
{
    // �o�����Ă����A�N�e�B�u�ɂ���܂ł̎��ԁi�b�j
    public float lifetimeDuration = 10f;
    private Coroutine deactivateCoroutine;

    void OnEnable()
    {
        Debug.Log("OnEnable called for " + gameObject.name);

        // �R���[�`�������Ɏ��s���̏ꍇ�͒�~����
        if (deactivateCoroutine != null)
        {
            StopCoroutine(deactivateCoroutine);
            Debug.Log("Stopped existing coroutine for " + gameObject.name);
        }

        // �V�����R���[�`�����J�n
        deactivateCoroutine = StartCoroutine(DeactivateAfterLifetime());
    }

    private IEnumerator DeactivateAfterLifetime()
    {
        Debug.Log("Coroutine started for " + gameObject.name + " with lifetime duration " + lifetimeDuration);

        // �w�肵���x�����ԑҋ@
        yield return new WaitForSeconds(lifetimeDuration);

        Debug.Log("Deactivating " + gameObject.name + " after lifetime duration");

        // �I�u�W�F�N�g���A�N�e�B�u�ɂ���
        gameObject.SetActive(false);
        deactivateCoroutine = null; // �R���[�`�������������̂ŁAnull�Ƀ��Z�b�g
    }
}