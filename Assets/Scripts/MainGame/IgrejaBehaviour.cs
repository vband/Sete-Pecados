using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgrejaBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Venceu o jogo!!");
            GameObject.Find("FadeImage").GetComponent<FadeController>().CallFading("GameOver");
        }
    }
}
