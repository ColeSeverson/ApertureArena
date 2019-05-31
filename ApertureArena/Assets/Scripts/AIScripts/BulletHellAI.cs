using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHellAI : MonoBehaviour
{
    public Transform parent;
    public GameObject bullet;
    public Transform bulletSpawnArea; // an empty on the weapon

    public float fireRate = 1f;
    public float projectileSpeed = 15f;

    private float fireDelay = 0f;

    //float temp = 0;

    private Pattern[] patterns;

    // Update is called once per frame
    void Start(){
      patterns = new Pattern[] {
        new Nova(),
        new Spray()
      };
    }
    void Update()
    {

    }
    public void fire(Vector3 direction, float bS) {
      bullet = Instantiate(bullet, bulletSpawnArea.position, new Quaternion(0, 0, 0, 0));
      bullet.GetComponent<Rigidbody>().AddForce(direction * bS);
      Destroy(bullet, 10f);
    }
    public void Shoot()
    {
        if (fireDelay <= 0)
        {
          float index = Random.Range(0f, patterns.Length);
          fireDelay = patterns[(int)index].Execute(this, transform.forward,  projectileSpeed, fireRate, 4f);
        }
        else
        {
            fireDelay -= Time.deltaTime;
        }
    }
}
