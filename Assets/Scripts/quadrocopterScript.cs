﻿using UnityEngine;

public class QuadrocopterScript : MonoBehaviour
{
    public Rigidbody RB;
    private double movementAcceleration;
    public float maxVeclocity;

    public StateMachine SM;
    public ChargingState charging;
    public GrazingState grazing;
    public AlertnessState alertness;
    public (Vector3, Vector3)[] fieldsBorders;

    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody>();
        movementAcceleration = 5;
        maxVeclocity = 3;
        SM = new StateMachine();

        charging = new ChargingState(this, SM);
        alertness = new AlertnessState(this, SM);
        fieldsBorders = new (Vector3, Vector3)[2] {
                                                    (new Vector3(-35,12,-25),new Vector3(28,12,22)),
                                                    (new Vector3(-76, 10, -235),new Vector3(71, 10, -77))
                                                   }; //test Borders
        grazing = new GrazingState(this, SM, fieldsBorders);
        SM.Initialize(grazing);
    }

    public void MoveToTarget(Vector3 target)
    {
        Vector3 movingVector = target - RB.position;
        MoveInDirection(movingVector, movementAcceleration, maxVeclocity);
    }


    public void MoveInDirection(Vector3 direction, double movementAcceleration, double speed)
    {
        direction = direction.normalized;
        Vector3 force = direction * (float)movementAcceleration;
        RB.AddForce(force);
        if (RB.velocity.magnitude > speed)
        {
            RB.velocity = RB.velocity.normalized * (float)speed;
        }
    }

    private void Update()
    {

        SM.CurrentState.HandleInput();

        SM.CurrentState.LogicUpdate();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        SM.CurrentState.PhysicsUpdate();
    }

    void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "WaterTrigger":
                grazing.WaterTrigger();
                break;
            case "OilTrigger":
                grazing.OilTrigger(other.GetComponent<SphereCollider>().transform.position);
                break;
            case "HumanTrigger":
                grazing.HumanTrigger(other.GetComponent<CapsuleCollider>().transform.position);
                break;
        }
    }

}