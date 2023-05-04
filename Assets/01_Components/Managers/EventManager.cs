using System;

public static partial class EventManager
{
    public static event Action OnResetGame;
    public static void Fire_OnResetGame() { OnResetGame?.Invoke(); }

    public static event Action<VfxType> OnPlayVfxOneShot;
    public static void Fire_OnPlayVfxOneShot(VfxType type) { OnPlayVfxOneShot?.Invoke(type); }

    public static event Action OnCheckMoney;
    public static void Fire_OnCheckMoney() { OnCheckMoney?.Invoke(); }
}
