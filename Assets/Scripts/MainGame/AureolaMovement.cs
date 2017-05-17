using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AureolaMovement : MonoBehaviour {

    public float velocidade;
    private Vector3 offset;

	// Use this for initialization
	void Start () {
        offset = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.position.y < offset.y + 10)
        {
            //transform.position = new Vector2(0, transform.position.y + velocidade * Time.deltaTime);
        }
        //else if(transform.position.y < offset.y - 10)
	}
}
