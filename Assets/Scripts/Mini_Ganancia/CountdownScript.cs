using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownScript : MonoBehaviour {

    public Text display;
    public float TempoContagem;
    public GameObject errado;

    private bool temp;

	// Update is called once per frame
	void Update () {
        temp = GetComponent<mecanica>().syncBool;

        if (temp)
        {
            display.gameObject.SetActive(true);
            if (TempoContagem > 0.0f)
            {
                TempoContagem -= Time.deltaTime;
                display.text = TempoContagem.ToString("F1");
            }
            else
            {
                display.fontSize = 35;
                display.text = "Time!";
                errado.GetComponent<errado>().Errado();
            }
        }
	}
}
