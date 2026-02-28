namespace Core;

using Godot;
using System;

static class Nodes
{
  internal static Character Character()
    => Create<Character>("res://Node/Entity/Character.tscn");

  internal static InteractRange InteractRange()
    => Create<InteractRange>("res://Node/Component/InteractRange.tscn");

  internal static Zone Zone()
    => Create<Zone>("res://Node/World/Zone.tscn");

  internal static Interact Interact()
  => Create<Interact>("res://Node/Component/Interact.tscn");

  static T Create<T>(string path) where T : Godot.Node
  {
    PackedScene node = ResourceLoader.Load<PackedScene>($"{path}") ??
                       throw new ArgumentException($"No node was found at {path} for {typeof(T)}");
    return node.Instantiate() as T ?? throw new ArgumentException($"Node at {path} is not of type {typeof(T)}");
  }
}
