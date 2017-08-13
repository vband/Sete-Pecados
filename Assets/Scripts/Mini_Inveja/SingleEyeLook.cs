using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleEyeLook : MonoBehaviour {

    

    [SerializeField] private Transform iris; //bolinha preta do olho

    private Transform GloboOcular; //posicao original do olho para usar como base de calculo
    private Vector3 OlharNormalizado;

    private GameObject target;
    private float iris_max_raio;

	// Use this for initialization
	void Start () {
        GloboOcular = GetComponentInParent<Transform>();
        target = GetComponentInParent<ParDeOlhosController>().Target;
        iris_max_raio = GetComponentInParent<ParDeOlhosController>().iris_max_raio;
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
            
            OlharNormalizado = (target.transform.position - iris.position).normalized;
            iris.position = (OlharNormalizado * iris_max_raio) + GloboOcular.position;
        }
        
    }

    public void SetIrisColor(Color cor)
    {
        iris.gameObject.GetComponent<SpriteRenderer>().color = cor;
    }
}
