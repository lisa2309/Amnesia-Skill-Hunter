using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StateController
{
    //LevelState
    public static int currentStage = 0;
    public static int currentSection = 0;

    //Playerstate
    public static bool isGodModeEnabled = false;
    public static int currentPlayerHealth;
    public static Ability currentAbility;

    public enum Ability
    {
        Fireball,
        Bow,
        None
    }
}
