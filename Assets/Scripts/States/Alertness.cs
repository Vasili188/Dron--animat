// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class AlertnessState : State
// {
//     private Vector3[] targets;
//     private int currentTargetIndex;
//     private bool safety;
//     public Vector3 triggerPosition;

//     public AlertnessState(QuadrocopterScript quadrocopter, StateMachine stateMachine) : base(quadrocopter, stateMachine)
//     {
//         var direction = (quadrocopter.RB.position - triggerPosition)*10;
//         safety = false;
//         currentTargetIndex = 0;
//         targets = new Vector3[3]{direction/3, direction*2/3 - direction/3, direction - direction*2/3};
//     }


//     public override void Enter()
//     {
//         base.Enter();
//     }

//     public override void Exit()
//     {
//         base.Exit();
//     }

//     public override void HandleInput()
//     {
//         base.HandleInput();
//     }

//     public override void LogicUpdate()
//     {
//         base.LogicUpdate();
//     }

//     public override void PhysicsUpdate()
//     {
//         base.PhysicsUpdate();
//         pickTargets();
//     }

//     public void pickTargets()
//     {
//         if (safety)
//         {
//             if (quadrocopter.RB.velocity.magnitude != 0)
//             {
//                 quadrocopter.RB.velocity = new Vector3(0, 0, 0);
//                 stateMachine.ChangeState(quadrocopter.grazing);
//             }
        
//             return;
//         }

//         Vector3 toTarget = targets[currentTargetIndex] - quadrocopter.RB.position;
        
//         if (toTarget.magnitude < 3)
//         {
//             System.Threading.Thread.Sleep(200);
//             currentTargetIndex++;
//         }
//         if (currentTargetIndex >= targets.Length)
//         {
//             //Debug.Log(string.Format("all waypoints taken. waypointsCount={0}. currentWaypointIndex={1}",waypoints.Length,currentWaypointIndex));
//             safety = true;
//             return;
//         }
//         quadrocopter.MoveToTarget(targets[currentTargetIndex]);    
//     }
// }