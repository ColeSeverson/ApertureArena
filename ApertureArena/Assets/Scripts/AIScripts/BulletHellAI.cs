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
    private Pattern[] patterns;

    // Update is called once per frame
    void Start(){
      patterns = new Pattern[] {
        new Nova()
      };
    }
    void Update()
    {

    }
    public void fire(Vector3 direction, float bS) {
      bullet = Instantiate(bullet, bulletSpawnArea.position, new Quaternion(90, 0, 0, 0));
      bullet.GetComponent<Rigidbody>().AddForce(direction * bS);
      Destroy(bullet, 10f);
    }
    public void Shoot()
    {
        if (temp >= fireRate)
        {
          float index = Random.Range(0f, patterns.Length);
          patterns[(int)index].Execute(this, transform.forward,  projectileSpeed, fireRate);
          temp = 0;
        }
        else
        {
            temp += Time.deltaTime;
        }
    }
}
