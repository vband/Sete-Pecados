using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode
{
    public enum GameModes
    {
        Minigame, Endless, Classic
    }
    //adicionada configuracao default para evitar null pointer exception 
    //e quebrar modo classico quando iniciado no editor
    public static GameModes Mode = GameModes.Classic;
}
