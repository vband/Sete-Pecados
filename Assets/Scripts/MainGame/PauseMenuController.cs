using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//REMOVED por causa do port para android
//using UnityEngine.PostProcessing;


public class PauseMenuController : MonoBehaviour {

    public Button VoltarAoJogo, VoltarParaMenu, Pause;
    public Text Pausado;
    public GameObject Cam;

    public void AtivaMenu(bool estado)
    {
        switch (estado)
        {
            case true:
#if UNITY_ANDROID
                Pause.gameObject.SetActive(false);
#endif
                AudioListener.pause = true;
                VoltarAoJogo.gameObject.SetActive(true);
                VoltarParaMenu.gameObject.SetActive(true);
                Pausado.gameObject.SetActive(true);
                Time.timeScale = 0;

                //Cam.GetComponent<PostProcessingBehaviour>().enabled = true;
                break;
            case false: 
                Time.timeScale = 1;
                AudioListener.pause = false;
#if UNITY_ANDROID
                Pause.gameObject.SetActive(true);
#endif
                VoltarAoJogo.gameObject.SetActive(false);
                VoltarParaMenu.gameObject.SetActive(false);
                Pausado.gameObject.SetActive(false);
                //REMOVED por causa do port para android
                //Cam.GetComponent<PostProcessingBehaviour>().enabled = false;
                break;
        }
        
    }

    public void VoltarAoJogoOnClick()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
        AtivaMenu(false);
    }

    public void VoltarParaMenuOnClick()
    {
        Time.timeScale = 1;
        GameObject.Find("FadeImage").GetComponent<FadeController>().CallFading("MainMenu");
    }

	
}
