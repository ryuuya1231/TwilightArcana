using UnityEngine;
using System.Collections;


public class TurretBulletController : MonoBehaviour
{
    public float bulletSpeed = 10f; // �e�̈ړ����x
    private Transform bulletTarget; // �e�̃^�[�Q�b�g
    private Vector3 bulletStartPosition; // ���ˈʒu

    // �e�̃^�[�Q�b�g��ݒ肷�郁�\�b�h
    public void SetTarget(Transform target)
    {
        this.bulletTarget = target;
    }

    // ���ˈʒu��ݒ肷�郁�\�b�h
    public void SetStartPosition(Vector3 position)
    {
        bulletStartPosition = position;
        transform.position = bulletStartPosition; // �e�̈ʒu�𔭎ˈʒu�ɐݒ�
    }

    void Update()
    {
        // �^�[�Q�b�g�����݂��Ȃ��ꍇ�A�e��j��
        if (bulletTarget == null)
        {
            Debug.Log("Target is null. Destroying bullet.");
            Destroy(gameObject);
            return;
        }

        // �^�[�Q�b�g�Ɍ������������v�Z
        Vector3 direction = (bulletTarget.position - transform.position).normalized;
        float distanceThisFrame = bulletSpeed * Time.deltaTime;

        // �^�[�Q�b�g�ɓ��B�����ꍇ�A�q�b�g���������s
        if (Vector3.Distance(transform.position, bulletTarget.position) <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        // �e���^�[�Q�b�g�Ɍ����Ĉړ�
        transform.Translate(direction * distanceThisFrame, Space.World);
        transform.LookAt(bulletTarget); // �e�̌������^�[�Q�b�g�Ɍ�����
    }

    // �^�[�Q�b�g�Ƀq�b�g�����ۂ̏���
    void HitTarget()
    {
        // �f�o�b�O���O: �^�[�Q�b�g�Ƀq�b�g
        Debug.Log("Bullet hit target: " + bulletTarget.name);

        // �^�[�Q�b�g�i�G�I�u�W�F�N�g�j��j��
        if (bulletTarget != null)
        {
            // Destroy(bulletTarget.gameObject);
        }
        // �e��j��
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
}