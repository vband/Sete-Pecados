using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGanancia : MonoBehaviour {
    public GameObject panfleto;
    public float forceY;
    public float forceX;
    public float MaxVelo;
    public float ArrebentaCordatimer;
    public float DropCooldown;
    public float AutoDestroyTime;

    private Vector2 offset;
    private Vector3 temp;

    private void Awake()
    {
        offset = transform.position;
    }

    // Use this for initialization
    void Start () {
        ArrebentaCordaStart(ArrebentaCordatimer);
        DropPaperStart(DropCooldown);
        AutoDestroyStart(AutoDestroyTime);
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

    private void ArrebentaCordaStart(float time)
    {
        StartCoroutine(ArrebentaCorda(time));
    }

    IEnumerator ArrebentaCorda(float time)
    {
        yield return new WaitForSeconds(time);
        Rigidbody2D placa = GetComponent<DistanceJoint2D>().connectedBody;
        if(placa != null)
        {
            placa.transform.SetParent(GameObject.Find("Environment").transform);
        }
        Destroy(GetComponent<DistanceJoint2D>());
        
    }


    private void DropPaperStart(float Cooldown)
    {
        StartCoroutine(DropPaper(Cooldown));
    }
    IEnumerator DropPaper(float Cooldown)
    {
    inicio:
        yield return new WaitForSeconds(Cooldown);
        if (!SceneController.paused && Time.deltaTime != 0)
        {
            Instantiate(panfleto, new Vector3(transform.position.x - 1, transform.position.y - 1, 0), Quaternion.identity, transform.parent);
        }
        goto inicio;
    }

    private void AutoDestroyStart(float time)
    {
        StartCoroutine(AutoDestroy(time));
    }

    IEnumerator AutoDestroy(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
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
