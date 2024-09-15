using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public static class Logger
{
    [Conditional("DEBUG")]
    public static void Log(object message, Object context = null)
    {
        Debug.Log(message, context);
    }

    [Conditional("DEBUG")]
    public static void LogWarning(string message, Object context = null)
    {
        Debug.LogWarning(message, context);
    }

    [Conditional("DEBUG")]
    public static void LogError(string message, Object context = null)
    {
        Debug.LogError(message, context);
    }

    [Conditional("DEBUG")]
    public static void LogTag(this MonoBehaviour mono, object message, Object context = null)
    {
        Debug.Log($"[{mono.GetType().Name.Bold()}] {message}", context);
    }

    [Conditional("DEBUG")]
    public static void LogTagWarning(this MonoBehaviour mono, object message, Object context = null)
    {
        Debug.LogWarning($"[{mono.GetType().Name.Bold()}] {message}", context);
    }

    [Conditional("DEBUG")]
    public static void LogTagError(this MonoBehaviour mono, object message, Object context = null)
    {
        Debug.LogError($"[{mono.GetType().Name.Bold()}] {message}", context);
    }
}