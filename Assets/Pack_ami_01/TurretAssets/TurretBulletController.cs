using UnityEngine;
using System.Collections;


public class TurretBulletController : MonoBehaviour
{
    public float bulletSpeed = 10f; // �e�̈ړ����x
    private Transform bulletTarget; // �e�̃^�[�Q�b�g
    private Vector3 bulletStartPosition; // ���ˈʒu
    [SerializeField] float lifeTime = 1.0f;
    [SerializeField] int IsDamage = 1;
    private void Start()
    {
        StartCoroutine(nameof(Timer));
    }
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
        Vector3 vec = bulletTarget.position+new UnityEngine.Vector3(0.0f, 4.0f, 0.0f);
        // �^�[�Q�b�g�Ɍ������������v�Z
        Vector3 direction = (vec - transform.position).normalized;
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
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("!Enemy!Hit");
            damageable.Damage((int)IsDamage);
            Destroy(gameObject);
        }
    }
    IEnumerator Timer()
    {
        yield return new WaitForSeconds(lifeTime);

        Destroy(gameObject);
    }
}