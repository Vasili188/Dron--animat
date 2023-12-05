using UnityEngine;

public class QuadrocopterScript : MonoBehaviour
{
    //public Vector3 Force;
    private Rigidbody rb;
    private double movementAcceleration;
    private float maxVeclocity;
    private Vector3[] targets;
    private int currentTargetIndex;
    private bool targetsTaken;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        movementAcceleration = 5;
        maxVeclocity = 10;
        targets = new Vector3[10] { new Vector3(50, 15, 20), new Vector3(50, 35, -10), new Vector3(50, 15, 20), new Vector3(50, 35, -10), new Vector3(50, 15, 20), new Vector3(50, 35, -10), new Vector3(50, 15, 20), new Vector3(50, 35, -10), new Vector3(50, 15, 20), new Vector3(50, 35, -10), };
        currentTargetIndex = 0;
        targetsTaken = false;
    }


    void pickTargets(Vector3[] targets)
    {
        Vector3 toTarget = targets[currentTargetIndex] - rb.position;
        Debug.Log("targetsLength:"+targets.Length.ToString() + "curTargIndex:" + currentTargetIndex.ToString() + "distToTarget" + toTarget.magnitude.ToString());
        if (toTarget.magnitude < 5)
            currentTargetIndex++;
        if (currentTargetIndex >= targets.Length)
        {
            targetsTaken = true;
            return;
        }
        MoveToTarget(targets[currentTargetIndex]);

        //for (var i = 0; i < targets.Length; i++)
        //{
        //    MoveToTarget(targets[i]);

            //    if ((targets[i] - rb.position).magnitude < 1 )  continue;
            //}
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

        //Debug.Log("Direction:" + direction.ToString() + ("Force:" + force.ToString()) + "Velocity:" + rb.velocity.ToString() + "Velocity magn:" + rb.velocity.magnitude);
        //Debug.Log("Force:" + force.ToString());
        //Debug.Log("Velocity magn:" + rb.velocity.magnitude);
        if (rb.velocity.magnitude > speed)
        {
            //Debug.Log("vel >> maxspeed");
            rb.velocity = rb.velocity.normalized * (float)speed;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!targetsTaken)
            pickTargets(targets);
        else
            rb.velocity = new Vector3(0,0,0);
        //MoveToTarget(new Vector3(50, 15, -30));
        //Move(new Vector3(0, 10, 10));
    }
}
