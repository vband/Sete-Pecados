using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinOrLoseScript : MonoBehaviour {

    public void Venceu()
    {
        GameObject.Find("Player").GetComponent<PlayerMovement>().StartDelaySobeCarinha();
        Screen.orientation = ScreenOrientation.AutoRotation;
        GameObject.Find("FadeImage").GetComponent<FadeController>().CallFading("Main");
#if UNITY_ANDROID
        Screen.orientation = ScreenOrientation.Landscape;
        Screen.orientation = ScreenOrientation.AutoRotation;
#endif
    }

    public void Perdeu()
    {
        Screen.orientation = ScreenOrientation.AutoRotation;
        GameObject.Find("FadeImage").GetComponent<FadeController>().CallFading("Main");
        LivesController.RemVidas();
#if UNITY_ANDROID
        Screen.orientation = ScreenOrientation.Landscape;
        Screen.orientation = ScreenOrientation.AutoRotation;
#endif
    }

    public void Perfect()
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
