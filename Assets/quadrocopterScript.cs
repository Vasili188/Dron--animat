using UnityEngine;

public class QuadrocopterScript : MonoBehaviour
{
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
        if (targetsTaken)
            return;

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
}
