using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHellAI : MonoBehaviour
{
    public Transform parent;
    public GameObject bullet;
    public Transform bulletSpawnArea; // an empty on the weapon
    public float fireRate;
    public float projectileSpeed = 15;
    float temp = 0;
   
    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot()
    {
        if (temp >= fireRate)
        {
            bullet = Instantiate(bullet, bulletSpawnArea.position, new Quaternion(90, 0, 0, 0));
            bullet.GetComponent<Rigidbody>().AddForce(parent.forward * projectileSpeed);
            Destroy(bullet, 10f);
            temp = 0;
        }
        else
        {
            temp += Time.deltaTime;
        }
    }
}
