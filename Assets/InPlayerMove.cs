using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.VFX;

public class InMove : MonoBehaviour, IDamageable
{
    [SerializeField] private Charadata data;
    float hp = 0;
    static int hashAttackType = Animator.StringToHash("AttackType");
    public float PlayerMovePower = 0;
    Animator animator;
    UnityEngine.Quaternion targetRotation;
    float inv = 1.5f;
    [SerializeField] CapsuleCollider coll;
    //private GameObject[] hitEffects;
    [SerializeField] GameObject HealingEffect;
    [SerializeField] int HealingCount=5;
    [SerializeField] int HealPower = 45;

    float protect=1;
    public float Hp
    {
        get { return hp; }
        set
        {
            hp = Mathf.Clamp(value, 0, data.MAXHP);

            if (hp <= 0)
            {
                Death();
            }
        }
    }

    public void Damage(int value)
    {
        
        if (value <= 0)
        {
            return;
        }

        Protect();
        Hp -= (float)value*protect;
        if (Hp <= 0)
        {
            Death();
        }
        Debug.Log( Hp);
    }

    public float GetPlayerHP()
    {
        return hp;
    }

    public void Death()
    {
        //Destroy(gameObject);
    }
    public void Protect()
    {
        if (GameObject.FindGameObjectWithTag("Protect") != null)
        {
            protect = 0.5f;
        }
        else
        {
            protect = 1;
        }
    }
    void Awake()
    {
        //コンポーネント関連付け
        TryGetComponent(out animator);
        hp = data.MAXHP;
        coll = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        //入力ベクトルの取得
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        var horizontalRotation = UnityEngine.Quaternion.AngleAxis(Camera.main.transform.transform.eulerAngles.y, UnityEngine.Vector3.up);
        var velocity = horizontalRotation * new UnityEngine.Vector3(horizontal, 0, vertical).normalized;

        //速度の取得
        var speed = Input.GetKey(KeyCode.LeftShift) ? 2 : 1;
        var rotationSpeed = PlayerMovePower * Time.deltaTime;

        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                animator.SetTrigger("Rolling");
                coll.enabled = false;
                inv = 1.5f;
            }
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                if (hp <= 100)
                {
                    if (HealingCount >= 0)
                    {
                        animator.SetTrigger("Healing");
                        Heal();
                        speed = 1;
                    }
                }


            }
        }

        //移動方向を向く
        if (velocity.magnitude > 0.5f)
        {
            transform.rotation = UnityEngine.Quaternion.LookRotation(velocity, UnityEngine.Vector3.up);
        }
        transform.rotation = UnityEngine.Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed);
        //移動速度をanimatorに代入
        animator.SetFloat("Speed", velocity.magnitude * speed, 0.1f, Time.deltaTime);

       
        inv -= Time.deltaTime;
        if (inv <= 0)
        {
            inv = 0;
            coll.enabled = true;
        }
        DebugKey();
    }
    void FootR() 
    {
        GetComponent<AudioSource>().Play();
    }
    void FootL() 
    {
        GetComponent<AudioSource>().Play();
    }

    public void Heal()
    {
        //GameObject _prefab = Resources.Load<GameObject>("Prefabs/Healing");
        UnityEngine.Vector3 _pos = GameObject.FindGameObjectWithTag("Player").transform.position;
        UnityEngine.Quaternion PlayerRot = GameObject.FindGameObjectWithTag("Player").transform.rotation;
        Instantiate(HealingEffect, _pos,PlayerRot);

    }


    void Healing()
    {
        hp += HealPower;
        HealingCount--;
        if (hp >= 100)
        {
            hp = 100;
        }
       
        
    }
    void Hit() { }
    void CallAnimationEnd() { }

    private void DebugKey()
    {
        if(Input.GetKeyDown(KeyCode.Alpha5))
        {
            SceneManager.LoadScene("BildScene");

        }
        //if (Input.GetKeyDown(KeyCode.Alpha4))
        //{
        //    SceneManager.LoadScene("TitleScene");


        //}
    }
}
//using UnityEngine;

//public class PlayerController : MonoBehaviour
//{
//    public float moveSpeed = 5f;
//    public float rotationSpeed = 720f;
//    public float jumpForce = 5f;

//    private Animator animator;
//    private Rigidbody rb;
//    private bool isGrounded;

//    void Awake()
//    {
//        animator = GetComponent<Animator>();
//        rb = GetComponent<Rigidbody>();
//    }

//    void Update()
//    {
//        HandleMovement();
//        HandleJump();
//    }

//    void HandleMovement()
//    {
//        float horizontal = Input.GetAxis("Horizontal");
//        float vertical = Input.GetAxis("Vertical");

//        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

//        if (direction.magnitude >= 0.1f)
//        {
//            // Calculate target angle
//            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
//            // Smooth the rotation
//            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationSpeed, 0.1f);
//            transform.rotation = Quaternion.Euler(0, angle, 0);

//            // Move in the direction
//            Vector3 moveDirection = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
//            rb.MovePosition(transform.position + moveDirection.normalized * moveSpeed * Time.deltaTime);

//            // Update animator
//            animator.SetFloat("Speed", direction.magnitude);
//        }
//        else
//        {
//            animator.SetFloat("Speed", 0);
//        }
//    }

//    void HandleJump()
//    {
//        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f);

//        if (isGrounded && Input.GetButtonDown("Jump"))
//        {
//            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
//            animator.SetTrigger("Jump");
//        }
//    }
//}
