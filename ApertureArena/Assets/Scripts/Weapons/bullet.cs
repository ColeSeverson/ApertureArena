using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public Vector3 direction;

    private float speed = 10f;
    private Rigidbody rigidBody;
    private int timer = 10;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
       // rigidBody.useGravity = false;
        InvokeRepeating("Timer", 0.01f, 5.0f);

        
    }

    private void Timer()
    {
        if (timer == 0)
        {
            Destroy(this);
        }
        timer = timer - 1;
    }
    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        //rigidBody.addForce(Physics.gravity * rigidBody.mass);
        rigidBody.AddForce(direction * speed);
       // rigidBody.AddForce(Physics.gravity * rigidBody.mass);

    }
}
