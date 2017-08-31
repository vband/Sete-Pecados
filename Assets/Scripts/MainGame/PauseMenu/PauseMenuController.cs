using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PauseMenuController : MonoBehaviour {

    public Button VoltarAoJogo, VoltarParaMenu, Pause;
    public Toggle Mudo;
    public Text Pausado;

    public void AtivaMenu(bool estado)
    {
        Vibration.Vibrate(30);
        switch (estado)
        {
            case true:
#if UNITY_ANDROID
                SceneController.paused = true;
#endif
                AudioListener.pause = true;

                VoltarAoJogo.gameObject.SetActive(true);
                VoltarParaMenu.gameObject.SetActive(true);
                Pausado.gameObject.SetActive(true);
                Mudo.gameObject.SetActive(true);
                Time.timeScale = 0;

                break;
            case false: 
                Time.timeScale = 1;
                if (PlayerPrefs.GetInt("MUDO") == 0)
                {
                    AudioListener.pause = false;
                }
#if UNITY_ANDROID
                SceneController.paused = false;
#endif
                VoltarAoJogo.gameObject.SetActive(false);
                VoltarParaMenu.gameObject.SetActive(false);
                Pausado.gameObject.SetActive(false);
                Mudo.gameObject.SetActive(false);

                break;
        }
        
    }

    public void VoltarAoJogoOnClick()
    {
        AtivaMenu(false);
    }

    public void VoltarParaMenuOnClick()
    {
        Vibration.Vibrate(30);
        Time.timeScale = 1;
        GameObject.Find("FadeImage").GetComponent<FadeController>().CallFading("MainMenu");
    }

	
}
