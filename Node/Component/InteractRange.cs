namespace Core;

using Godot;
using System.Collections.Generic;
using System.Linq;

partial class InteractRange : Area3D
{
  // todo: cycle through _range
  public Interact? Selected { get; private set; }
  readonly HashSet<Interact> _range = [];

  public override void _Ready()
  {
    AreaEntered += (area) =>
    {
      if (area is Interact interact && _range.Add(interact))
      {
        Selected = interact;
      }
    };

    AreaExited += (area) =>
    {
      if (area is Interact interact && _range.Remove(interact))
      {
        if (Selected == interact)
        {
          Selected = null;
        }
      }
    };
  }
}
