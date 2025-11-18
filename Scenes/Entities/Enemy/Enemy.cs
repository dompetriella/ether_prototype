using Godot;
using System;

public partial class Enemy : Entity
{
    [Export]
    public HealthComponent HealthComponent;

    [Export]
    public SpeedComponent SpeedComponent;

    [Export]
    public StateMachine StateMachine;

    public override void _Ready() {
        base._Ready();
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        MoveAndSlide();
    }

}
