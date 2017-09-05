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
        sr.color = RandomColor();
        velocidade = new Vector2(ArremecavelVelocity, 0);
        rb.AddTorque(10);
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

    Color RandomColor()
    {
        switch (Random.Range(0, 6))
        {
            case 0:
                return Color.red;
            case 1:
                return Color.magenta;
            case 2:
                return Color.yellow;
            case 3:
                return Color.black;
            case 4:
                return Color.green;
            case 5:
                return Color.blue;
            default:
                return Color.red;
        }
    }
}
