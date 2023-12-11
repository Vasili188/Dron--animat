//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class LyingState : State
//{
//    public LyingState(QuadrocopterScript character, StateMachine stateMachine) : base(character, stateMachine)
//    {
//    }

//    public override void Enter()
//    {
//        base.Enter();
//        if (character.GetComponent<CharacterController>().isGrounded)
//        {
//            character.pickTargets;
//        }
//    }

//    public override void Exit()
//    {
//        base.Exit();

//    }

//    public override void HandleInput()
//    {
//        base.HandleInput();
//    }

//    public override void LogicUpdate()
//    {
//        base.LogicUpdate();
//    }

//    public override void PhysicsUpdate()
//    {
//        base.PhysicsUpdate();
//        character.MoveInDirection(moveDirection, movementAcceleration, maxSpeed);
//    }
//}
