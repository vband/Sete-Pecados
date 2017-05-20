using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public float speed; // Velocidade do carro
    public float lifeTime; // Tempo que o carro permanece na Scene

    private float timer;

	void Start ()
    {
        timer = 0;
	}
	
	void FixedUpdate ()
    {
        
        if (!SceneController.paused)
        {
            timer += Time.fixedDeltaTime;
            Move();
            Despawn(); 
        }
        
	}

    private void Move()
    {
        GetComponent<Rigidbody2D>().MovePosition((Vector2)transform.position + Vector2.left * speed);
    }

    private void Despawn()
    {
        if (timer >= lifeTime)
        {
            gameObject.SetActive(false);
            Destroy(this);
        }
    }
}
