using Godot;
using System;

public partial class GameScene : Node
{

    [Export]
    public Marker2D EnemySpawn;

    public PackedScene EnemyScene;

    public override void _Ready()
    {
        base._Ready();

        EnemyScene = ResourceLoader.Load<PackedScene>("uid://dpop08al81sfj");

        Timer timer = new() { Autostart = true, WaitTime = 5.0, OneShot = false };
        timer.Timeout += spawnEnemy;
        AddChild(timer);
    }

    private void spawnEnemy()
    {
        var rng = new RandomNumberGenerator();
        Enemy enemyScene = EnemyScene.Instantiate<Enemy>();
        enemyScene.GlobalPosition = new Vector2 { X = EnemySpawn.GlobalPosition.X + rng.RandiRange(-40, 40), Y = EnemySpawn.GlobalPosition.Y + rng.RandiRange(0, -80) };

        rng.Randomize();
        var randomColor = Color.FromHsv(rng.Randf(), 1.0f, 1.0f);
        enemyScene.Modulate = randomColor;
        AddChild(enemyScene);
    }
}
