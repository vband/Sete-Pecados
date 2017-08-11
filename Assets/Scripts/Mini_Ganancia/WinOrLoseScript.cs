using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinOrLoseScript : MonoBehaviour {

    public void Venceu()
    {
        GameObject.Find("Player").GetComponent<PlayerMovement>().StartDelaySobeCarinha();
        GameObject.Find("FadeImage").GetComponent<FadeController>().CallFading("Main");
    }

    public void Perdeu()
    {
        GameObject.Find("FadeImage").GetComponent<FadeController>().CallFading("Main");
        LivesController.RemVidas();
    }

    public void Perfect()
    {
        LivesController.addVidas();
        GameObject.Find("Player").GetComponent<PlayerMovement>().StartDelaySobeCarinha();
        GameObject.Find("FadeImage").GetComponent<FadeController>().CallFading("Main");
    }
}
