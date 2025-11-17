using Enums;
using Godot;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;

public partial class Player : Entity
{
    [Export]
    public Label StateLabel;

    [Export]
    public Marker2D Barrel1;

    [Export]
    public Marker2D Barrel2;

    [Export]
    public StateMachine StateMachine;

    [Export]
    public PackedScene Projectile;

    [Export]
    public Timer PrimaryWeaponProjectileCooldown;

    [Export]
    public Timer SecondaryWeaponProjectileCooldown;

    public PolarityState polarityState = PolarityState.Blue;

    public override void _Ready()
    {
        base._Ready();

        AssignPolarity(polarityState);
    }


    public override void _Process(double delta)
    {
        base._Process(delta);

        StateLabel.Text = StateMachine.CurrentState.Name;

        FlipPolarity();
        FirePrimaryWeapon();
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        MoveAndSlide();
    }

    private void FlipPolarity()
    {
        if (Input.IsActionJustPressed(InputActions.UiFocusNext))
        {
            switch (polarityState)
            {
                case PolarityState.Red:
                    AssignPolarity(PolarityState.Blue);
                    break;
                case PolarityState.Blue:
                    AssignPolarity(PolarityState.Red);
                    break;
            }
        }
    }

    private void FirePrimaryWeapon()
    {
        if (Input.IsActionPressed(InputActions.UiAccept))
        {

            if (PrimaryWeaponProjectileCooldown.IsStopped())
            {
                var leftBarrelStart = Barrel1.GlobalPosition;
                var rightBarrelStart = Barrel2.GlobalPosition;

                var leftProjectile = Projectile.Instantiate<Node2D>();
                var rightProjectile = Projectile.Instantiate<Node2D>();

                leftProjectile.GlobalPosition = leftBarrelStart;
                rightProjectile.GlobalPosition = rightBarrelStart;

                GetTree().CurrentScene.AddChild(leftProjectile);
                GetTree().CurrentScene.AddChild(rightProjectile);

                PrimaryWeaponProjectileCooldown.Start();
            }
        }
    }

    private void AssignPolarity(PolarityState state)
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