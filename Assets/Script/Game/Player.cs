using FlMr_Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    float moveSpeed;
    float inputHorizontal;
    float inputVertical;
    Rigidbody rb;
    int playerHp = 100;

    void Start()
    {
        moveSpeed = 10f;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal");// * moveSpeed * Time.deltaTime; ;
        inputVertical = Input.GetAxisRaw("Vertical");// * moveSpeed * Time.deltaTime;
    }

    void FixedUpdate()
    {
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

        Vector3 moveForward = cameraForward * inputVertical + Camera.main.transform.right * inputHorizontal;

        rb.velocity = moveForward * moveSpeed + new Vector3(0, rb.velocity.y, 0);

        if (moveForward != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveForward);
        }
    }

    public int GetPlayerHP()
    {
        return playerHp;
    }
    public void SetPlayerHPDamage(int damege)
    {
        playerHp -= damege;
    }
}
