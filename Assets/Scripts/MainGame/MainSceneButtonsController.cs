using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneButtonsController : MonoBehaviour
{
    public GameObject almasSalvasText;

    public void VoltarParaMenu()
    {
        GameObject.Find("FadeImage").GetComponent<FadeController>().CallFading("MainMenu");
        almasSalvasText.SetActive(false);
    }

    public void JogarDeNovo()
    {
        GameOverBehaviour.restart = true;
        GameObject.Find("FadeImage").GetComponent<FadeController>().CallFading("GameOver");
        almasSalvasText.SetActive(false);
    }
}
