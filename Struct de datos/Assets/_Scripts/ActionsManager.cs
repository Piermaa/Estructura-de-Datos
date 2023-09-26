using System;
using System.Collections.Generic;
using UnityEngine;

public static class ActionKeys
{
    public const string ENEMY_DEATH_KEY ="EnemyDeath";
    public const string PLAYER_DEATH_KEY ="PlayerDeath";
}

public static class ActionsManager
{
    private static Dictionary<string, Action> actions = new();

    public static void InvokeAction(string name)
    {
        if (!actions.ContainsKey(name))
        {
            Debug.LogWarning($"Tried to invoke {name}, but no action was found");
            return;
        }

        actions[name]?.Invoke();
    }

    public static void RegisterAction(string name)
    {
        if (actions.ContainsKey(name))
        {
            Debug.LogWarning($"Tried to register {name}, but it is already registered");
            return;
        }

        actions.Add(name,() => { });
    }

    public static void SubscribeToAction(string name, Action action)
    {
        if (!actions.ContainsKey(name))
        {
            RegisterAction(name);
        }

        actions[name] += action;
    }

    public static void UnsubscribeToAction(string name, Action action)
    {
        if (!actions.ContainsKey(name))
        {
            Debug.LogWarning($"Tried to unsubscribe to {name}, but no action was found");
            return;
        }

        actions[name] -= action;
    }

    public static void DeleteAction(string name)
    {
        if (!actions.ContainsKey(name))
        {
            Debug.LogWarning($"Tried to delete {name}, but no action was found");
            return;
        }

        actions[name] = null;
        actions.Remove(name);
    }

    public static void DeleteAllActions()
    {
        foreach (var item in actions)
        {
            DeleteAction(item.Key);
        }
    }
/// <summary>
/// Entre niveles se van a mantener las acciones y no queremos asi q lo reseteo en el awake del game manager
/// </summary>
    public static void ResetManager()
    {
        actions = new();
    }
}