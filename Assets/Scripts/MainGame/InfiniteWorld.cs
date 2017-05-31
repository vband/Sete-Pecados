using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteWorld : MonoBehaviour
{
    public Transform player;
    public Transform[] objectsToGenerate1; // 1o array de objetos que serão gerados
    public Transform[] objectsToGenerate2; // 2o array de objetos que serão gerados
    public Transform[] objectsToGenerate3; // 3o array de objetos que serão gerados
    public Transform beginnigOfWorld, endOfWorld; // Ponteiros para o início e o fim da cidade no momento inicial do jogo
    public SceneController cenaPrincipal;
    public Transform igrejaPrefab;

    private float worldWidth; // Largura inicial da cidade
    private int nIterations; // Quantidade atual de vezes que o script já gerou novos objetos
    private Vector3 currentCityEndPos;
    private float distanceToWin;
    private bool hasCreatedChurch;
    private int arrayPointer; // Variável que guarda qual array de objetos está sendo usado para criar a cidade
    private List<int> arrayIndexes; // Os índices dos arrays de objetos: 1, 2 ou 3

	void Start ()
    {
        //Calcula a largura inicial da cidade
        worldWidth = endOfWorld.position.x - beginnigOfWorld.position.x;
        nIterations = 1;
        distanceToWin = cenaPrincipal.distanceToWin;
        hasCreatedChurch = false;
        arrayIndexes = new List<int>() {1, 2, 3};
        arrayPointer = 1;
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
        switch(arrayPointer)
        {
            case 1:
                for (int i = 0; i < objectsToGenerate1.Length; i++)
                {
                    Instantiate(objectsToGenerate1[i], objectsToGenerate1[i].position + Vector3.right * worldWidth * nIterations,
                                new Quaternion(0, 0, 0, 0), this.transform);

                }
                break;

            case 2:
                for (int i = 0; i < objectsToGenerate2.Length; i++)
                {
                    Instantiate(objectsToGenerate2[i], objectsToGenerate2[i].position + Vector3.right * worldWidth * nIterations,
                                new Quaternion(0, 0, 0, 0), this.transform);

                }
                break;

            case 3:
                for (int i = 0; i < objectsToGenerate3.Length; i++)
                {
                    Instantiate(objectsToGenerate3[i], objectsToGenerate3[i].position + Vector3.right * worldWidth * nIterations,
                                new Quaternion(0, 0, 0, 0), this.transform);

                }
                break;
        }

        nIterations++;

        // Muda o array que será usado na próxima vez
        // Anota qual foi o último usado, pois esse não poderá repetir
        int lastIndex = arrayPointer;
        // Remove o último usado da lista
        arrayIndexes.Remove(lastIndex);
        // Sorteia um índice novo, dentre os que não foram usados na última vez
        arrayPointer = arrayIndexes[Random.Range(0, arrayIndexes.Count)];
        // Devolve o índice que tinha sido removido à lista
        arrayIndexes.Add(lastIndex);
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
        instance.position = new Vector3 (instance.position.x + igrejaExtent + 0.75f, instance.position.y, instance.position.z);
        instance.GetComponent<IgrejaBehaviour>().cenaPrincipal = cenaPrincipal;
    }
}
