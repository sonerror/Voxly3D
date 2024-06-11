using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager
{
    public static  Action OnLoadNewScene;

    public static void TriggerLoadNewScene()
    {
        OnLoadNewScene?.Invoke();
    }
}
