using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteWorld : MonoBehaviour
{
    public Transform player;
    public Transform[] objectsToGenerate; // Array de objetos que serão gerados constantemente: prédios, postes, etc
    public Transform beginnigOfWorld, endOfWorld; // Ponteiros para o início e o fim da cidade no momento inicial do jogo

    private float worldWidth; // Largura inicial da cidade
    private int nIterations; // Quantidade atual de vezes que o script já gerou novos objetos

	void Start ()
    {
        //Calcula a largura inicial da cidade
        worldWidth = endOfWorld.position.x - beginnigOfWorld.position.x;
        nIterations = 1;
	}
	
	void Update ()
    {
        // Se o jogador se aproximar do fim da cidade, gera novos objetos
		if (Vector3.Distance(player.position, endOfWorld.position + Vector3.right * worldWidth * nIterations) < 50)
        {
            generateMoreObjects();
        }
	}

    private void generateMoreObjects()
    {
        // Instancia todos os objetos que foram preparados antecipadamente
        for (int i = 0; i < objectsToGenerate.Length; i++)
        {
            Instantiate(objectsToGenerate[i], objectsToGenerate[i].position + Vector3.right * worldWidth * nIterations,
                new Quaternion(0, 0, 0, 0));
        }

        nIterations++;
    }
}
