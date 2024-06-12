//using UnityEngine;
//using UnityEngine.InputSystem;

///// <summary>
///// �v���C���[�̈ړ��𐧌䂷��N���X
///// </summary>
//public class PlayerMove : MonoBehaviour
//{
//    [Header("Reference")]
//    [SerializeField] Transform playerTrn;
//    [SerializeField] Transform orientationTrn;
//    [SerializeField] Transform groundCheckTrn;

//    [Header("Movement")]
//    [SerializeField] float moveSpeed = 10;
//    [SerializeField] float rotationSpeed = 180;
//    [SerializeField] float groundDrag = 10;

//    [Header("Jumping")]
//    [SerializeField] float jumpForce = 10;
//    [SerializeField] float jumpCooldown = 1;
//    [SerializeField] float airMultiplier = 0.2f;
//    bool readyToJump;

//    [Header("Ground Check")]
//    [SerializeField] LayerMask whatIsGround;
//    float groundCheckRayLength = 0.3f;
//    bool isGrounded = true;
//    bool wasGrounded = true;

//    [Header("Slope Handling")]
//    [SerializeField] float maxSlopeAngle = 45;
//    float slopeCheckRayLength = 0.5f;
//    private RaycastHit slopeHit;
//    private bool exitingSlope;


//    Rigidbody rigidBody;
//    Transform cameraTrn;
//    Vector3 moveDirection;
//    Quaternion targetRotation;

//    Vector2 moveInput; // �ړ�����
//    bool jumpInput;

//    readonly float GROUND_DRAG = 5;
//    readonly float GRAVITY = 9.81f;
//    readonly Vector2 VECTOR2_ZERO = new Vector2(0, 0);

//    void Start()
//    {
//        cameraTrn = Camera.main.transform;

//        rigidBody = playerTrn.GetComponent<Rigidbody>();
//        rigidBody.drag = groundDrag;

//        readyToJump = true;
//    }

//    void Update()
//    {
//        CheckGround();
//        Rotate();
//        SpeedControl();

//        if (jumpInput)
//        {
//            Jump();
//            jumpInput = false;
//        }
//    }

//    void FixedUpdate()
//    {
//        Move();
//    }


//    /// <summary>
//    /// �v���C���[���ڒn���Ă��邩����
//    /// </summary>
//    void CheckGround()
//    {
//        isGrounded = Physics.Raycast(groundCheckTrn.position, Vector3.down, groundCheckRayLength, whatIsGround);
//        if (wasGrounded != isGrounded)
//        {
//            if (isGrounded)
//            {
//                rigidBody.drag = groundDrag;
//            }
//            else
//            {
//                rigidBody.drag = 0;
//            }
//            wasGrounded = isGrounded;
//        }
//    }


//    // �ړ����͂��󂯎��֐�
//    public void OnMove(InputValue value)
//    {
//        moveInput = value.Get<Vector2>();
//    }

//    // �W�����v���͂��󂯎��֐�
//    public void OnJump(InputValue value)
//    {
//        jumpInput = value.isPressed;
//    }




//    /// <summary>
//    /// �ړ�����
//    /// </summary>
//    void Move()
//    {
//        moveDirection = orientationTrn.forward * moveInput.y + orientationTrn.right * moveInput.x;

//        // �X���[�v��
//        if (OnSlope() && !exitingSlope)
//        {
//            rigidBody.AddForce(GetSlopeMoveDirection() * moveSpeed * 20f, ForceMode.Force);

//            if (rigidBody.velocity.y > 0)
//                rigidBody.AddForce(Vector3.down * 80f, ForceMode.Force);
//        }
//        // �n��
//        else if (isGrounded)
//        {
//            rigidBody.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
//        }
//        // ��
//        else if (!isGrounded)
//        {
//            rigidBody.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
//        }

//        // �X���[�v�ɂ��鎞�d�͂𖳌���
//        rigidBody.useGravity = !OnSlope();
//    }


