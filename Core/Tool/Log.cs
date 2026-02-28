namespace Core;

using Godot;

static class Log
{
  internal static void Info(string message) => Print($"[INFO] {message}");
  internal static void Debug(string message) => Print($"[DEBUG] {message}");
  internal static void Warning(string message) => Print($"[WARNING] {message}");
  internal static void Error(string message) => Print($"[ERROR] {message}");
  static void Print(params object[] what) => GD.Print(what);
}
