using System;
using System.Collections.Generic;
using UnityEngine;

public class QuadrocopterScript : MonoBehaviour
{
    private Rigidbody rb;
    private double movementAcceleration;
    private float maxVeclocity;
    private Vector3[] targets;
    public int currentTargetIndex;
    private bool targetsTaken;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        movementAcceleration = 5;
        maxVeclocity = 7;
        targets = new Vector3[8] { new Vector3(50, 15, -10), new Vector3(-50, 15, -10), new Vector3(-50, 15, 30), new Vector3(50, 15, 30), new Vector3(50, 15, -30), new Vector3(-50, 15, -30), new Vector3(-50, 15, 30), new Vector3(50, 15, 30) };
        currentTargetIndex = 0;
        targetsTaken = false;
    }


    void pickTargets(Vector3[] targets)
    {
        if (targetsTaken)
            return;

        Vector3 toTarget = targets[currentTargetIndex] - rb.position;
        // Debug.Log("targetsLength:"+targets.Length.ToString() + "curTargIndex:" + currentTargetIndex.ToString() + "distToTarget" + toTarget.magnitude.ToString());
        if (toTarget.magnitude < 3)
        {
            currentTargetIndex++;
            System.Threading.Thread.Sleep(500);        
        }
        if (currentTargetIndex >= targets.Length)
        {
            targetsTaken = true;
            return;
        }
        MoveToTarget(targets[currentTargetIndex]);
    }

    void MoveToTarget(Vector3 target)
    {
        Vector3 movingVector = target - rb.position;
        MoveInDirection(movingVector, movementAcceleration, maxVeclocity);
    }


    void MoveInDirection(Vector3 direction, double movementAcceleration, double speed)
    {
        direction = direction.normalized;
        Vector3 force = direction * (float)movementAcceleration;
        rb.AddForce(force);
        if (rb.velocity.magnitude > speed)
        {
            rb.velocity = rb.velocity.normalized * (float)speed;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        pickTargets(targets);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "WaterTrigger")
        {
            WaterTrigger();
        }
    }

    void WaterTrigger()
    {
        Debug.Log("В зоне воды");
        var newTargets = new List<Vector3> { new Vector3(rb.position.x, 11, rb.position.z) };
        for (var i = currentTargetIndex + 1; i < targets.Length; i++)
        {
            newTargets.Add(targets[i]);
        }
        targets=newTargets.ToArray();
    //     targets[currentTargetIndex] = new Vector3(rb.position.x, 9, rb.position.z);
        FixedUpdate(); 
    }
}
