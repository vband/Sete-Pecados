using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public float speed; // Velocidade do carro

	void FixedUpdate ()
    {
        
        if (!SceneController.paused || SceneController.hasGameFinished)
        {
            Move();
        }
        
	}

    private void Move()
    {
        GetComponent<Rigidbody2D>().MovePosition((Vector2)transform.position + Vector2.left * speed);
    }

}
