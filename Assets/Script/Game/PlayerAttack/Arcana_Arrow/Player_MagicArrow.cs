//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.VFX;

//public class Player_MagicArrow : MonoBehaviour
//{
//    [SerializeField] GameObject Player;
//    [SerializeField] Transform target;
//    [SerializeField, Min(0)] float time = 0.5f;
//    [SerializeField] float lifeTime = 1.0f;
//    [SerializeField] bool limitAcceleration = false;
//    [SerializeField, Min(0)] float maxAcceleration = 100;
//    [SerializeField] Vector3 minInitVelocity;
//    [SerializeField] Vector3 maxInitVelocity;
//    [SerializeField] VisualEffect Effect;
//    Vector3 position;
//    Vector3 velocity;
//    Vector3 acceleration;
//    Transform thisTransform;
//    [SerializeField] float trackingRadius = 10.0f;
//    public Transform Target
//    {
//        set { target = value; }
//        get { return target; }
//    }

//    void Start()
//    {
//        Player = GameObject.FindGameObjectWithTag("Player");
//        target = GameObject.FindGameObjectWithTag("EnemyTarget")?.transform;
//        if (target == null)
//        {
//            position = Player.transform.position + new Vector3(0, 2, 0) + Player.transform.forward * 10; // 10 is the distance in the forward direction
//        }
//        else
//        {
//            position = Player.transform.position + new Vector3(0, 2, 0);
//        }
//        thisTransform = transform;
//        position = Player.transform.position + new Vector3(0, 2, 0);
//        thisTransform = transform;
//        velocity = new Vector3(Random.Range(minInitVelocity.x, maxInitVelocity.x), Random.Range(minInitVelocity.y, maxInitVelocity.y), Random.Range(minInitVelocity.z, maxInitVelocity.z));
//        StartCoroutine(nameof(Timer));
//        Effect.SendEvent("StartEffect");
//    }
//    public void Update()
//    {
//        acceleration = 2f / (time * time) * (target.position - position - time * velocity);

//        if (limitAcceleration && acceleration.sqrMagnitude > maxAcceleration * maxAcceleration)
//        {
//            acceleration = acceleration.normalized * maxAcceleration;
//        }

//        time -= Time.deltaTime;

//        if (time < 0f)
//        {
//            return;
//        }
//        Quaternion randomRotation = Quaternion.Euler(0, Random.Range(-45, 45), 0);
//        velocity += acceleration * Time.deltaTime;
//        position += velocity * Time.deltaTime;
//        thisTransform.position = position;
//        thisTransform.rotation = Quaternion.LookRotation(velocity);

//        thisTransform.Rotate(0, 0, 100);

//    }
//    private void OnCollisionEnter(Collision collision)
//    {
//        //Debug.Log("!Enemy!Hit");
//        if (collision.gameObject.tag == "Enemy")
//        {
//            Debug.Log("!Enemy!Hit");
//            Destroy(gameObject);
//            //Player.GetComponent<PlayerAttacker>().HitTrigger(collision);
//            // Debug.Log("Hit");
//        }
//    }

//    IEnumerator Timer()
//    {
//        yield return new WaitForSeconds(lifeTime);

//        Destroy(gameObject);
//    }

//}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Player_MagicArrow : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] Vector3 targetDirection; // Changed type to Vector3
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

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        if (GameObject.FindGameObjectWithTag("EnemyTarget") != null)
        {
            targetDirection = GameObject.FindGameObjectWithTag("EnemyTarget").transform.position;// - Player.transform.position; // Calculate direction towards enemy target
        }
        else
        {
            targetDirection = Player.transform.forward; // If enemy target is null, set direction to player's forward direction
        }

        thisTransform = transform;
        position = Player.transform.position + new Vector3(0, 2, 0);
        thisTransform = transform;
        velocity = new Vector3(Random.Range(minInitVelocity.x, maxInitVelocity.x), Random.Range(minInitVelocity.y, maxInitVelocity.y), Random.Range(minInitVelocity.z, maxInitVelocity.z));
        StartCoroutine(nameof(Timer));
        Effect.SendEvent("StartEffect");
    }

    public void Update()
    {
        acceleration = 2f / (time * time) * (targetDirection - position - time * velocity);

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
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("!Enemy!Hit");
            Destroy(gameObject);
        }
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(lifeTime);

        Destroy(gameObject);
    }
}


