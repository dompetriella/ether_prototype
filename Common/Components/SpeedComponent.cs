using Godot;

[GlobalClass]
public partial class SpeedComponent : Node
{
    // Useful for running methods on a node
    // without needing to search the Scene Tree
    [Export]
    public Node ComponentOwner;

    [Export]
    public float Speed = 300.0f;
}
