using Godot;
using System;


public partial class EnemyIdle : State
{
    [Export] public Entity Entity;

    public override void _Ready()
    {
        base._Ready();

        GetTree().CreateTimer(timeSec: 2).Timeout += onTimerTimeout;
    }

    private void onTimerTimeout()
    {
        EmitSignal(State.SignalName.TransitionState, this, EnemyStateMachine.States.Active);
    }
}