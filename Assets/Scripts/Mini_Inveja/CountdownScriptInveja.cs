using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownScriptInveja : MonoBehaviour {

    public Text display;
    public float TempoContagem;
    
    
	// Update is called once per frame
	void Update () {
        

        if (GetComponent<MinigameInvejaController>().syncBool)
        {
            display.gameObject.SetActive(true);
            if (TempoContagem > 0.0f && !GetComponent<MinigameInvejaController> ().faceClick)
            {
                TempoContagem -= Time.deltaTime;
                display.text = TempoContagem.ToString("F1");
            }

            else if(!GetComponent<MinigameInvejaController>().faceClick )
            {
                display.fontSize = 35;
                display.text = "Time!";
                GetComponent<MinigameInvejaController> ().RostoErrado();
            }
        }
	}
}
