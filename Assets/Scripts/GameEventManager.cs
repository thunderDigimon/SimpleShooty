using UnityEngine;
using System.Collections.Generic;

public enum GameEvent
{
    POINTER_STATUS
}

public class GameEventManager : Singleton<GameEventManager>
{
    public delegate void GameEventObj(object obj);

    //-- Generic Game Events 
    private Dictionary<GameEvent, GameEventObj> observerDict;
    //-----------------------

    private static GameEventManager sGlobalRef = null;

    private static string TAG = "GameEventManager_";

    private GameEventManager()
    {
        observerDict = new Dictionary<GameEvent, GameEventObj>();
    }

    public void Destroy()
    {
        sGlobalRef = null;
        observerDict = null;
    }

    public void RegisterEventObserver(GameEvent actionName, GameEventObj observer)
    {
        if (observerDict.ContainsKey(actionName))
        {
            observerDict[actionName] += observer;
        }
        else
        {
            observerDict.Add(actionName, observer);
        }
    }

    public void UnregisterEventObserver(GameEvent actionName, GameEventObj observer)
    {
        if (observerDict.ContainsKey(actionName))
        {
            GameEventObj observers = observerDict[actionName];
            observers -= observer;
            if (observers == null)
            {
                observerDict.Remove(actionName);
            }
        }
        else
        {
            Debug.Log(TAG + "Removing from unregisterd event '" + actionName + "'");
        }
    }

    public void TriggerEvent(GameEvent actionName)
    {
        TriggerEvent(actionName, null);
    }

    public void TriggerEvent(GameEvent actionName, object data)
    {
        if (observerDict.ContainsKey(actionName))
        {
            observerDict[actionName].Invoke(data);
        }
        else
        {
            Debug.Log(TAG + "No observers for action '" + actionName + "'");
        }
    }

}
