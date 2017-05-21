using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGanancia : MonoBehaviour {
    public float forceY;
    public float forceX;
    public float MaxVelo;

    private Vector2 offset;
    private Vector3 temp;



    private void Awake()
    {
        offset = transform.position;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!SceneController.paused)
        {
            MoveVacilante();
            moveHorizontal();
            
        }
        else if(SceneController.paused)
        {
            GetComponent<Rigidbody2D>().Sleep();
        }
        
	}

    void moveHorizontal()
    {
        if (GetComponent<Rigidbody2D>().velocity.x < MaxVelo  )
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(forceX * Time.deltaTime, 0));
        }
        
    }

    void MoveVacilante()
    {
        if (transform.position.y > offset.y)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, (forceY / 1.2f) * Time.deltaTime));
        }
        else if (transform.position.y < offset.y)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, (forceY * 1.2f) * Time.deltaTime));
        }
        else
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, forceY * Time.deltaTime));
        }
    }
}
