using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeAlvo : MonoBehaviour {

    // Use this for initialization
    void Start () {
        //obtem quantidade de parametros
        int Parametros = GetComponent<Animator>().parameterCount;
        //sorteia um numero, de acordo com a quatidade, usando mod
        int RandNum = (Random.Range(0, 100) % Parametros);
        //seta o valor em true, de acordo com a escolha, no animator
        GetComponent<Animator>().SetBool( GetComponent<Animator>().GetParameter(RandNum).name, true);
	}
	
}
