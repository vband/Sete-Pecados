using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentaZoiudoScript : MonoBehaviour {

    [SerializeField] private float rotationTime;
    [SerializeField] private GameObject GM;
    private Quaternion inicio;
    private Quaternion fim;

    private float timeStartedLerping;
    private bool isLerping = false;

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

            if (!GetComponent<ZoiudoScript>().IsSeeeing)
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
            IniciaRotacao(0);
        }
    }

    private void IniciaRotacao(float angulo)
    {

        inicio = transform.rotation;
        fim = Quaternion.Euler(new Vector3(0, angulo, 0));

        timeStartedLerping = Time.time;
        isLerping = true;
    }

    private void StartAI()
    {
        StartCoroutine(AIRoutine());
    }

    IEnumerator AIRoutine()
    {
        yield return new WaitUntil(() => GM.GetComponent<MiniGameGananciaController>().isPlaying == true);
        inicio:
        IniciaRotacao(Random.Range(-360, 360));
        yield return new WaitUntil(() => isLerping == false);
        IniciaRotacao(Random.Range(-360, 360));
        goto inicio;
    }
}
