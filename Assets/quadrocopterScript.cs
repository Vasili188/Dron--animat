﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadrocopterScript : MonoBehaviour
{
    //public Vector3 Force;
    private Rigidbody rb;
    public double movementAcceleration;
    public float maxVeclocity;
    public Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    void pickTargets(Vector3[] targets)
    {

    }

    void MoveToTarget(Vector3 target)
    {
        Vector3 movingVector = target - rb.position;
        MoveInDirection(movingVector, movementAcceleration, maxVeclocity);
        //Debug.Log("Vector to target" + movingVector.ToString());
        //Debug.Log("Velocity after adding force" + rb.velocity.ToString());
    }


    void MoveInDirection(Vector3 direction, double movementAcceleration, double speed)
    {
        direction = direction.normalized;
        Vector3 force = direction * (float)movementAcceleration;
        //force.y = -force.y;
        rb.AddForce(force);
        Debug.Log("Direction:" + direction.ToString() + ("Force:" + force.ToString()) + "Velocity:" + rb.velocity.ToString() + "Velocity magn:" + rb.velocity.magnitude);
        //Debug.Log("Force:" + force.ToString());
        //Debug.Log("Velocity magn:" + rb.velocity.magnitude);
        if (rb.velocity.magnitude > speed)
        {
            Debug.Log("vel >> maxspeed");
            rb.velocity = rb.velocity.normalized * (float)speed;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveToTarget(target);
        //Move(new Vector3(0, 10, 10));
    }
}