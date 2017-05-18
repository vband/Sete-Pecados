using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AjustaTexto : MonoBehaviour {

    // Use this for initialization
    void Start () {
        GetComponent<RectTransform>().sizeDelta = new Vector2(GameObject.Find("minigame").GetComponent<mecanica>().screenSize.x * 90, 60);	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
