using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentaZoiudoScript : MonoBehaviour {

    public static float rotationTime;
    [SerializeField] private GameObject GM;
    private Quaternion inicio;
    private Quaternion fim;

    private float timeStartedLerping;
    private bool isLerping = false;

    private float rotacao = 0;

    private void Start()
    {
        StartAI();
    }

    private void FixedUpdate()
    {
        if (isLerping)
        {
            
            float timeSinceStarted = Time.time - timeStartedLerping;
            float percentageComplete = timeSinceStarted / rotationTime;

            if (!GetComponent<ZoiudoScript>().IsSeeeing || rotationTime == 0.2f)
            {
                transform.rotation = Quaternion.Lerp(inicio, fim, percentageComplete);
            }
            else
            {
                isLerping = false;
            }
               

            if (percentageComplete >= 1.0f)
            {
                isLerping = false;
            }
        }

        if (GM.GetComponent<MiniGameGananciaController>().GetPerdeu())
        {
            StopCoroutine(AIRoutine());
            rotationTime = 0.2f;
            IniciaRotateTo(0);
        }
    }

    private void IniciaRotacao(float angulo)
    {
        inicio = transform.rotation;
        fim = Quaternion.Euler(new Vector3(0, angulo, 0)) * inicio; //soma a rotacao com o novo angulo sorteado 

        timeStartedLerping = Time.time;
        
    }

    private void IniciaRotateTo(float angulo)
    {
        inicio = transform.rotation;
        fim = Quaternion.Euler(new Vector3(0, angulo, 0));

        timeStartedLerping = Time.time;
    }

    private void StartAI()
    {
        StartCoroutine(AIRoutine());
    }

    IEnumerator AIRoutine()
    {
        yield return new WaitUntil(() => GM.GetComponent<MiniGameGananciaController>().isPlaying == true);
        inicio:
        IniciaRotacao(RotacaoAleatoria());
        isLerping = true;
        yield return new WaitUntil(() => isLerping == false);
        goto inicio;
    }


    private float RotacaoAleatoria()
    {
        
        
        if (Random.Range(0, 100) > 50)
        {
            rotacao = Random.Range(45, 270);
            return rotacao;
        }
        else
        {
            rotacao = Random.Range(-45, -270);
            return rotacao;
        }
        
    }
}
