using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Player_MagicArrow : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] Transform target;
    [SerializeField, Min(0)] float time = 0.5f;
    [SerializeField] float lifeTime = 1.0f;
    [SerializeField] bool limitAcceleration = false;
    [SerializeField, Min(0)] float maxAcceleration = 100;
    [SerializeField] Vector3 minInitVelocity;
    [SerializeField] Vector3 maxInitVelocity;
    [SerializeField] VisualEffect Effect;
    Vector3 position;
    Vector3 velocity;
    Vector3 acceleration;
    Transform thisTransform;
    [SerializeField] float trackingRadius = 10.0f;
    //float targetTime = 1;
    public Transform Target
    {
        set { target = value; }
        get { return target; }
    }

    void Start()
    {
        //Debug.Log("Arrow");
        Player = GameObject.FindGameObjectWithTag("Player");
        target = GameObject.FindGameObjectWithTag("EnemyTarget").transform;
        thisTransform = transform;
        position = Player.transform.position + new Vector3(0, 2, 0);
        thisTransform = transform;
        velocity = new Vector3(Random.Range(minInitVelocity.x, maxInitVelocity.x), Random.Range(minInitVelocity.y, maxInitVelocity.y), Random.Range(minInitVelocity.z, maxInitVelocity.z));
        StartCoroutine(nameof(Timer));
        Effect.SendEvent("StartEffect");
    }
    public void Update()
    {
        //if (target == null){return;}
        //if ((target.position - position).sqrMagnitude > trackingRadius * trackingRadius)
        //{
        //    return;
        //}


        acceleration = 2f / (time * time) * (target.position - position - time * velocity);

        if (limitAcceleration && acceleration.sqrMagnitude > maxAcceleration * maxAcceleration)
        {
            acceleration = acceleration.normalized * maxAcceleration;
        }

        time -= Time.deltaTime;

        if (time < 0f)
        {
            return;
        }
        Quaternion randomRotation = Quaternion.Euler(0, Random.Range(-45, 45), 0);
        velocity += acceleration * Time.deltaTime;
        position += velocity * Time.deltaTime;
        thisTransform.position = position;
        thisTransform.rotation = Quaternion.LookRotation(velocity);

        thisTransform.Rotate(0, 0, 100);

    }
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("!Enemy!Hit");
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("!Enemy!Hit");
            Destroy(gameObject);
            //Player.GetComponent<PlayerAttacker>().HitTrigger(collision);
            // Debug.Log("Hit");
        }
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(lifeTime);

        Destroy(gameObject);
    }

    public void TargetRange()
    {
        if (target == null)

        target = GameObject.FindGameObjectWithTag("EnemyTarget").transform;
        if ((target.position - position).sqrMagnitude > trackingRadius * trackingRadius)
        {

            return;
        }

    }

}
