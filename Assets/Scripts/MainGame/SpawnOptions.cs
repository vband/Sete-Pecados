using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOptions : MonoBehaviour {

    public bool PowerUP = false;
    public bool InimigoTerrestre = false;
    public bool InimigoAlado = false;

    private bool[] Config = { false, false, false };
    private Transform segmentoPai;

	// Use this for initialization
	void Start () {
        Config[0] = PowerUP;
        Config[1] = InimigoTerrestre;
        Config[2] = InimigoAlado;
        
        // se marcar alado e terrestre, vai spawnar um terrestre.
        if(Config[1] && Config[2])
        {
            Config[1] = true;
            Config[2] = false;
        }

        // Obtém o transform do seu pai, que é o segmento a que ele pertence
        Transform[] auxArray = GetComponentsInParent<Transform>();
        segmentoPai = auxArray[1];
    }

    public bool[] getConfig()
    {
        return Config;
    }

	public Transform GetParent()
    {
        return segmentoPai;
    }
}
