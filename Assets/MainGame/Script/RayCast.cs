using UnityEngine;

public class RaycastTester : MonoBehaviour
{
    [SerializeField] GameObject player;
    private float CoolTime = 0;
    public float CoolTimeNum;
    public bool IsAttackFlg = true;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (IsAttackFlg)
            {
                IsAttackFlg = false;
            
                var startpos = player.transform.position;
                var raydir = player.transform.forward.normalized;
                Debug.DrawRay(startpos, raydir * 30.0f, Color.red);
                RaycastHit hit;
                var isHit = Physics.Raycast(startpos, raydir, out hit, 30f);
                
                if (isHit && hit.collider.CompareTag("Enemy"))
                {
                    //Destroy(hit.collider.gameObject);
                    hit.collider.GetComponent<Enemy>().Damege();

                }
                else
                {
                    return;
                }
            }
        }
        if (!IsAttackFlg)
        {
            CoolTime += Time.deltaTime;
        }
        if (CoolTime >= CoolTimeNum)
        {
            IsAttackFlg = true;
            CoolTime = 0;
        }
    }
}
