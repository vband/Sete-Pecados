using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinigameModeDisplayController : MonoBehaviour
{
    [SerializeField]
    private Text lives, score, highScore;

    public void UpdateDisplay(int newNumberOfLives, int newScore, int newHighScore)
    {
        lives.text = "x " + newNumberOfLives.ToString();
        score.text = "Score: " + newScore.ToString();
        highScore.text = "High Score: " + newHighScore.ToString();

#if UNITY_ANDROID
        Screen.orientation = ScreenOrientation.LandscapeLeft;
#endif
    }
}
