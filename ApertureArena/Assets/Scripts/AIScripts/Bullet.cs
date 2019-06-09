using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script exists so that the bullets created during the AI shooting will get despawned.
public class Bullet : MonoBehaviour
{
    private bool timer;

    private void Start()
    {
        timer = false;
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(.5f);
        timer = true;
    }

    private void OnTriggerEnter(Collider collision)
    {

        if (timer == false && collision.gameObject.tag == "Bullet")
            return;

        Destroy(gameObject);
    }
}
