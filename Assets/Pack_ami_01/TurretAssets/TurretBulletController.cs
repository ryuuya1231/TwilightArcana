using UnityEngine;
using System.Collections;


public class TurretBulletController : MonoBehaviour
{
    public float bulletSpeed = 10f; // 弾の移動速度
    private Transform bulletTarget; // 弾のターゲット
    private Vector3 bulletStartPosition; // 発射位置
    [SerializeField] float lifeTime = 1.0f;
    [SerializeField] int IsDamage = 1;
    private void Start()
    {
        StartCoroutine(nameof(Timer));
    }
    // 弾のターゲットを設定するメソッド
    public void SetTarget(Transform target)
    {
        this.bulletTarget = target;
    }

    // 発射位置を設定するメソッド
    public void SetStartPosition(Vector3 position)
    {
        bulletStartPosition = position;
        transform.position = bulletStartPosition; // 弾の位置を発射位置に設定
    }

    void Update()
    {
        // ターゲットが存在しない場合、弾を破壊
        if (bulletTarget == null)
        {
            Debug.Log("Target is null. Destroying bullet.");
            Destroy(gameObject);
            return;
        }
        Vector3 vec = bulletTarget.position+new UnityEngine.Vector3(0.0f, 4.0f, 0.0f);
        // ターゲットに向かう方向を計算
        Vector3 direction = (vec - transform.position).normalized;
        float distanceThisFrame = bulletSpeed * Time.deltaTime;

        // ターゲットに到達した場合、ヒット処理を実行
        if (Vector3.Distance(transform.position, bulletTarget.position) <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        // 弾をターゲットに向けて移動
        transform.Translate(direction * distanceThisFrame, Space.World);
        transform.LookAt(bulletTarget); // 弾の向きをターゲットに向ける
    }

    // ターゲットにヒットした際の処理
    void HitTarget()
    {
        // デバッグログ: ターゲットにヒット
        Debug.Log("Bullet hit target: " + bulletTarget.name);

        // ターゲット（敵オブジェクト）を破壊
        if (bulletTarget != null)
        {
            // Destroy(bulletTarget.gameObject);
        }
        // 弾を破壊
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