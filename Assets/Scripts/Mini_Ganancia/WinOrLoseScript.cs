using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinOrLoseScript : MonoBehaviour {

    public void Venceu()
    {
        if (GameMode.Mode == GameMode.GameModes.Minigame)
        {
            MinigameModeController minigameModeController = FindObjectOfType<MinigameModeController>();
            minigameModeController.OnMinigameFinished(true, "Ganancia");
        }
        else
        {
            GameObject.Find("Player").GetComponent<PlayerMovement>().StartDelaySobeCarinha();
            Screen.orientation = ScreenOrientation.AutoRotation;
            GameObject.Find("FadeImage").GetComponent<FadeController>().CallFading("Main");
#if UNITY_ANDROID
        Screen.orientation = ScreenOrientation.Landscape;
        Screen.orientation = ScreenOrientation.AutoRotation;
#endif
        }
    }

    public void Venceu(string callerScene)
    {
        if (GameMode.Mode == GameMode.GameModes.Minigame)
        {
            MinigameModeController minigameModeController = FindObjectOfType<MinigameModeController>();
            minigameModeController.OnMinigameFinished(true, callerScene);
        }
        else
        {
            GameObject.Find("Player").GetComponent<PlayerMovement>().StartDelaySobeCarinha();
            Screen.orientation = ScreenOrientation.AutoRotation;
            GameObject.Find("FadeImage").GetComponent<FadeController>().CallFading("Main");
#if UNITY_ANDROID
        Screen.orientation = ScreenOrientation.Landscape;
        Screen.orientation = ScreenOrientation.AutoRotation;
#endif
        }
    }

    public void Perdeu()
    {
        if (GameMode.Mode == GameMode.GameModes.Minigame)
        {
            MinigameModeController minigameModeController = FindObjectOfType<MinigameModeController>();
            minigameModeController.OnMinigameFinished(false, "Ganancia");
        }
        else
        {
            Screen.orientation = ScreenOrientation.AutoRotation;
            GameObject.Find("FadeImage").GetComponent<FadeController>().CallFading("Main");
            LivesController.RemVidas();
#if UNITY_ANDROID
        Screen.orientation = ScreenOrientation.Landscape;
        Screen.orientation = ScreenOrientation.AutoRotation;
#endif
        }
    }

    public void Perdeu(string callerScene)
    {
        if (GameMode.Mode == GameMode.GameModes.Minigame)
        {
            MinigameModeController minigameModeController = FindObjectOfType<MinigameModeController>();
            minigameModeController.OnMinigameFinished(false, callerScene);
        }
        else
        {
            Screen.orientation = ScreenOrientation.AutoRotation;
            GameObject.Find("FadeImage").GetComponent<FadeController>().CallFading("Main");
            LivesController.RemVidas();
#if UNITY_ANDROID
        Screen.orientation = ScreenOrientation.Landscape;
        Screen.orientation = ScreenOrientation.AutoRotation;
#endif
        }
    }

    public void Perfect()
    {
        if (GameMode.Mode == GameMode.GameModes.Minigame)
        {
            MinigameModeController minigameModeController = FindObjectOfType<MinigameModeController>();
            minigameModeController.OnMinigameFinished(true, "Ganancia");
        }
        else
        {
            LivesController.addVidas();
            GameObject.Find("Player").GetComponent<PlayerMovement>().StartDelaySobeCarinha();
            Screen.orientation = ScreenOrientation.AutoRotation;
            GameObject.Find("FadeImage").GetComponent<FadeController>().CallFading("Main");
#if UNITY_ANDROID
        Screen.orientation = ScreenOrientation.Landscape;
        Screen.orientation = ScreenOrientation.AutoRotation;
#endif
        }
    }

    public void Perfect(string callerScene)
    {
        if (GameMode.Mode == GameMode.GameModes.Minigame)
        {
            MinigameModeController minigameModeController = FindObjectOfType<MinigameModeController>();
            minigameModeController.OnMinigameFinished(true, callerScene);
        }
        else
        {
            LivesController.addVidas();
            GameObject.Find("Player").GetComponent<PlayerMovement>().StartDelaySobeCarinha();
            Screen.orientation = ScreenOrientation.AutoRotation;
            GameObject.Find("FadeImage").GetComponent<FadeController>().CallFading("Main");
#if UNITY_ANDROID
        Screen.orientation = ScreenOrientation.Landscape;
        Screen.orientation = ScreenOrientation.AutoRotation;
#endif
        }
    }
}
