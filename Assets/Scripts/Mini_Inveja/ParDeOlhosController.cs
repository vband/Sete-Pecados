using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParDeOlhosController : MonoBehaviour {

    [SerializeField] bool RamdomColor;

    public float iris_max_raio; //raio em relacao ao centro do olho

    //cores dos olhos inveja
    public static Color PRETO, CASTANHO, AZUL, VERDE;

    const int CORRETO = 0, FRENTE = 1, OPOSTO = 2, ALEATORIO = 3;

    [HideInInspector] public float AnguloRotacao = 0;

    // Use this for initialization
    void Start() {
        PRETO = new Color(0, 0, 0);
        CASTANHO = new Color(0.588f, 0.435f, 0.231f);
        AZUL = Color.cyan;
        VERDE = new Color(0.341f, 0.901f, 0.360f);

        AnguloRotacao = Random.Range(-90, 90);

        Color cor = PickRandomColor();
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<SingleEyeLook>().SetIrisColor(cor);
        }
    }

    private Color PickRandomColor()
    {
        //cores claras nao deram certo inicialmente
        //int rand = Random.Range(0, 100);
        int rand = Random.Range(0, 50);

        if (rand < 25)
            return PRETO;
        if (rand < 50)
            return CASTANHO;
        if (rand < 75)
            return AZUL;
        if (rand < 100)
            return VERDE;

        return PRETO;

    }

    public void SetTarget(Transform target)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<SingleEyeLook>().target = target;
        }
    }

    public void SetTarget(Transform target, int modo)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<SingleEyeLook>().target = target;
            transform.GetChild(i).GetComponent<SingleEyeLook>().modo = modo;
        }
    }
	
}
