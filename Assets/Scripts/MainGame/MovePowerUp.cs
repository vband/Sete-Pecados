using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePowerUp : MonoBehaviour {

    public float forca;
    public float despawn_time;
    private Rigidbody2D rb2D;
    

    // Use this for initialization
    void Start () {
        rb2D = GetComponent<Rigidbody2D>();
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
        if( collision.gameObject.tag == "Player")
        {
            GetComponentInParent<PowerUpController>().coleta(gameObject.tag);
            Destroy(this.gameObject);
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
        if(despawn_time < 0)
        {
            Destroy(gameObject);
        }
    }
}
