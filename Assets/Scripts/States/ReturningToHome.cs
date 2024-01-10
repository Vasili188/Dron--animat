using UnityEngine;
public class ReturningToHomeState : State
{
    private Vector3 home;
    private bool homeReached { get { return (quadrocopter.RB.position - home).magnitude < 1; } }
    public ReturningToHomeState(QuadrocopterScript quadrocopter, StateMachine stateMachine, Vector3 home) : base(quadrocopter, stateMachine)
    {
        this.home = home;
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
        quadrocopter.MoveToTarget(home);
        if (homeReached)
        {
            quadrocopter.RB.velocity = Vector3.zero;
            stateMachine.ChangeState(quadrocopter.awaiting);
        }
    }
}

