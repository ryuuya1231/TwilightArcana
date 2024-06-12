using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;


public class BossController : MonoBehaviour, IDamageable
{
    //ボス本体
    [SerializeField]
    private Animator animator = null;
    [SerializeField]
    private NavMeshAgent navmeshAgent = null;
    [SerializeField]
    private Transform target = null;
    [SerializeField]
    private CapsuleCollider capsuleCollider = null;
    [SerializeField, Min(0)]
    private int maxHp = 100;
    [SerializeField]
    private float deadWaitTime = 3;
    [SerializeField]
    private float chaseDistance = 5;

    //通常攻撃
    [SerializeField]
    private Collider attackCollider = null;
    [SerializeField]
    private float attackPower = 4;
    [SerializeField]
    private float attackTime = 1;
    [SerializeField]
    private float attackInterval = 1;
    [SerializeField]
    private float attackDistance = 6;

    //突進攻撃
    [SerializeField]
    private Collider rashCollider = null;
    [SerializeField]
    private float rashPower = 20;
    [SerializeField]
    private float rashTime = 0.5f;
    [SerializeField]
    private float rashInterval = 2;
    [SerializeField]
    private float chargeTime = 2;
    [SerializeField]
    private float rashCoolTime = 30;

    //ジャンプ攻撃
    [SerializeField]
    private Collider jumpCollider = null;
    [SerializeField]
    private float jumpPower = 10;
    [SerializeField]
    private float jumpTime = 1;
    [SerializeField]
    private float jumpInterval = 1;
    [SerializeField]
    private float jumpDistance = 24;

    //のけぞりによるダメージ演出
    [SerializeField]
    private Transform waistBone;

    //死亡エフェクト地点設定
    [SerializeField]
    private Transform BasePoint;

    //アニメーション読み込み
    readonly int MoveHash = Animator.StringToHash("Walk");
    readonly int AttackHash = Animator.StringToHash("Hit2");
    readonly int RashHash = Animator.StringToHash("Hit");
    readonly int DeadHash = Animator.StringToHash("Die");
    readonly int RageHash = Animator.StringToHash("Rage");
    readonly int JumpHash = Animator.StringToHash("Jump");
    readonly int LandHash = Animator.StringToHash("Land");

    //フラグ関係
    private bool isDead = false;
    private bool isAttacking = false;
    private bool powerUp = false;
    private bool attack = false;
    private bool rash = false;
    private bool jump = false;

    //その他
    public int hp = 0;
    private float coolTime = 0;
    private GameObject player;
    private GameObject hitEffect;
    private Transform thisTransform;
    private Transform defaultTarget;

    //エフェクト
    public Transform effectPool;
    private GameObject[] hitEffects;
    private GameObject powerEffect;
    private int effect_Hit = 0;
    private int effect_Jump = 1;
    private int effect_SecondForm = 2;
    private int effect_Rash = 3;
    private int effect_Dead = 4;

    //ノックバック
    float knockBackPower = 50;
    Rigidbody playerRigidbody;

    //のけぞりの回転角度
    private Vector3 offsetAnglesWaist;
    private Sequence seq;

    //通常攻撃時間調整
    WaitForSeconds attackWait;
    WaitForSeconds attackIntervalWait;

    //突進攻撃時間調整
    WaitForSeconds rashWait;
    WaitForSeconds rashIntervalWait;
    WaitForSeconds chargeWait;
    WaitForSeconds animationWait;
    WaitForSeconds rageWait;

    //ジャンプ攻撃時間調整
    WaitForSeconds jumpWait;
    WaitForSeconds jumpIntervalWait;
    WaitForSeconds flyWait;

    //死亡時間調整
    WaitForSeconds deadEffectWait;

    public int Hp
    {
        //ボスのHP
        set
        {
            hp = Mathf.Clamp(value, 0, maxHp);
        }
        get
        {
            return hp;
        }
    }

    public int MaxHp()
    {
        return maxHp;
    }

