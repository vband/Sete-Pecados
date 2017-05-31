using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteWorld : MonoBehaviour
{
    public Transform player;
    public Transform[] objectsToGenerate; // Array de objetos que serão gerados constantemente: prédios, postes, etc
    public Transform beginnigOfWorld, endOfWorld; // Ponteiros para o início e o fim da cidade no momento inicial do jogo
    public SceneController cenaPrincipal;
    public Transform igrejaPrefab;

    private float worldWidth; // Largura inicial da cidade
    private int nIterations; // Quantidade atual de vezes que o script já gerou novos objetos
    private Vector3 currentCityEndPos;
    private float distanceToWin;
    private bool hasCreatedChurch;

	void Start ()
    {
        //Calcula a largura inicial da cidade
        worldWidth = endOfWorld.position.x - beginnigOfWorld.position.x;
        nIterations = 1;
        distanceToWin = cenaPrincipal.distanceToWin;
        hasCreatedChurch = false;
    }

    void Update ()
    {
        // Atualiza a posição do final da cidade
        currentCityEndPos = endOfWorld.position + Vector3.right * worldWidth * (nIterations-1);

        // Se a cidade já cresceu o bastante para terminar o jogo, cria a igreja
        if (Vector3.Distance(beginnigOfWorld.position, currentCityEndPos) >= distanceToWin)
        {
            if (!hasCreatedChurch)
            {
                CreateChurch();
                hasCreatedChurch = true;
            }
        }
        
        // Se o jogador se aproximar do fim da cidade, gera novos objetos
        else if (Vector3.Distance(player.position, currentCityEndPos) < 50)
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
                        new Quaternion(0, 0, 0, 0), this.transform);
            
        }

        nIterations++;
    }

    private void CreateChurch()
    {
        // Instancia a igreja
        Transform instance = Instantiate(igrejaPrefab,
            igrejaPrefab.position + new Vector3(currentCityEndPos.x, 0, 0),
            new Quaternion(0, 0, 0, 0),
            cenaPrincipal.transform);

        // Reposiciona a igreja, para ficar certo
        float igrejaExtent = instance.GetComponent<BoxCollider2D>().bounds.extents.x;
        instance.position = new Vector3 (instance.position.x + igrejaExtent + 1.75f, instance.position.y, instance.position.z);
    }
}
