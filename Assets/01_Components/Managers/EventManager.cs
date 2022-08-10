using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public static partial class EventManager
{
    public static event Action OnResetGame;
    public static void Fire_OnResetGame() { OnResetGame?.Invoke(); }
}