    void Start()
    {
        //NaviMesh初期化
        thisTransform = transform;
        navmeshAgent = GetComponent<NavMeshAgent>();
        defaultTarget = target;
        player = GameObject.FindGameObjectWithTag("Player");

        //エフェクト初期化
        hitEffects = new GameObject[effectPool.childCount];
        for (int i = 0; i < effectPool.childCount; i++)
        {
            hitEffects[i] = effectPool.GetChild(i).gameObject;
        }

        //リジットボディ初期化
        playerRigidbody = player.GetComponent<Rigidbody>();

        //攻撃関係の当たり判定初期化
        attackCollider.enabled = false;
        rashCollider.enabled = false;
        jumpCollider.enabled = false;

        //通常攻撃時間調整の初期化
        attackWait = new WaitForSeconds(attackTime);
        attackIntervalWait = new WaitForSeconds(attackInterval);

        //突進攻撃時間調整の初期化
        rashWait = new WaitForSeconds(rashTime);
        rashIntervalWait = new WaitForSeconds(rashInterval);
        chargeWait = new WaitForSeconds(chargeTime);
        animationWait = new WaitForSeconds(0.8f);
        rageWait = new WaitForSeconds(1.6f);
        coolTime = rashCoolTime;

        //ジャンプ攻撃時間調整の初期化
        jumpWait = new WaitForSeconds(jumpTime);
        jumpIntervalWait = new WaitForSeconds(jumpInterval);
        flyWait = new WaitForSeconds(1);

        //死亡時間調整の初期化
        deadEffectWait = new WaitForSeconds(1.5f);

        Debug.Log(Hp);
        //ボス初期化
        InitBoss();
    }
    void Update()
    {
        if (isDead)
        {
            animator.SetFloat(MoveHash, 0);
            return;
        }

        //攻撃時NaviMeshの停止
        if (isAttacking)
        { navmeshAgent.isStopped = true; }
        else
        { navmeshAgent.isStopped = false; }


        //ボス当たり判定
        capsuleCollider.enabled = true;

        //デバック用:ボスのHPを半分にする
        if (Input.GetKeyDown(KeyCode.R))
        {
            Hp = maxHp / 2;
            Debug.Log(Hp);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            Death();
        }

        //第二形態移行関数
        SecondForm();

        //突進攻撃用クールタイム
        coolTime += Time.deltaTime;
        //ボス動作関係
        CheckDistance();
        Move();
        UpdateAnimator();

    }
    void InitBoss()
    {
        Hp = maxHp;
    }

    public void Damage(int value)
    {
        if (isDead) { return; }
        //ボスダメージ処理
        if (value <= 0) { return; }
        if (rash) { return; }
        Hp -= value;
        Debug.Log(Hp);
        if (Hp <= 0) { Death(); }
    }

    public void Death()
    {
        //ボス死亡時処理
        isDead = true;
        StopAttack();
        capsuleCollider.enabled = false;
        animator.speed = 1;
        navmeshAgent.isStopped = true;
        animator.SetBool(DeadHash, true);
        Destroy(powerEffect);
        StartCoroutine(nameof(DeadTimer));
    }

    IEnumerator DeadTimer()
    {
        //消滅までの時間調整
        yield return deadEffectWait;
        GameObject effect = SpawnEffect(effect_Dead);
        effect.transform.position = BasePoint.position;
        yield return new WaitForSeconds(deadWaitTime);
        Destroy(gameObject);
    }
    void Move()
    {
        if (player.gameObject == null) { return; }
        //NaviMeshへ位置情報をセット
        if (navmeshAgent.pathStatus != NavMeshPathStatus.PathInvalid)
        {
            navmeshAgent.SetDestination(target.position);
        }
    }

    void UpdateAnimator()
    {
        //移動時アニメーションのセット
        animator.SetFloat(MoveHash, navmeshAgent.desiredVelocity.magnitude);
    }

    void CheckDistance()
    {
        if (player.gameObject == null) { return; }
        //ボスとプレイヤーの距離判定
        float diff = (player.transform.position - thisTransform.position).sqrMagnitude;

        //通常攻撃
        if (diff < attackDistance * attackDistance)
        {
            if (!isAttacking)
            {
                StartCoroutine(nameof(Attack));
            }
        }
        //追跡
        else if (diff < chaseDistance * chaseDistance)
        {
            target = player.transform;
        }
        //ジャンプ攻撃
        else if (diff < jumpDistance * jumpDistance)
        {
            if (!isAttacking)
            {
                StartCoroutine(nameof(Jump));
            }
        }
        //デフォルト時
        else
        {
            target = defaultTarget;
        }
    }
    IEnumerator Attack()
    {
        //通常攻撃処理
        isAttacking = true;
        attack = true;
        navmeshAgent.velocity = Vector3.zero;
        yield return attackWait;
        animator.SetTrigger(AttackHash);
        thisTransform.DOLookAt(target.position, 0.5f);
        yield return new WaitForSeconds(0.4f);
        animator.speed = 0.5f;
        yield return new WaitForSeconds(0.2f);
        attackCollider.enabled = true;
        thisTransform.DOLookAt(target.position, 0.1f);
        animator.speed = 3;
        yield return attackIntervalWait;
        attackCollider.enabled = false;
        animator.speed = 1;
        attack = false;
        isAttacking = false;
    }
    IEnumerator Rash()
    {
        //突進攻撃処理
        isAttacking = true;
        rash = true;
        animator.SetTrigger(RageHash);
        thisTransform.DOLookAt(target.position, 0.2f);
        yield return rageWait;
        animator.SetTrigger(RashHash);
        yield return animationWait;
        animator.speed = 0;
        thisTransform.DOLookAt(target.position, 1);
        RashEffect();
        yield return chargeWait;

        RashAttack();

        yield return rashWait;
        rashCollider.enabled = false;
        yield return rashIntervalWait;
        coolTime = 0;
        rash = false;
        isAttacking = false;
    }

    void RashAttack()
    {
        //突進攻撃処理
        thisTransform.DOLookAt(target.position, 0.1f);
        capsuleCollider.enabled = true;
        thisTransform.DOMove(player.transform.position, 0.5f);
        animator.speed = 1;
        rashCollider.enabled = true;
        capsuleCollider.enabled = false;
    }

