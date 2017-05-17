using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Remove_instruçoes : MonoBehaviour {

    public float tempo;
    public float velocidade;
    private bool move_instructions = false;


	// Use this for initialization
	void Start () {
        StartCoroutine(timer(tempo));
    }
	
	// Update is called once per frame
    void Update()
    {
        if (move_instructions && transform.position.y < 8)
        {
            transform.Translate(new Vector3(0, velocidade * Time.deltaTime));
        }
    }

    IEnumerator timer(float segundos)
    {
        yield return new WaitForSeconds(segundos);
        move_instructions = true;
    }
}
