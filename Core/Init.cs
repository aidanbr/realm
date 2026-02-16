namespace Core;

using Godot;
using System.Collections.Generic;

public partial class Init : Node
{
  // data
  const int MaxEntity = 1024;
  readonly Entity[] _entity = new Entity[MaxEntity];
  readonly Queue<int> _slots = new(MaxEntity);
  readonly bool[] _used = new bool[MaxEntity];

  // player data
  (int ID, InteractRange interactRange) _player;

  // godot
  readonly Dictionary<int, Character> _active = new(MaxEntity);

  public override void _Ready()
  {
    Reset();

    _player = InitPlayer();
  }

  public override void _Process(double delta)
  {
    // player 
    Vector2 v = Input.GetVector(Inputs.Left, Inputs.Right, Inputs.Forward, Inputs.Back);

    if (!v.IsZeroApprox())
    {
      Entity player = Get(_player.ID);
      player.Position += new Vector3 { X = v.X, Z = v.Y } * 2 * (float)delta;
    }

    if (Input.IsActionJustReleased(Inputs.Interact))
    {
      Interact? interact = _player.interactRange.Selected;

      if (interact is not null)
      {
        Log.Info($"Interacted with {interact.Id}");
      }
    }

    // loop
    for (int i = 1; i < _entity.Length; ++i)
    {
      if (!Used(i)) continue;

      // movement
      if (_active.TryGetValue(i, out Character? node))
      {
        node.Position = _entity[i].Position;
      }
    }
  }

  (int, InteractRange) InitPlayer()
  {
    // data
    int id = Add(new Entity { Type = EntityType.Player });

    // godot
    Character character = Nodes.Character();
    character.AddChild(new Camera3D { Position = new Vector3 { X = 0, Y = 1, Z = 2} } );
    InteractRange interactRange = Nodes.InteractRange();
    character.AddChild(interactRange);
    AddChild(character);

    _active.Add(id, character);

    return (id, interactRange);
  }

  void Reset()
  {
    for (int i = 1; i < _entity.Length; ++i)
    {
      _slots.Enqueue(i);
      _entity[i] = new Entity();
      _used[i] = false;
    }
  }

  int Add(Entity entity)
  {
    if (!_slots.TryDequeue(out int idx))
    {
      Log.Warning($"Could not add entity: {entity.Type}");
      return 0;
    }
    _entity[idx] = entity;
    _used[idx] = true;
    return idx;
  }

  void Remove(int idx)
  {
    if (idx is <= 0 or >= MaxEntity)
    {
      Log.Warning($"Could not remove entity: {idx}");
    }

    _entity[idx] = new Entity();
    _used[idx] = false;
    _slots.Enqueue(idx);
  }

  bool Used(int idx)
    => idx is > 0 and < MaxEntity && _used[idx];

  Entity Get(int idx)
    => Used(idx) ? _entity[idx] : _entity[0];

  void SetOwner(int ownerId, int id)
    => Get(ownerId).OwnedIdx = id;
}
