using UnityEngine;

public class QuadrocopterScript : MonoBehaviour
{
    public Rigidbody RB;
    private double movementAcceleration;
    public float maxVeclocity;

    public StateMachine SM;
    public ChargingState charging;
    public GrazingState grazing;
    public AlertnessState alertness;
    public Vector3[] fieldBorders;

    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody>();
        movementAcceleration = 5;
        maxVeclocity = 5;
        SM = new StateMachine();

        charging = new ChargingState(this, SM);
        alertness = new AlertnessState(this, SM);
        fieldBorders = new Vector3[2] { new Vector3(0, 0, 0),
                                        new Vector3(100, 0, 100)}; //test Borders
        grazing = new GrazingState(this, SM, fieldBorders);
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
