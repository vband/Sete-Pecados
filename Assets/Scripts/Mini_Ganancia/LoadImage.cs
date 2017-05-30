using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadImage : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Image>().sprite = GameObject.Find("minigame").GetComponent<mecanica>().rosto_correto;
	}
}
