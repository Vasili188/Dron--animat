using UnityEngine;

public class GrazingState : State
{
    //private static Random random = new Random();
    private bool waypointsTaken;
    private int currentWaypointIndex;
    private Vector3[] waypoints;

    public GrazingState(QuadrocopterScript quadrocopter, StateMachine stateMachine, Vector3[] fieldBorders) : base(quadrocopter, stateMachine)
    {
        waypointsTaken = false;
        currentWaypointIndex = 0;
        waypoints = new Vector3[3] { GetRandomPoint(fieldBorders[0], fieldBorders[1]),
                                     GetRandomPoint(fieldBorders[0], fieldBorders[1]),
                                     GetRandomPoint(fieldBorders[0], fieldBorders[1])
                                    };
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void HandleInput()
    {
        base.HandleInput();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        pickWaypoints();
    }


    public static Vector3 GetRandomPoint(Vector3 corner1, Vector3 corner2)
    {

        float randomX = Random.Range((float)corner1.x, (float)corner2.x);
        float randomY = Random.Range((float)corner1.y, (float)corner2.y);
        float randomZ = Random.Range((float)corner1.z, (float)corner2.z);

        return new Vector3(randomX, randomY, randomZ);
    }

    public void pickWaypoints()
    {
        if (waypointsTaken)
        {
            if (quadrocopter.RB.velocity.magnitude != 0)
            {
                quadrocopter.RB.velocity = new Vector3(0, 0, 0);
                stateMachine.ChangeState(quadrocopter.charging);
            }
            return;
        }

        Vector3 toTarget = waypoints[currentWaypointIndex] - quadrocopter.RB.position;
        
        if (toTarget.magnitude < 3)
        {
            Debug.Log(string.Format("Taken waypoint #{0}", currentWaypointIndex));
            currentWaypointIndex++;
        }
        if (currentWaypointIndex >= waypoints.Length)
        {
            //Debug.Log(string.Format("all waypoints taken. waypointsCount={0}. currentWaypointIndex={1}",waypoints.Length,currentWaypointIndex));
            waypointsTaken = true;
            return;
        }
        quadrocopter.MoveToTarget(waypoints[currentWaypointIndex]);
    }

    public void WaterTrigger()
    {
        waypoints[currentWaypointIndex] = new Vector3(quadrocopter.RB.position.x, 5, quadrocopter.RB.position.z);
    }

    public void HumanTrigger()
    {
        quadrocopter.maxVeclocity *= 2;
    }

    public void OilTrigger(Vector3 target)
    {
        waypoints[currentWaypointIndex] = target;
    }

}

