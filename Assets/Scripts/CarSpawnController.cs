using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawnController : MonoBehaviour
{
    public float chance; // Chance de gerar um novo carro a cada frame
    public float timeInterval; // Intervalo de tempo entre a geração de dois carros
    public GameObject[] cars; // Vetor com os carros disponíveis
    public GameObject camera;
    public float streetYPosition;

    private float timer; // Cronômetro
    private Vector3 screenDimensionsInWorldUnits;

	void Start ()
    {
        timer = 0;
        screenDimensionsInWorldUnits = camera.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(
            Screen.width, Screen.height, 0));
	}
	
	void Update ()
    {
        timer += Time.deltaTime;
        SpawnCar();
	}

    private void SpawnCar()
    {

        if (timer >= timeInterval)
        {
            timer = 0;

            // Gera um número aleatório entre 0 e 1. Se for menor ou igual a 'chance', entra no if
            if (Random.value <= chance)
            {
                // Escolhe aleatoriamente um dos carros disponíveis
                GameObject toInstantiate = cars[(int)Random.Range(0, cars.Length)];
                // Instancia
                Instantiate(toInstantiate,
                    new Vector3(camera.transform.position.x + screenDimensionsInWorldUnits.x + 20, streetYPosition, 0),
                    new Quaternion(0,0,0,0));
            }
        }
    }
}
