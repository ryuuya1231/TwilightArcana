using UnityEngine;
using Cinemachine;

/// <summary>
/// CinemachineVirtualCamera�𐧌䂷��N���X
/// </summary>
public class PlayerCamera : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera freeLookCamera;
    [SerializeField] CinemachineVirtualCamera lockonCamera;

    readonly int LockonCameraActivePriority = 11;
    readonly int LockonCameraInactivePriority = 0;

    private void Start()
    {
            // �I�u�W�F�N�g�̖��O�ŃJ�������������Ď擾
            GameObject freeLookCameraObject = GameObject.Find("CM vcam1");
            GameObject lockonCameraObject = GameObject.Find("CM vcam2");

            // �I�u�W�F�N�g�����������ꍇ�ɃR���|�[�l���g���擾
            if (freeLookCameraObject != null)
            {
                freeLookCamera = freeLookCameraObject.GetComponent<CinemachineVirtualCamera>();
            }
            else
            {
                Debug.LogError("FreeLookCamera�I�u�W�F�N�g��������܂���B");
            }

            if (lockonCameraObject != null)
            {
                lockonCamera = lockonCameraObject.GetComponent<CinemachineVirtualCamera>();
            }
            else
            {
                Debug.LogError("LockonCamera�I�u�W�F�N�g��������܂���B");
            }
            // �R���|�[�l���g��������Ȃ������ꍇ�̃G���[�n���h�����O
            if (freeLookCamera == null)
            {
                Debug.LogError("FreeLookCamera��CinemachineVirtualCamera�R���|�[�l���g���A�^�b�`����Ă��܂���B");
            }
            if (lockonCamera == null)
            {
                Debug.LogError("LockonCamera��CinemachineVirtualCamera�R���|�[�l���g���A�^�b�`����Ă��܂���B");
            }
    }

        /// <summary>
        /// �J�����̊p�x���v���C���[����Ƀ��Z�b�g
        /// </summary>
        public void ResetFreeLookCamera()
    {
        // ������
    }


    /// <summary>
    /// ���b�N�I������VirtualCamera�؂�ւ�
    /// </summary>
    /// <param name="target"></param>
    public void ActiveLockonCamera(GameObject target)
    {
        lockonCamera.Priority = LockonCameraActivePriority;
        lockonCamera.LookAt = target.transform;
    }


    /// <summary>
    /// ���b�N�I����������VirtualCamera�؂�ւ�
    /// </summary>
    public void InactiveLockonCamera()
    {
        lockonCamera.Priority = LockonCameraInactivePriority;
        lockonCamera.LookAt = null;

        // ���O��LockonCamera�̊p�x�������p��
        var pov = freeLookCamera.GetCinemachineComponent<CinemachinePOV>();
        pov.m_VerticalAxis.Value = Mathf.Repeat(lockonCamera.transform.eulerAngles.x + 180, 360) - 180;
        pov.m_HorizontalAxis.Value = lockonCamera.transform.eulerAngles.y;
    }

}