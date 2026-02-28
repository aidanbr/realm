namespace Core;

using Godot;

partial class Zone : Node3D
{
  public override void _Ready()
  {
    AddInteract(22, Vector3.Zero);
  }

  void AddInteract(int ID, Vector3 position)
  {
    Interact interact = Nodes.Interact();
    interact.ID = ID;
    interact.Position = position;
    AddChild(interact);
  }
}