//    /// <summary>
//    /// �ō����x����
//    /// </summary>
//    void SpeedControl()
//    {
//        if (OnSlope() && !exitingSlope)
//        {
//            if (rigidBody.velocity.magnitude > moveSpeed)
//                rigidBody.velocity = rigidBody.velocity.normalized * moveSpeed;
//        }
//        else
//        {
//            Vector3 flatVel = new Vector3(rigidBody.velocity.x, 0f, rigidBody.velocity.z);
//            if (flatVel.magnitude > moveSpeed)
//            {
//                Vector3 limitedVel = flatVel.normalized * moveSpeed;
//                rigidBody.velocity = new Vector3(limitedVel.x, rigidBody.velocity.y, limitedVel.z);
//            }
//        }

//    }


//    /// <summary>
//    /// ��]����
//    /// </summary>
//    void Rotate()
//    {
//        var dir = playerTrn.position - new Vector3(cameraTrn.position.x, playerTrn.position.y, cameraTrn.position.z);
//        orientationTrn.forward = dir.normalized;

//        moveDirection = orientationTrn.forward * moveInput.y + orientationTrn.right * moveInput.x;

//        if (moveDirection != Vector3.zero)
//        {
//            playerTrn.forward = Vector3.Slerp(playerTrn.forward, moveDirection.normalized, rotationSpeed * Time.deltaTime);
//        }
//    }


//    /// <summary>
//    /// �W�����v����
//    /// </summary>
//    private void Jump()
//    {
//        if (readyToJump && isGrounded)
//        {
//            readyToJump = false;

//            exitingSlope = true;
//            rigidBody.velocity = new Vector3(rigidBody.velocity.x, 0f, rigidBody.velocity.z);
//            rigidBody.AddForce(transform.up * jumpForce, ForceMode.Impulse);

//            Invoke(nameof(ResetJump), jumpCooldown);
//        }
//    }
//    private void ResetJump()
//    {
//        readyToJump = true;
//        exitingSlope = false;
//    }

//    /// <summary>
//    /// �X���[�v��ɂ��邩
//    /// </summary>
//    /// <returns></returns>
//    private bool OnSlope()
//    {
//        if (Physics.Raycast(groundCheckTrn.position, Vector3.down, out slopeHit, slopeCheckRayLength))
//        {
//            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
//            return angle < maxSlopeAngle && angle != 0;
//        }

//        return false;
//    }

//    private Vector3 GetSlopeMoveDirection()
//    {
//        return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
//    }


//}
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.VFX;

public class InMove : MonoBehaviour
{
    static int hashAttackType = Animator.StringToHash("AttackType");
    public float PlayerMovePower = 0;
    Animator animator;
    UnityEngine.Quaternion targetRotation;
    void Awake()
    {
        //�R���|�[�l���g�֘A�t��
        TryGetComponent(out animator);
    }

    // Update is called once per frame
    void Update()
    {
        //���̓x�N�g���̎擾
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        var horizontalRotation = UnityEngine.Quaternion.AngleAxis(Camera.main.transform.transform.eulerAngles.y, UnityEngine.Vector3.up);
        var velocity = horizontalRotation * new UnityEngine.Vector3(horizontal, 0, vertical).normalized;

        //���x�̎擾
        var speed = Input.GetKey(KeyCode.LeftShift) ? 2 : 1;
        var rotationSpeed = PlayerMovePower * Time.deltaTime;

        //�ړ�����������
        if (velocity.magnitude > 0.5f)
        {
            transform.rotation = UnityEngine.Quaternion.LookRotation(velocity, UnityEngine.Vector3.up);
        }
        transform.rotation = UnityEngine.Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed);
        //�ړ����x��animator�ɑ��
        animator.SetFloat("Speed", velocity.magnitude * speed, 0.1f, Time.deltaTime);

        {
            //if (Input.GetKeyUp(KeyCode.Q) || Input.GetKeyUp(KeyCode.E) || Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Mouse1))
            //{
            //    speed = 1;
            //    animator.SetFloat("Speed", speed);
            //}
            if (Input.GetKeyDown(KeyCode.Space))
            {
                animator.SetTrigger("Rolling");
            }
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("invincible"))
        {
            Debug.Log("Rolling");
        }

    }
    void FootR() { }
    void FootL() { }
    void Hit() { }
    void CallAnimationEnd() { }
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
