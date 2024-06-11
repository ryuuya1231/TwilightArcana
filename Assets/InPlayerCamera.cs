using UnityEngine;
using Cinemachine;

/// <summary>
/// CinemachineVirtualCameraを制御するクラス
/// </summary>
public class PlayerCamera : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera freeLookCamera;
    [SerializeField] CinemachineVirtualCamera lockonCamera;

    readonly int LockonCameraActivePriority = 11;
    readonly int LockonCameraInactivePriority = 0;

    private void Start()
    {
            // オブジェクトの名前でカメラを検索して取得
            GameObject freeLookCameraObject = GameObject.Find("CM vcam1");
            GameObject lockonCameraObject = GameObject.Find("CM vcam2");

            // オブジェクトが見つかった場合にコンポーネントを取得
            if (freeLookCameraObject != null)
            {
                freeLookCamera = freeLookCameraObject.GetComponent<CinemachineVirtualCamera>();
            }
            else
            {
                Debug.LogError("FreeLookCameraオブジェクトが見つかりません。");
            }

            if (lockonCameraObject != null)
            {
                lockonCamera = lockonCameraObject.GetComponent<CinemachineVirtualCamera>();
            }
            else
            {
                Debug.LogError("LockonCameraオブジェクトが見つかりません。");
            }
            // コンポーネントが見つからなかった場合のエラーハンドリング
            if (freeLookCamera == null)
            {
                Debug.LogError("FreeLookCameraにCinemachineVirtualCameraコンポーネントがアタッチされていません。");
            }
            if (lockonCamera == null)
            {
                Debug.LogError("LockonCameraにCinemachineVirtualCameraコンポーネントがアタッチされていません。");
            }
    }

        /// <summary>
        /// カメラの角度をプレイヤーを基準にリセット
        /// </summary>
        public void ResetFreeLookCamera()
    {
        // 未実装
    }


    /// <summary>
    /// ロックオン時のVirtualCamera切り替え
    /// </summary>
    /// <param name="target"></param>
    public void ActiveLockonCamera(GameObject target)
    {
        lockonCamera.Priority = LockonCameraActivePriority;
        lockonCamera.LookAt = target.transform;
    }


    /// <summary>
    /// ロックオン解除時のVirtualCamera切り替え
    /// </summary>
    public void InactiveLockonCamera()
    {
        lockonCamera.Priority = LockonCameraInactivePriority;
        lockonCamera.LookAt = null;

        // 直前のLockonCameraの角度を引き継ぐ
        var pov = freeLookCamera.GetCinemachineComponent<CinemachinePOV>();
        pov.m_VerticalAxis.Value = Mathf.Repeat(lockonCamera.transform.eulerAngles.x + 180, 360) - 180;
        pov.m_HorizontalAxis.Value = lockonCamera.transform.eulerAngles.y;
    }

}