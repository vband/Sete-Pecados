using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoltarParaMenuScript : MonoBehaviour {

	public void VoltarParaMenu()
    {
        GameObject.Find("FadeImage").GetComponent<FadeController>().CallFading("MainMenu");
    }

}
