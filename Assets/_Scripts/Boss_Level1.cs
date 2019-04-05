using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Level1 : MonoBehaviour
{
    [SerializeField]
    GameObject fire;

    float fireRate;
    float nextFire;

    void Start()
    {
        fireRate = 1f;
        nextFire = Time.time;
    }

    void Update()
    {
        CheckIfTimeToFire();
    }

    void CheckIfTimeToFire()
    {
        if(Time.time > nextFire)
        {
            Instantiate(fire, transform.position, Quaternion.identity);
            nextFire = Time.time + fireRate;
        }
    }




}
