using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGanancia : MonoBehaviour {
    public GameObject panfleto;
    public GameObject Placa;
    public HingeJoint2D CordaPlaca;
    public float forceY;
    public float forceX;
    public float MaxVelo;
    public float DropCooldown;

    private float DropCooldownOriginal;
    private Vector2 offset;
    private Vector3 temp;
    private bool natela;
    private Rigidbody2D rb2D;

    private void Awake()
    {
        offset = transform.position;
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Use this for initialization
    void Start () {
        ArrebentaCordaStart();
        DropPaperStart();
        DropCooldownOriginal = DropCooldown;   
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (!SceneController.paused)
        {
            MoveVacilante();
        }
        else if(SceneController.paused)
        {
            GetComponent<Rigidbody2D>().Sleep();
            
        }

        natela = NaTela();

        //atualiza tempo para drop em funcao do deltaTime.
        if (!SceneController.paused)
        {
            DropCooldown -= Time.deltaTime;

            // TESTE - despausar movimentação
            if (rb2D.bodyType == RigidbodyType2D.Kinematic)
            {
                rb2D.bodyType = RigidbodyType2D.Dynamic;
                rb2D.freezeRotation = false;
            }
        }

        // TESTE - pausar movimentação
        else
        {
            rb2D.bodyType = RigidbodyType2D.Kinematic;
            rb2D.velocity = new Vector2(0, 0);
            rb2D.freezeRotation = true;
        }

    }

    private void ArrebentaCordaStart()
    {
        StartCoroutine(ArrebentaCorda());
    }

    IEnumerator ArrebentaCorda()
    {
        yield return new WaitUntil(() => natela == true);
        yield return new WaitForSeconds(Random.Range(0.5f,2f));
        
        if(Placa != null)
        {
            //Placa.transform.SetParent(GameObject.Find("Environment").transform);
            Placa.transform.SetParent(transform.parent.parent);//associa a placa ao segmento de origem.
        }
        
        Destroy(CordaPlaca);
        
    }


    private void DropPaperStart()
    {
        StartCoroutine(DropPaper());
    }
    IEnumerator DropPaper()
    {
        yield return new WaitUntil(() => natela == true);

        // Solta um papel logo quando aparece na tela
        //Instantiate(panfleto, new Vector3(transform.position.x - 1, transform.position.y - 1, 0), Quaternion.identity, transform.parent.parent);

        inicio:

        yield return new WaitUntil(() => DropCooldown < 0);

        // Solta novos papéis, desta vez, periodicamente
        Instantiate(panfleto, new Vector3(transform.position.x - 1, transform.position.y - 1, 0), Quaternion.identity, transform.parent.parent);
        DropCooldown = DropCooldownOriginal;

        goto inicio;
    }


    void moveHorizontalStart()
    {
        StartCoroutine(MoveHorizontal());    
    }

    IEnumerator MoveHorizontal()
    {
        inicio:
        yield return new WaitUntil(() => GetComponent<Rigidbody2D>().IsSleeping() == true);
        yield return new WaitUntil(() => GetComponent<Rigidbody2D>().IsAwake() == true);
        GetComponent<Rigidbody2D>().velocity += new Vector2(-MaxVelo, 0);
        goto inicio;
    }

    void MoveVacilante()
    {
        if (transform.position.y > offset.y)
        {
            Vector3 velAtual = GetComponent<Rigidbody2D>().velocity;
            velAtual.x = -MaxVelo;
            GetComponent<Rigidbody2D>().velocity = velAtual;
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, (forceY / 1.2f) * Time.deltaTime));
        }
        else if (transform.position.y < offset.y)
        {
            Vector3 velAtual = GetComponent<Rigidbody2D>().velocity;
            velAtual.x = -MaxVelo;
            GetComponent<Rigidbody2D>().velocity = velAtual;
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, (forceY * 1.2f) * Time.deltaTime));
        }
        else
        {
            Vector3 velAtual = GetComponent<Rigidbody2D>().velocity;
            velAtual.x = -MaxVelo;
            GetComponent<Rigidbody2D>().velocity = velAtual;
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, forceY * Time.deltaTime));
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
