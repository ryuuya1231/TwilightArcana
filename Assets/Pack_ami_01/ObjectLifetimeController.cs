using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectLifetimeController : MonoBehaviour
{
    // 出現してから非アクティブにするまでの時間（秒）
    public float lifetimeDuration = 10f;
    private Coroutine deactivateCoroutine;

    void OnEnable()
    {
        Debug.Log("OnEnable called for " + gameObject.name);

        // コルーチンが既に実行中の場合は停止する
        if (deactivateCoroutine != null)
        {
            StopCoroutine(deactivateCoroutine);
            Debug.Log("Stopped existing coroutine for " + gameObject.name);
        }

        // 新しいコルーチンを開始
        deactivateCoroutine = StartCoroutine(DeactivateAfterLifetime());
    }

    private IEnumerator DeactivateAfterLifetime()
    {
        Debug.Log("Coroutine started for " + gameObject.name + " with lifetime duration " + lifetimeDuration);

        // 指定した遅延時間待機
        yield return new WaitForSeconds(lifetimeDuration);

        Debug.Log("Deactivating " + gameObject.name + " after lifetime duration");

        // オブジェクトを非アクティブにする
        gameObject.SetActive(false);
        deactivateCoroutine = null; // コルーチンが完了したので、nullにリセット
    }
}