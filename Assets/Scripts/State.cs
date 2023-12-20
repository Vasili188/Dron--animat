using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected QuadrocopterScript quadrocopter;
    protected StateMachine stateMachine;

    protected State(QuadrocopterScript quadrocopter, StateMachine stateMachine)
    {
        this.quadrocopter = quadrocopter;
        this.stateMachine = stateMachine;
    }

    protected void Display(State state)
    {
        Debug.Log(state.GetType().Name);
    }

    public virtual void Enter()
    {
        Display(this);
    }

    public virtual void HandleInput()
    {

    }

    public virtual void LogicUpdate()
    {

    }

    public virtual void PhysicsUpdate()
    {

    }

    public virtual void Exit()
    {

    }
}
