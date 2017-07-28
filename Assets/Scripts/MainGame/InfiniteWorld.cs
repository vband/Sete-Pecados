using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteWorld : MonoBehaviour
{
    public Transform player;
    public Transform segmento1, segmento2, segmento3; // Pacotes que guardam os diferentes segmentos da cidade
    // Ponteiros para o início e o fim dos diferentes segmentos da cidade
    public Transform beginnigOfSection1, endOfSection1, beginnigOfSection2, endOfSection2, beginnigOfSection3, endOfSection3;
    public SceneController cenaPrincipal;
    public Transform igrejaPrefab;

    private float sectionWidth1, sectionWidth2, sectionWidth3; // Largura dos diferentes segmentos da cidade
    //private int nIterations; // Quantidade atual de vezes que o script já gerou novos objetos
    private Vector3 currentCityEndPos;
    private float distanceToWin;
    private bool hasCreatedChurch;
    private int nextArrayPointer, prevArrayPointer; // Variável que guarda qual array de objetos está sendo usado para criar a cidade
    private List<int> arrayIndexes; // Os índices dos arrays de objetos: 1, 2 ou 3

	void Start ()
    {
        sectionWidth1 = endOfSection1.position.x - beginnigOfSection1.position.x;
        sectionWidth2 = endOfSection2.position.x - beginnigOfSection2.position.x;
        sectionWidth3 = endOfSection3.position.x - beginnigOfSection3.position.x;
        distanceToWin = cenaPrincipal.distanceToWin;
        hasCreatedChurch = false;
        arrayIndexes = new List<int>() {1, 2, 3};
        nextArrayPointer = GetNextIndex(arrayIndexes, 1);
        currentCityEndPos = endOfSection1.position;
        Debug.DrawLine(currentCityEndPos + Vector3.down, currentCityEndPos + Vector3.up, Color.blue, 300f);
    }

    void Update ()
    {
        // Se a cidade já cresceu o bastante para terminar o jogo, cria a igreja
        if (Vector3.Distance(beginnigOfSection1.position, currentCityEndPos) >= distanceToWin)
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

        Debug.DrawLine(currentCityEndPos + Vector3.down, currentCityEndPos + Vector3.up, Color.blue);
	}

    private void generateMoreObjects()
    {
        Transform instance;
        // Instancia todos os objetos que foram preparados antecipadamente
        switch (nextArrayPointer)
        {
            case 1:
                instance = Instantiate(segmento1, segmento1.position + currentCityEndPos + new Vector3(9.5f, 0),
                    new Quaternion(0, 0, 0, 0), this.transform);
                instance.gameObject.SetActive(true);
                GetComponent<DespawnController>().AdicionarNovoSegmento(instance);

                // Recalcula o fim da cidade
                currentCityEndPos = currentCityEndPos + Vector3.right * sectionWidth1;
                break;

            case 2:
                instance = Instantiate(segmento2, segmento2.position + currentCityEndPos + new Vector3(8.64f, 0),
                    new Quaternion(0, 0, 0, 0), this.transform);
                instance.gameObject.SetActive(true);
                GetComponent<DespawnController>().AdicionarNovoSegmento(instance);

                // Recalcula o fim da cidade
                currentCityEndPos = currentCityEndPos + Vector3.right * sectionWidth2;
                break;

            case 3:
                instance = Instantiate(segmento3, segmento3.position + currentCityEndPos + new Vector3(10, 0),
                    new Quaternion(0, 0, 0, 0), this.transform);
                instance.gameObject.SetActive(true);
                GetComponent<DespawnController>().AdicionarNovoSegmento(instance);

                // Recalcula o fim da cidade
                currentCityEndPos = currentCityEndPos + Vector3.right * sectionWidth3;
                break;
        }

        // Muda o array que será usado na próxima vez
        // Anota qual foi o último usado, pois esse não poderá repetir
        prevArrayPointer = nextArrayPointer;
        // Sorteia um índice novo, que será diferente do último usado
        nextArrayPointer = GetNextIndex(arrayIndexes, prevArrayPointer);
        Debug.DrawLine(currentCityEndPos + Vector3.down, currentCityEndPos + Vector3.up, Color.blue, 300f);
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
        instance.position = new Vector3 (instance.position.x + igrejaExtent + 0.68f, instance.position.y, instance.position.z);
        instance.GetComponent<IgrejaBehaviour>().cenaPrincipal = cenaPrincipal;

        // Nomeia o objeto
        instance.gameObject.name = "Igreja";

        // Atualiza o fim da cidade
        float igrejaWidth = instance.GetComponent<BoxCollider2D>().bounds.size.x;
        currentCityEndPos += new Vector3(igrejaWidth + 0.68f * 2, 0, 0);

        // Cria prédios ao lado da igreja (segmento 1)
        instance = Instantiate(segmento1, segmento1.position + currentCityEndPos + new Vector3(9.5f, 0),
            new Quaternion(0, 0, 0, 0), this.transform);
        instance.gameObject.SetActive(true);
    }

    private int GetNextIndex(List<int> list, int lastIndex)
    {
        int nextIndex;
        list.Remove(lastIndex);
        nextIndex = list[Random.Range(0, list.Count)];
        list.Add(lastIndex);
        return nextIndex;
    }
}
