using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class creditosscript : MonoBehaviour {

    public GameObject TelaInicial,opcoes,creditos;
    

    public void clickcreditos()
    {
        TelaInicial.SetActive(false);
        opcoes.SetActive(false);
        creditos.SetActive(true);
    }

    public void clickvoltar()
    {
        TelaInicial.SetActive(true);
        opcoes.SetActive(false);
        creditos.SetActive(false);
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
