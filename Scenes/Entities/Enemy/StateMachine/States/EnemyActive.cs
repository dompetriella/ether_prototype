using Enums;
using Godot;
using System;
using System.Threading.Tasks;


public partial class EnemyActive : State
{
    [Export] public Entity Entity;
    [Export] public SpeedComponent SpeedComponent;

    private Timer movementTimer;
    private Timer firingTimer;

    private Timer aliveTimer;

    private Vector2 moveDirection = Vector2.Zero;
    private RandomNumberGenerator rng = new();

    private Node2D firingNode;

    private PackedScene projectile;

    private PolarityState currentPolarity;

    public override void _Ready()
    {
        base._Ready();

        projectile = ResourceLoader.Load<PackedScene>("uid://bugrwbbeua7ah");

        aliveTimer = new() { Autostart = true, WaitTime = 20 };
        aliveTimer.Timeout += onAliveTimeTimeout;
        AddChild(aliveTimer);
        setMovementTimer();
    }

    public override void PhysicsUpdate(double delta)
    {
        base.PhysicsUpdate(delta);

        // Movement logic
        if (movementTimer != null && !movementTimer.IsStopped())
        {
            Entity.Velocity = moveDirection * SpeedComponent.Speed;
            Entity.MoveAndSlide();
        }
        else
        {
            Entity.Velocity = Vector2.Zero;
        }

        // Firing logic
        if (firingTimer != null && !firingTimer.IsStopped())
        {
            FireRandomly(delta: (float)delta, currentPolarity);
        }
    }

    private void setMovementTimer()
    {
        rng.Randomize();
        int randomValue = rng.RandiRange(1, 3);

        // Pick a random direction
        moveDirection = new Vector2(
            rng.RandfRange(-1f, 1f),
            rng.RandfRange(-1f, 1f)
        ).Normalized();

        movementTimer = new() { WaitTime = randomValue, OneShot = true };
        movementTimer.Timeout += () => setFiringTimer();
        AddChild(movementTimer);
        movementTimer.Start();
    }

    private void setFiringTimer()
    {
        rng.Randomize();

        int randomValue = rng.RandiRange(2, 4);

        firingTimer = new() { WaitTime = randomValue, OneShot = true };
        firingTimer.Timeout += () => setMovementTimer();
        currentPolarity = rng.RandiRange(0, 1) == 0
            ? PolarityState.Red
            : PolarityState.Blue;
        AddChild(firingTimer);
        firingTimer.Start();
    }

    private async void onAliveTimeTimeout()
    {
        var tween = GetTree().CreateTween();
        tween.TweenProperty(Entity, "modulate:a", 0.0f, 1.0);

        await ToSignal(tween, Tween.SignalName.Finished);

        Entity.QueueFree();
        firingNode.QueueFree();
    }

    private float fireCooldown = 0.2f; // seconds between shots
    private float timeSinceLastShot = 0f;

    private void FireRandomly(float delta, PolarityState polarityState)
    {
        // Update cooldown timer
        timeSinceLastShot -= delta;
        if (timeSinceLastShot > 0f)
            return; // still cooling down, don't fire

        if (Entity == null || projectile == null)
            return;

        // Instantiate projectile
        var projectileInstance = projectile.Instantiate<EnemyProjectile>();
        projectileInstance.GlobalPosition = Entity.GlobalPosition; // Spawn at enemy

        // Random direction
        Vector2 randomDir = new Vector2(
            rng.RandfRange(-1f, 1f),
            rng.RandfRange(-1f, 1f)
        ).Normalized();
        projectileInstance.Initialize(randomDir);

        // Random polarity
        projectileInstance.SetPolarity(polarityState);

        // Add to scene tree (as sibling, not child of enemy)
        Entity.GetParent().AddChild(projectileInstance);

        // Reset cooldown
        timeSinceLastShot = fireCooldown;
    }


}