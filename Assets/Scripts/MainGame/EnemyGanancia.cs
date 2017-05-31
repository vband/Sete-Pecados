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
    public float AutoDestroyTime;

    private float DropCooldownOriginal;
    private Vector2 offset;
    private Vector3 temp;
    private bool natela;
    private bool EntrouNaTelaUmaVez;

    private void Awake()
    {
        offset = transform.position;
    }

    // Use this for initialization
    void Start () {
        ArrebentaCordaStart();
        DropPaperStart();
        DropCooldownOriginal = DropCooldown;
        
    }
	
	// Update is called once per frame
	void Update () {
        if (!SceneController.paused)
        {
            MoveVacilante();
        }
        else if(SceneController.paused)
        {
            GetComponent<Rigidbody2D>().Sleep();
            
        }
        AutoDestroy();

        natela = NaTela();

        //atualiza tempo para drop em funcao do deltaTime.
        if (!SceneController.paused)
        {
            DropCooldown -= Time.deltaTime;
        }
       
    }

    private void ArrebentaCordaStart()
    {
        StartCoroutine(ArrebentaCorda());
    }

    IEnumerator ArrebentaCorda()
    {
        yield return new WaitUntil(() => natela == true);
        EntrouNaTelaUmaVez = true;
        yield return new WaitForSeconds(Random.Range(0.5f,2f));
        
        if(Placa != null)
        {
            Placa.transform.SetParent(GameObject.Find("Environment").transform);
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
        
        inicio:

        yield return new WaitUntil(() => DropCooldown < 0);
        Instantiate(panfleto, new Vector3(transform.position.x - 1, transform.position.y - 1, 0), Quaternion.identity, transform.parent);
        DropCooldown = DropCooldownOriginal;

        goto inicio;
    }

    public void AutoDestroy()
    {
        if ( !NaTela() && EntrouNaTelaUmaVez && Time.deltaTime != 0)
        {
            AutoDestroyTime -= Time.deltaTime;
        }
        if(AutoDestroyTime < 0)
        {
            Destroy(gameObject);
        }
        
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

    public bool NaTela()
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
