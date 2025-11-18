using Godot;
using System;

[GlobalClass]
public partial class HealthComponent : Node
{

    // Useful for running methods on a node
    // without needing to search the Scene Tree
    [Export]
    public Node ComponentOwner;

    [Export]
    public int MaxHealth = 200;

    public int CurrentHealth;

    public override void _Ready()
    {
        base._Ready();

        CurrentHealth = MaxHealth;
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    public void HealDamage(int healing)
    {
        CurrentHealth = Math.Clamp(CurrentHealth + healing, 0, MaxHealth);
    }

    public void Die()
    {
        if (ComponentOwner != null)
        {
            ComponentOwner.QueueFree();
        }
        else
        {
            GetParent().QueueFree();
        }
    }
}
