using UnityEngine;
using System.Collections;

public class TurretController : MonoBehaviour
{
    public float detectionRadius = 10f; // �G�����o����͈�
    public GameObject turretBulletPrefab; // ���˂���e�̃v���n�u
    public Transform turretBulletSpawnPoint; // �e�̔��ˈʒu
    public float turretFireRate = 1f; // �e�̔��˃��[�g�i�b�j
    public float turretDelayBeforeFire = 0.5f; // �^���b�g�����S�ɏo�����Ă���e�𔭎˂���܂ł̒x������

    private float nextTurretFireTime;
    private bool isTurretReadyToFire = false;

    void OnEnable()
    {
        // �^���b�g���A�N�e�B�u�ɂȂ邽�тɒx����ݒ�
        StartCoroutine(ReadyToFireAfterDelay());
    }

    private IEnumerator ReadyToFireAfterDelay()
    {
        // �x�����ݒ肳��Ă���ꍇ�ɑҋ@
        isTurretReadyToFire = false;
        yield return new WaitForSeconds(turretDelayBeforeFire);
        isTurretReadyToFire = true;
    }

    void Update()
    {
        if (!isTurretReadyToFire) return;

        // �^���b�g�̈ʒu������͈͓̔��ɂ��邷�ׂẴR���C�_�[���擾
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius);
        GameObject closestEnemy = null;
        float closestDistance = detectionRadius;

        // ���o���ꂽ�R���C�_�[���`�F�b�N���āA�ł��߂��G��������
        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                float distance = Vector3.Distance(transform.position, hitCollider.transform.position);
                if (distance < closestDistance)
                {
                    closestEnemy = hitCollider.gameObject;
                    closestDistance = distance;
                }
            }
        }

        // �f�o�b�O���O: ���o���ꂽ�ł��߂��G
        if (closestEnemy != null)
        {
           // Debug.Log("Detected closest enemy: " + closestEnemy.name);
        }

        // �ł��߂��G���͈͓��ɂ���A���˃��[�g�̊Ԋu���o�߂��Ă���ꍇ�ɒe�𔭎�
        if (closestEnemy != null && Time.time >= nextTurretFireTime)
        {
            Shoot(closestEnemy.transform);
            nextTurretFireTime = Time.time + 1f / turretFireRate;
        }
    }

    // �w�肳�ꂽ�^�[�Q�b�g�Ɍ������Ēe�𔭎˂��郁�\�b�h
    void Shoot(Transform target)
    {
        // �f�o�b�O���O: �e�̔���
        //Debug.Log("Shooting at target: " + target.name);

        // �e�̃C���X�^���X�𐶐����Ĕ���
        GameObject bullet = Instantiate(turretBulletPrefab, turretBulletSpawnPoint.position, turretBulletSpawnPoint.rotation);
        TurretBulletController bulletController = bullet.GetComponent<TurretBulletController>();
        if (bulletController != null)
        {
            // �e�̃^�[�Q�b�g��ݒ�
            bulletController.SetTarget(target);
            // ���ˈʒu��ݒ�
            bulletController.SetStartPosition(turretBulletSpawnPoint.position);
        }
    }

    // �^���b�g�̌��o�͈͂����o�����郁�\�b�h
    void OnDrawGizmosSelected()
    {
        // ���o�͈͂�Ԃ����C���[�t���[���̋��Ƃ��ĕ`��
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}