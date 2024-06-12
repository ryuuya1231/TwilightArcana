using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// �v���C���[�̃��b�N�I���@�\�̐��䂷��N���X
/// </summary>
public class PlayerLockon : MonoBehaviour
{
    [SerializeField] PlayerCamera playerCamera;
    [SerializeField] Transform originTrn;
    [SerializeField] float lockonRange = 20;
    [SerializeField] string targetLayerName;
    [SerializeField] GameObject lockonCursor;

    float lockonFactor = 0.3f;
    float lockonThreshold = 0.5f;
    bool lockonInput = false;
    bool isLockon = false;

    Camera mainCamera;
    Transform cameraTrn;
    GameObject targetObj;


    void Start()
    {
        mainCamera = Camera.main;
        cameraTrn = mainCamera.transform;
        lockonCursor = GameObject.Find("Canvas");
    }


    void Update()
    {
        if (lockonInput)
        {
            // ���łɃ��b�N�I���ς݂Ȃ��������
            if (isLockon)
            {
                isLockon = false;
                playerCamera.InactiveLockonCamera();
                //lockonCursor.SetActive(false);
                lockonInput = false;
                return;
            }

            // ���b�N�I���Ώۂ̌����A����Ȃ烍�b�N�I���A���Ȃ��Ȃ�J�����p�x�����Z�b�g
            targetObj = GetLockonTarget();
            if (targetObj)
            {
                isLockon = true;
                playerCamera.ActiveLockonCamera(targetObj);
                //lockonCursor.SetActive(true);
            }
            else
            {
                playerCamera.ResetFreeLookCamera();
            }
            lockonInput = false;
        }

        // ���b�N�I���J�[�\��
        if (isLockon)
        {
            //lockonCursor.transform.position = mainCamera.WorldToScreenPoint(targetObj.transform.position);
        }
    }



    // ���b�N�I�����͂��󂯎��֐�
    public void OnLockon(InputValue value)
    {
        lockonInput = value.isPressed;
    }


    /// <summary>
    /// ���b�N�I���Ώۂ̌v�Z�������s���擾����
    /// �v�Z��3�̍H���ɕ������
    /// </summary>
    /// <returns></returns>
    GameObject GetLockonTarget()
    {
        // 1. SphereCastAll���g����Player���ӂ�Enemy���擾��List�Ɋi�[
        RaycastHit[] hits = Physics.SphereCastAll(originTrn.position, lockonRange, Vector3.up, 0, LayerMask.GetMask(targetLayerName));
        if (hits?.Length == 0)
        {
            // �͈͓��Ƀ^�[�Q�b�g�Ȃ�
            return null;
        }


        // 2. 1�̃��X�g�S�Ă�ray���΂��ː����ʂ���̂�����List��
        List<GameObject> hitObjects = new List<GameObject>();
        RaycastHit hit;
        for (var i = 0; i < hits.Length; i++)
        {
            var direction = hits[i].collider.gameObject.transform.position - originTrn.position;
            if (Physics.Raycast(originTrn.position, direction, out hit, lockonRange))
            {
                if (hit.collider.gameObject == hits[i].collider.gameObject)
                {
                    hitObjects.Add(hit.collider.gameObject);
                }
            }
        }
        if (hitObjects?.Count == 0)
        {
            // �ː����ʂ����^�[�Q�b�g�Ȃ�
            return null;
        }


        // 3. 2�̃��X�g�S�Ẵx�N�g���ƃJ�����̃x�N�g�����r���A��ʒ����Ɉ�ԋ߂����̂�T��
        // ����������Ă邩�悭�킩��Ȃ�...
        float degreep = Mathf.Atan2(cameraTrn.forward.x, cameraTrn.forward.z);
        float degreemum = Mathf.PI * 2;
        GameObject target = null;

        foreach (var enemy in hitObjects)
        {
            Vector3 pos = cameraTrn.position - enemy.transform.position;
            Vector3 pos2 = enemy.transform.position - cameraTrn.position;
            pos2.y = 0.0f;
            pos2.Normalize();

            float degree = Mathf.Atan2(pos2.x, pos2.z);
            if (Mathf.PI <= (degreep - degree))
            {
                degree = degreep - degree - Mathf.PI * 2;
            }
            else if (-Mathf.PI >= (degreep - degree))
            {
                degree = degreep - degree + Mathf.PI * 2;
            }
            else
            {
                degree = degreep - degree;
            }

            degree = degree + degree * (pos.magnitude / 500) * lockonFactor;
            if (Mathf.Abs(degreemum) >= Mathf.Abs(degree))
            {
                degreemum = degree;
                target = enemy;
            }
        }

        //// ���߂���ԏ������l�����l��菬�����ꍇ�A�^�[�Q�b�e�B���O���I���ɂ��܂�
        if (Mathf.Abs(degreemum) <= lockonThreshold)
        {
            return target;
        }

        return null;
    }

}