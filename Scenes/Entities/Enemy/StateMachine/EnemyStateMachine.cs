using Godot;
using System;

public partial class EnemyStateMachine : StateMachine
{
    public static class States
    {
        public const string Active = "EnemyActive";
        public const string Idle = "EnemyIdle";
    }

    public override void _Ready()
    {
        base._Ready();
    }
}