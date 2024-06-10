using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMove : MonoBehaviour
{
    // �ړ����x
    public float moveSpeed = 5.0f;

    void Update()
    {
        // ���͂��擾
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // �ړ��x�N�g�����v�Z
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        // �ړ���K�p
        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);
    }
}
