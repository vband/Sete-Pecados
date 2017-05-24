using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour {

    public Button VoltarAoJogo, VoltarParaMenu;

    public void AtivaMenu(bool estado)
    {
        switch (estado)
        {
            case true:
                VoltarAoJogo.gameObject.SetActive(true);
                VoltarParaMenu.gameObject.SetActive(true);
                break;
            case false:
                VoltarAoJogo.gameObject.SetActive(false);
                VoltarParaMenu.gameObject.SetActive(false);
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
