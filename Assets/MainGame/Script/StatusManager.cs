using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StatusManager : MonoBehaviour
{
    public GameObject Main;
    public int HP;
    public int MaxHP;
    public float ResetTime = 0;
    public Image HPGage;

    public GameObject Effect;
    public AudioSource AudioSource;
    public AudioClip HitSE;
    private new Collider collider;

    public string TagName;

    void Start()
    {
        collider= GetComponent<Collider>(); 
    }

    private void Update()
    {
        if (HP <= 0)
        {
            HP = 0;
            var effect = Instantiate(Effect);
            effect.transform.position = transform.position;
            Destroy(effect, 5);
            Destroy(Main);
        }

        float percent=(float)HP/MaxHP;
        HPGage.fillAmount = percent;
    }
    //“–‚½‚è”»’è
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == TagName)
        {
            Damage();
            collider.enabled = false;
            Invoke("ColliderReset", ResetTime);
        }
    }

    void Damage()
    {
        AudioSource.PlayOneShot(HitSE);
        HP--;
    }

    void ColliderReset()
    {
        collider.enabled = true;
    }
}
