using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampolim : MonoBehaviour {

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" &&
           collision.gameObject.GetComponent<PlayerMovement>().isJumping == false)
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 1500));
        }
    }
}
