using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOptions : MonoBehaviour {

    public bool PowerUP = false;
    public bool InimigoTerrestre = false;
    public bool InimigoAlado = false;

    private bool[] Config = {false,false,false};

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
    }

    public bool[] getConfig()
    {
        return Config;
    }
	
}
