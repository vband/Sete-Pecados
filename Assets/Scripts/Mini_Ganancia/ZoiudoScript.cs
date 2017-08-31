using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoiudoScript : MonoBehaviour {
    [SerializeField] private GameObject GM;
    [SerializeField] private GameObject OlhoEsquerdo;
    [SerializeField] private GameObject OlhoDireito;
	[SerializeField] private float VelocidadeDeAumento;
    [SerializeField] private float TamanhoMax;


    private Vector3 Unidade;
    private float TamanhoInicial;

    [HideInInspector] public bool IsSeeeing = false;

    private void Start()
    {
        Unidade = new Vector3(VelocidadeDeAumento, VelocidadeDeAumento, VelocidadeDeAumento);
        TamanhoInicial = OlhoEsquerdo.transform.localScale.x;  
    }

    private void Update()
    {
        if (!GM.GetComponent<MiniGameGananciaController>().GetPerdeu())
        {
            if (IsSeeeing)
                AumentaOlho();
            else
                DiminuiOlho();

            if (OlhoEsquerdo.transform.localScale.x >= TamanhoMax)
                GM.GetComponent<MiniGameGananciaController>().SetPerdeu();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
            IsSeeeing = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
            IsSeeeing = false;
    }

    private void AumentaOlho()
    {
        GM.GetComponent<MiniGameGananciaController>().SetUnperfect();
        if (OlhoEsquerdo.transform.localScale.x < TamanhoMax)
        {
            OlhoEsquerdo.transform.localScale += Unidade;
            OlhoDireito.transform.localScale += Unidade;
        }
    }

    private void DiminuiOlho()
    {
        if(OlhoEsquerdo.transform.localScale.x > TamanhoInicial)
        {
            OlhoEsquerdo.transform.localScale -= Unidade;
            OlhoDireito.transform.localScale -= Unidade;
        }
    }
}
