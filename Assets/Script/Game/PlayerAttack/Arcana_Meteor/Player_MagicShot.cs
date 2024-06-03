using FlMr_Inventory;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class Player_MagicShot : MonoBehaviour
{
    public float shot_speed = 0.0f;
    protected Vector3 forward;
    protected Rigidbody rb;
    protected GameObject characterObject;
    private GameObject attPrefab;
    [SerializeField] private VisualEffect visualEffect;
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();            // プレハブのRigidbodyを取得
        GameObject g = GameObject.FindWithTag("Enemy");
        if (g)
        {
            Debug.Log(g.name + "を探知しました");
            forward = g.transform.position - characterObject.transform.position;
            forward.Normalize();
        }
        else
        {
            Debug.Log("敵を探知できませんでした");
            forward = characterObject.transform.forward;
        }
        Debug.Log(characterObject.name + "|forward:" + forward + "|position:" + rb.position);
        Destroy(attPrefab, 5.0f);
        if (visualEffect)
        {
            Debug.Log("VisualEffect参照完了");
            visualEffect = Instantiate(visualEffect, rb.position, Quaternion.identity);
            visualEffect.Play();
        }
        else Debug.Log("VisualEffect参照失敗");
    }
    void Update()
    {
        rb.velocity = forward.normalized * shot_speed;        // プレハブを移動させる
        if (visualEffect)
        {
            visualEffect.transform.position = rb.position;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        // 敵に衝突したら、プレハブを破壊
        if (other.gameObject.tag == "Enemy")
        {
            var enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<GolemController>();
            if (enemy)
            {
                enemy.GetAnimator().SetTrigger("Damage");
                enemy.GetNavMesh().updatePosition = false;
                enemy.GetNavMesh().updateRotation = false;
                enemy.GetNavMesh().speed = 0.0f;
                Debug.Log(enemy.name + "と衝突");
            }
            Destroy(attPrefab);
        }
    }
    // Golemと生成されたプレハブのGameObjectのセッター
    public void SetObject(GameObject characterObject, GameObject attPrefab)
    {
        this.characterObject = characterObject;
        this.attPrefab = attPrefab;
    }
    private void OnDestroy()
    {
        if (visualEffect)
        {
            Debug.Log("Object destroyed!");
            visualEffect.enabled = false; ;
        }
    }
}
