using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownScript : MonoBehaviour {

    public Text display;
    public float TempoContagem;
    
    
	// Update is called once per frame
	void Update () {
        

        if (GetComponent<mecanica>().syncBool)
        {
            display.gameObject.SetActive(true);
            if (TempoContagem > 0.0f && !GetComponent<mecanica> ().faceClick)
            {
                TempoContagem -= Time.deltaTime;
                display.text = TempoContagem.ToString("F1");
            }
            else if(!GetComponent<mecanica>().faceClick)
            {
                display.fontSize = 35;
                display.text = "Time!";
                GetComponent<mecanica> ().RostoErrado();
            }
        }
	}
}
