using Enums;
using Godot;
using System;

public partial class EnemyProjectile : Node2D
{
    [Export]
    public SpeedComponent SpeedComponent;

    [Export]
    public Area2D Area2D;

    public PolarityState polarityState;

    private Vector2 direction = Vector2.Zero;

    public void Initialize(Vector2 moveDirection)
    {
        direction = moveDirection;
    }

    public override void _Ready()
    {
        base._Ready();

        Area2D.BodyEntered += OnBodyEntered;
    }

    public override void _Process(double delta)
    {
        base._Process(delta);

        if (SpeedComponent != null)
            Position += direction * SpeedComponent.Speed * (float)delta;

    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
    }

    public void OnBodyEntered(Node body)
    {
        if (body is Player player)
        {
            QueueFree();
        }

        if (body is StaticBody2D)
        {
            QueueFree();
        }
    }

    public void SetPolarity(PolarityState state)
    {

        polarityState = state;

        switch (state)
        {
            case PolarityState.Red:
                Modulate = Colors.Red;
                break;

            case PolarityState.Blue:
                Modulate = Colors.Blue;
                break;
        }
    }

}
