using TreeEditor;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class GrazingState : State
{
    private bool waypointsTaken;
    private int currentWaypointIndex;
    private Vector3[] waypoints;
    private bool FoundOil;
    private (Vector3,Vector3) fieldBorders; 
    private (Vector3, Vector3)[] fieldsBorders;
    private int currentField;

    public GrazingState(QuadrocopterScript quadrocopter, StateMachine stateMachine, (Vector3,Vector3)[] fieldsBorders) : base(quadrocopter, stateMachine)
    {
        this.fieldsBorders = fieldsBorders;
        currentField = 0;
        SetFieldBorders(fieldsBorders[0], 4);
        //this.fieldBorders = fieldBorders;
        //waypointsTaken = false;
        //currentWaypointIndex = 0;
        ////waypoints = new Vector3[2] { new Vector3(50, 30, 30), new Vector3(50, 10, 50) };
        //var sideSplit = 5;
        //var fieldPiecesCorners = SplitField(fieldBorders, sideSplit).ToArray();
        //waypoints = new Vector3[sideSplit*sideSplit];



        //fieldPiecesCorners
        //    .Select((pieceCorners, index) =>
        //             waypoints[index] = GetRandomPoint(pieceCorners.Item1,
        //                                               pieceCorners.Item2));

        //for (int i = 0;i < fieldPiecesCorners.Length; i++)
        //{
        //    waypoints[i] = GetRandomPoint(fieldPiecesCorners[i].Item1, fieldPiecesCorners[i].Item2);
        //}
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
                if (FoundOil)
                {
                    Debug.Log("Информация о базе противника передана");
                }
                else
                {
                    Debug.Log("В заданной области баз противника не обнаружено");
                }
                quadrocopter.RB.velocity = new Vector3(0, 0, 0);
                stateMachine.ChangeState(quadrocopter.charging);
            }
            
            return;
        }
        
        Vector3 toTarget = waypoints[currentWaypointIndex] - quadrocopter.RB.position;

        if (toTarget.magnitude < 3)
        {
            Debug.Log(string.Format("Taken waypoint #{0}, coord{1}", currentWaypointIndex, waypoints[currentWaypointIndex]));
            currentWaypointIndex++;
            //Debug.Log("Next waypoint:" + waypoints[currentWaypointIndex]);
        }
        if (currentWaypointIndex >= waypoints.Length)
        {
            //Debug.Log(string.Format("all waypoints taken. waypointsCount={0}. currentWaypointIndex={1}",waypoints.Length,currentWaypointIndex));
            
            waypointsTaken = true;

            currentField += 1;
            if (currentField >= fieldsBorders.Length) return;
            SetFieldBorders(fieldsBorders[currentField], 4);

            return;
        }
        quadrocopter.MoveToTarget(waypoints[currentWaypointIndex]);
    }

    public void WaterTrigger()
    {
        //waypoints[currentWaypointIndex] = new Vector3(quadrocopter.RB.position.x, 5, quadrocopter.RB.position.z);
    }

    public void HumanTrigger(Vector3 danger)
    {
        quadrocopter.alertness.triggerPosition = danger;
        stateMachine.ChangeState(quadrocopter.alertness);
    }

    public void OilTrigger(Vector3 target)
    {
        Debug.Log("OIL");
        FoundOil = true;
        currentWaypointIndex = waypoints.Length-1;
        waypoints[currentWaypointIndex] = target;
    }

    private IEnumerable<(UnityEngine.Vector3, UnityEngine.Vector3)> SplitField((Vector3,Vector3) fieldBorders, int sideSplitNumber) //splits field to sideSplitNum^2 pieces
    {
        var piecesCorners = new (Vector3, Vector3)[sideSplitNumber * sideSplitNumber];
        Vector3 fieldSize = fieldBorders.Item1 - fieldBorders.Item2;
        var xSideLength = Mathf.Abs(fieldSize.x / sideSplitNumber);
        var zSideLength = Mathf.Abs(fieldSize.z / sideSplitNumber);
        int height = Random.Range((int)fieldBorders.Item1.y, (int)fieldBorders.Item2.y);
        for (var i = 0; i < sideSplitNumber; i++)
        {
            for (var j = 0; j < sideSplitNumber; j++)
            {
                var xMin = i * xSideLength;
                var xMax = (i + 1) * xSideLength;

                var zMin = j * zSideLength;
                var zMax = (j + 1) * zSideLength;

                Vector3 corner1 = new Vector3(xMin, height, zMin);
                Vector3 corner2 = new Vector3(xMax, height, zMax);

                piecesCorners[i * sideSplitNumber + j] = (corner1, corner2);
            }
        }

        var absPiecesCorners = piecesCorners
                .Select(corners => (corners.Item1 = corners.Item1 + fieldBorders.Item1,
                                    corners.Item2 = corners.Item2 + fieldBorders.Item1)); //translating coordinates to absolute values
        return absPiecesCorners;
    }
    private void SetFieldBorders((Vector3,Vector3) fieldBorders, int sideSplittingNumber)
    {
        this.fieldBorders = fieldBorders;

        waypointsTaken = false;
        currentWaypointIndex = 0; 
        var fieldPiecesCorners = SplitField(fieldBorders, sideSplittingNumber).ToArray();
        waypoints = new Vector3[sideSplittingNumber * sideSplittingNumber];

        for (int i = 0; i < fieldPiecesCorners.Length; i++)
        {
            waypoints[i] = GetRandomPoint(fieldPiecesCorners[i].Item1, fieldPiecesCorners[i].Item2);
        }

        waypointsTaken = false;

        Debug.Log(fieldPiecesCorners[0].Item1);
        Debug.Log(fieldPiecesCorners[0].Item2);


        Debug.Log(fieldPiecesCorners[1].Item1);
        Debug.Log(fieldPiecesCorners[1].Item2);


        Debug.Log(fieldPiecesCorners[2].Item1);
        Debug.Log(fieldPiecesCorners[2].Item2);


        Debug.Log(fieldPiecesCorners[3].Item1);
        Debug.Log(fieldPiecesCorners[3].Item2);
    }


}

