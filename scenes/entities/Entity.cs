using Godot;
using System;

public abstract partial class Entity : CharacterBody2D
{
    [Export]
    public int TotalHealth;

    public int CurrentHealth { get; private set; }

    public override void _Ready()
    {
        base._Ready();

        CurrentHealth = TotalHealth;
    }

    public int TakeDamage(int damage)
    {
        GD.Print(CurrentHealth);
        CurrentHealth -= damage;
        GD.Print(CurrentHealth);
        return CurrentHealth;
    }

    public void Die()
    {
        QueueFree();
    }
}