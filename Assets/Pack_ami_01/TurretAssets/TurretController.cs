using UnityEngine;
using System.Collections;

public class TurretController : MonoBehaviour
{
    public float detectionRadius = 10f; // 敵を検出する範囲
    public GameObject turretBulletPrefab; // 発射する弾のプレハブ
    public Transform turretBulletSpawnPoint; // 弾の発射位置
    public float turretFireRate = 1f; // 弾の発射レート（秒）
    public float turretDelayBeforeFire = 0.5f; // タレットが完全に出現してから弾を発射するまでの遅延時間

    private float nextTurretFireTime;
    private bool isTurretReadyToFire = false;

    void OnEnable()
    {
        // タレットがアクティブになるたびに遅延を設定
        StartCoroutine(ReadyToFireAfterDelay());
    }

    private IEnumerator ReadyToFireAfterDelay()
    {
        // 遅延が設定されている場合に待機
        isTurretReadyToFire = false;
        yield return new WaitForSeconds(turretDelayBeforeFire);
        isTurretReadyToFire = true;
    }

    void Update()
    {
        if (!isTurretReadyToFire) return;

        // タレットの位置から一定の範囲内にいるすべてのコライダーを取得
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius);
        GameObject closestEnemy = null;
        float closestDistance = detectionRadius;

        // 検出されたコライダーをチェックして、最も近い敵を見つける
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

        // デバッグログ: 検出された最も近い敵
        if (closestEnemy != null)
        {
           // Debug.Log("Detected closest enemy: " + closestEnemy.name);
        }

        // 最も近い敵が範囲内にあり、発射レートの間隔が経過している場合に弾を発射
        if (closestEnemy != null && Time.time >= nextTurretFireTime)
        {
            Shoot(closestEnemy.transform);
            nextTurretFireTime = Time.time + 1f / turretFireRate;
        }
    }

    // 指定されたターゲットに向かって弾を発射するメソッド
    void Shoot(Transform target)
    {
        // デバッグログ: 弾の発射
        //Debug.Log("Shooting at target: " + target.name);

        // 弾のインスタンスを生成して発射
        GameObject bullet = Instantiate(turretBulletPrefab, turretBulletSpawnPoint.position, turretBulletSpawnPoint.rotation);
        TurretBulletController bulletController = bullet.GetComponent<TurretBulletController>();
        if (bulletController != null)
        {
            // 弾のターゲットを設定
            bulletController.SetTarget(target);
            // 発射位置を設定
            bulletController.SetStartPosition(turretBulletSpawnPoint.position);
        }
    }

    // タレットの検出範囲を視覚化するメソッド
    void OnDrawGizmosSelected()
    {
        // 検出範囲を赤いワイヤーフレームの球として描画
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}