using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHellAI : MonoBehaviour
{
    // This class creates a "bullet hell" like Ai shooting scheme. Basically it'll just fire tons of shots 
    public Transform parent;
    public GameObject bullet;
    public Transform bulletSpawnArea; // an empty on the weapon

    public float fireRate = 1f;
    public float projectileSpeed = 15f;

    private float fireDelay = 0f;

    //float temp = 0;

    private Pattern[] patterns;
    private GameObject bullet1;

    void Start(){
      patterns = new Pattern[] {
        new Nova(),
        new Cone(),
        new Wall(),
        new Spray(),
        new Wave()
      };
    }
    void Update()
    {

    }
    public void fire(Vector3 direction, float bS, float level = 0) {
      bullet1 = Instantiate(bullet, bulletSpawnArea.position + (new Vector3(0, 1, 0) * level), new Quaternion(0, 0, 0, 0));
      bullet1.GetComponent<Rigidbody>().AddForce(direction * bS);
      Destroy(bullet1, 10f);
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
