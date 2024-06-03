using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMove : MonoBehaviour
{
    // 移動速度
    public float moveSpeed = 5.0f;

    void Update()
    {
        // 入力を取得
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // 移動ベクトルを計算
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        // 移動を適用
        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);
    }
}
