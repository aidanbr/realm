namespace Core;

using Godot;

enum EntityType
{
  Unset = 0,
  Player = 1,
  Character = 2,
  Monster = 3,
  Item = 4,
}

class Entity
{
  public Vector3 Position { get; set; }
  public EntityType Type { get; init; }
  public int OwnerIdx { get; set; }
  public int NextIdx { get; set; }
  public int OwnedIdx { get; set; }

  public override string ToString() => $"{Type} {OwnerIdx} {NextIdx} {OwnedIdx}";
}
