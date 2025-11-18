using Godot;
using System;

public partial class PlayerStateMachine : StateMachine
{
    public static class States
    {
        public const string Immobile = "PlayerImmobile";
        public const string Moving = "PlayerMoving";
        public const string Idle = "PlayerIdle";
    }

    public override void _Ready()
    {
        base._Ready();

        ScaffoldManager.Instance.ScaffoldingStarted += OnScaffoldingStarted;

        ScaffoldManager.Instance.ScaffoldingEnded += OnScaffoldingEnded;
    }

    private void OnScaffoldingStarted()
    {
        CurrentState?.EmitSignal(State.SignalName.TransitionState, CurrentState, States.Immobile);
    }

    private void OnScaffoldingEnded()
    {
        CurrentState?.EmitSignal(State.SignalName.TransitionState, CurrentState, States.Idle);
    }

    public static Vector2 GetInputVector()
    {
        var inputVector = Vector2.Zero;

        if (Input.IsActionPressed(InputActions.UiRight)) inputVector.X += 1;

        if (Input.IsActionPressed(InputActions.UiLeft)) inputVector.X -= 1;

        if (Input.IsActionPressed(InputActions.UiUp)) inputVector.Y -= 1;

        if (Input.IsActionPressed(InputActions.UiDown)) inputVector.Y += 1;

        inputVector = inputVector.Normalized();

        return inputVector;
    }
}