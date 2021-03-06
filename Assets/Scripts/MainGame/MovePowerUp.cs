﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePowerUp : MonoBehaviour {

    public float forca;
    public float despawn_time;
    private Rigidbody2D rb2D;
    private GameObject Spawner;
    

    // Use this for initialization
    void Start () {
        rb2D = GetComponent<Rigidbody2D>();
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("FILHO-PANEL"), true);
        Spawner = GameObject.Find("Spawner");
    }
	
	// Update is called once per frame
	void Update () {
        if (!SceneController.paused)
        {
            move();
            destroyTimer();
        }
            
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if(collision.gameObject.tag == "FILHO-PANEL")
        {
            Spawner.GetComponent<PowerUpController>().coleta(gameObject.tag);

            Destroy(this.gameObject);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && this.gameObject.tag == "aguabenta")
        {
            Spawner.GetComponent<PowerUpController>().coleta(gameObject.tag);
            Destroy(this.gameObject);
        }

        if (collision.gameObject.tag == "Player" && this.gameObject.tag != "aguabenta")
        {
            this.gameObject.layer = LayerMask.NameToLayer("FILHO-PANEL");
        }
    }

    private void move()
    {
        if(rb2D.velocity.y == 0)
        {
            rb2D.AddForce(new Vector2(0, forca));
        }
    }

    private void destroyTimer()
    {
        despawn_time -= Time.fixedDeltaTime;
        if( !NaTela() && despawn_time < 0)
        {
            Destroy(gameObject);
        }
    }

    private bool NaTela()
    {
        if (GetComponent<Renderer>().isVisible)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
