using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (timer == false)
            return;
        Debug.Log("collision");
        Destroy(gameObject);
    }
}
