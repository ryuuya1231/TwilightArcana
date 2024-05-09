using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class controller : MonoBehaviour
{
    static int hashAttackType = Animator.StringToHash("AttackType");
    public float PlayerMovePower=0;
    Animator animator;


    UnityEngine.Quaternion targetRotation;
    void Awake()
    {
        //コンポーネント関連付け
        TryGetComponent(out animator);

        targetRotation = transform.rotation;
        Cursor.lockState = CursorLockMode.Locked;
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

        //移動方向を向く
        if (velocity.magnitude > 0.5f)
        {
            transform.rotation = UnityEngine.Quaternion.LookRotation(velocity, UnityEngine.Vector3.up);
        }
        transform.rotation = UnityEngine.Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed);
        //移動速度をanimatorに代入
        animator.SetFloat("Speed", velocity.magnitude * speed, 0.1f, Time.deltaTime);
        
        {
            if(Input.GetMouseButtonDown(0))//左クリック
            {
               
            }

            if (Input.GetMouseButtonDown(1))//右クリック
            {

            }

        }
        { 
            if(Input.GetKeyDown(KeyCode.Space))
            {
                animator.SetTrigger("Rolling");
            }
        }

    }

    public int AttackType
    {
        get => animator.GetInteger(hashAttackType);
        set => animator.SetInteger(hashAttackType,value);
    }


    void FootR() { }
    void FootL() { }
    void Hit() { }
    void CallAnimationEnd() { }
}
