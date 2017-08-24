using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArremecavelController : MonoBehaviour {

    [SerializeField] private float ArremecavelVelocity;

    private Vector2 velocidade;
    private Rigidbody2D rb;
    private SpriteRenderer sr;


	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        velocidade = new Vector2(ArremecavelVelocity, 0);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (!SceneController.paused)
            rb.velocity = velocidade;
        else
            rb.velocity = Vector3.zero;
        if(!sr.isVisible)
            Destroy(this.gameObject);
	}
}
