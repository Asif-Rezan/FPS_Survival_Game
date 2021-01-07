using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBowScript : MonoBehaviour
{
    private Rigidbody myBody;
    public float speed = 30f;
    public float deactive_Timer = 3f;
    public float damage = 15f;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody>();
    }


    void Start()
    {
        Invoke("DeactivateGameObject", deactive_Timer);
    }
    public void Launch(Camera mainCamera)
    {
        myBody.velocity = mainCamera.transform.forward * speed;
        transform.LookAt(transform.position + myBody.velocity);
    }

    void DeactivateGameObject()
    {
        if(gameObject.activeInHierarchy)
        {
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider target)
    {
        if(target.tag==Tags.ENEMY_TAG)
        {
            target.GetComponent<HealthScript>().ApplyDamage(damage);
            gameObject.SetActive(false);
        }
    }


}
