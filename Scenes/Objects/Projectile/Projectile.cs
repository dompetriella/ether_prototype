using Godot;
using System;

public partial class Projectile : Node2D
{
    private Vector2 _direction = Vector2.Up;

    [Export]
    public Area2D Area2D;

    [Export]
    public int Damage = 5;

    [Export]
    public float Speed { get; set; } = 200f;

    public override void _Ready() {
        base._Ready();

        Area2D.BodyEntered += OnBodyEntered;
    }

    public override void _Process(double delta)
    {
        Position += _direction * (float)delta * Speed;
    }

    private void OnBodyEntered(Node body)
    {
        if (body is Enemy enemy)
        {

            var enemyHealth = enemy.TakeDamage(damage: Damage);
            QueueFree();
            if (enemyHealth <= 0)
            {
                enemy.Die();
            }
        }
    }
}
