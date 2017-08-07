using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentaZoiudoScript : MonoBehaviour {

    [SerializeField] private float rotationTime;

    private Quaternion inicio;
    private Quaternion fim;

    private float timeStartedLerping;
    private bool isLerping = false;

    

    // Use this for initialization
    void Start () {
        StartAI();
    }
	
	

    private void FixedUpdate()
    {
        if (isLerping)
        {
            
            float timeSinceStarted = Time.time - timeStartedLerping;
            float percentageComplete = timeSinceStarted / rotationTime;
 
            if(!GetComponent<ZoiudoScript>().IsSeeeing)
                transform.rotation = Quaternion.Lerp(inicio, fim, percentageComplete);

            if (percentageComplete >= 1.0f)
            {
                isLerping = false;
            }
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
        inicio:
        IniciaRotacao(Random.Range(-360, 360));
        yield return new WaitUntil(() => isLerping == false);
        IniciaRotacao(Random.Range(-360, 360));
        goto inicio;
    }
}
