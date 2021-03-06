﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinigameModeDisplayController : MonoBehaviour
{
    [SerializeField]
    private Text lives = null, score = null, highScore = null;

    public void UpdateDisplay(int newNumberOfLives, int newScore, int newHighScore)
    {
        lives.text = "x " + newNumberOfLives.ToString();
        score.text = "Pontos: " + newScore.ToString();
        highScore.text = "Recorde: " + newHighScore.ToString();

#if UNITY_ANDROID
        Screen.orientation = ScreenOrientation.LandscapeLeft;
#endif
    }
}
