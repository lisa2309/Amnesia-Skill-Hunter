using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StateController
{
    //LevelState
    public static int currentStage = 0;
    public static int currentSection = 0;
    public static int maxPlayerhealth;

    //Playerstate
    public static bool isGodModeEnabled = false;
    public static int currentPlayerHealth;
    public static Ability currentAbility = Ability.None;

    public enum Ability
    {
        Fireball,
        Bow,
        None
    }

    public static void resetLevelState()
    {
        currentSection = 0;
        currentStage = 0;
    }

    public static void resetPlayerStats()
    {
        currentPlayerHealth = maxPlayerhealth;
        currentAbility = Ability.None;
    }
}