    void RashEffect()
    {
        //突進攻撃のエフェクト処理
        GameObject effect = SpawnEffect(effect_Rash);
        effect.transform.position = capsuleCollider.transform.position;
    }

    IEnumerator Jump()
    {
        //ジャンプ攻撃処理
        isAttacking = true;
        jump = true;
        navmeshAgent.velocity = Vector3.zero;
        yield return jumpWait;
        animator.SetTrigger(JumpHash);
        thisTransform.DOJump(player.transform.position, 20, 1, 2);
        JumpEffect();
        yield return flyWait;
        jumpCollider.enabled = true;
        capsuleCollider.enabled = false;
        animator.SetTrigger(LandHash);
        yield return jumpIntervalWait;
        jumpCollider.enabled = false;
        capsuleCollider.enabled = true;
        thisTransform.DOLookAt(target.position, 0.5f);
        jump = false;
        isAttacking = false;
    }

    void JumpEffect()
    {
        //ジャンプ攻撃のエフェクト処理
        GameObject effect = SpawnEffect(effect_Jump);
        effect.transform.position = player.transform.position;
    }

    void StopAttack()
    {
        //全攻撃の停止処理
        StopCoroutine(nameof(Attack));
        StopCoroutine(nameof(Rash));
        StopCoroutine(nameof(Jump));
        attackCollider.enabled = false;
        rashCollider.enabled = false;
        jumpCollider.enabled = false;
        isAttacking = false;
    }

    void SecondForm()
    {
        //第二形態処理
        if (Hp <= maxHp / 2)
        {
            //第二形態移行時に一度だけ呼ばれる処理
            if (!powerUp)
            {
                attackPower *= 1.5f;
                rashPower *= 1.5f;
                jumpPower *= 1.5f;
                powerEffect = SpawnEffect(effect_SecondForm);

                powerUp = true;
            }

            //突進攻撃呼び出し
            if (!isAttacking)
            {
                if (coolTime >= rashCoolTime)
                {
                    StartCoroutine(nameof(Rash));
                }
            }

            //第二形態用エフェクト処理
            if (powerEffect == null) { return; }
            powerEffect.transform.position = BasePoint.position;

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();

        if (other.gameObject.tag == "Player")
        {
            //攻撃の種類を判断する処理
            if (attack)
            {
                //通常攻撃
                Vector3 hitPos = other.ClosestPointOnBounds(attackCollider.bounds.center);
                GameObject newHits = SpawnEffect(effect_Hit);
                newHits.transform.position = hitPos;
                damageable.Damage((int)attackPower);
                knockBackPower = 50;
                attackCollider.enabled = false;
            }
            else if (rash)
            {
                //突進攻撃
                Vector3 hitPos = other.ClosestPointOnBounds(rashCollider.bounds.center);
                GameObject newHits = SpawnEffect(effect_Hit);
                newHits.transform.position = hitPos;
                damageable.Damage((int)rashPower);
                knockBackPower = 500;
                rashCollider.enabled = false;
            }
            else if (jump)
            {
                //ジャンプ攻撃
                Vector3 hitPos = other.ClosestPointOnBounds(jumpCollider.bounds.center);
                GameObject newHits = SpawnEffect(effect_Hit);
                newHits.transform.position = hitPos;
                damageable.Damage((int)jumpPower);
                knockBackPower = 500;
                jumpCollider.enabled = false;
            }
            //プレイヤーのノックバック処理
            playerRigidbody.velocity = Vector3.zero;
            Vector3 distination = (other.transform.position - transform.position).normalized;
            distination.y = 0;
            playerRigidbody.AddForce(distination * knockBackPower, ForceMode.VelocityChange);

        }

    }

    private void OnCollisionEnter(Collision other)
    {
        //被ダメ処理
        if (other.gameObject.tag == "Attack")
        {
            if (rash) { return; }
            var bulletAngles = other.transform.eulerAngles;
            bulletAngles.x = 0f;
            HitTiltWaist(Quaternion.Euler(bulletAngles) * Vector3.back);
        }
    }

    private void HitTiltWaist(Vector3 vector)
    {
        //のけぞり処理
        seq?.Kill();
        seq = DOTween.Sequence();
        vector = transform.InverseTransformVector(vector);
        var tiltAngles = new Vector3(0f, -vector.x, -vector.z).normalized * 30f;
        seq.Append(DOTween.To(() => Vector3.zero, angles => offsetAnglesWaist = angles, tiltAngles, 0.1f));
        seq.Append(DOTween.To(() => tiltAngles, angles => offsetAnglesWaist = angles, Vector3.zero, 0.2f));
        seq.Play();
    }

    private void LateUpdate()
    {
        waistBone.localEulerAngles += offsetAnglesWaist;
    }

    private GameObject SpawnEffect(int effectNum)
    {
        //エフェクト
        GameObject spawnedHit = Instantiate(hitEffects[effectNum]);
        return spawnedHit;
    }

    public void Protect()
    {

    }
}


