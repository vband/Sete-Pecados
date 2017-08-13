using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleEyeLook : MonoBehaviour {

    

    [SerializeField] private Transform iris; //bolinha preta do olho

    private Transform GloboOcular; //posicao original do olho para usar como base de calculo
    private Vector3 OlharNormalizado;

    public Transform target;
    public int modo = 0;
    const int CORRETO = 0, FRENTE = 1, OPOSTO = 2, ALEATORIO = 3;
    private float iris_max_raio;

    private float AnguloRotacao = 0;


	// Use this for initialization
	void Start () {
        
        GloboOcular = GetComponentInParent<Transform>();
        //target = GetComponentInParent<ParDeOlhosController>().Target;
        
        iris_max_raio = GetComponentInParent<ParDeOlhosController>().iris_max_raio;
        AnguloRotacao = Random.Range(-90, 90);
    }
	
	// Update is called once per frame
	void Update () {
        IrisLookAt();
        
    }

    public void IrisLookAt()
    {
        //somente se tiver um invejoso seleciona, este olha para ele.
        if(target != null)
        {
            if (modo == CORRETO)
            {
                OlharNormalizado = (target.position - iris.position).normalized;
                iris.position = (OlharNormalizado * iris_max_raio) + GloboOcular.position;
            }
            else if(modo == FRENTE)
            {
                //NÃO ALTERA POSICAO ORIGINAL
            }
            else if (modo == OPOSTO)
            {
                OlharNormalizado = (target.position - iris.position).normalized;
                iris.position =( (OlharNormalizado * iris_max_raio) * (-1) )+ GloboOcular.position;
            }
            else if(modo == ALEATORIO)
            {
                OlharNormalizado = (target.position - iris.position).normalized;
                OlharNormalizado = Quaternion.Euler(0, 0, AnguloRotacao) * OlharNormalizado;
                iris.position = ((OlharNormalizado * iris_max_raio) * (-1)) + GloboOcular.position;
            }
        
            
        }
    }

    public void SetIrisColor(Color cor)
    {
        iris.gameObject.GetComponent<SpriteRenderer>().color = cor;
    }
}
