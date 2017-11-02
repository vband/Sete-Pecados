using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteWorld : MonoBehaviour
{
    public Transform[] segmentosDif1, segmentosDif2, segmentosDif3, segmentosDif4, segmentosDif5;
    public Transform player;
    //public Transform segmento1, segmento2, segmento3; // Pacotes que guardam os diferentes segmentos da cidade
    public Transform pointerToCityBeginning; // Ponteiro para o início da cidade
    public SceneController cenaPrincipal;
    public Transform igrejaPrefab;
    public Transform segmentoInicial;

    private Transform[][] matrizDeSegmentos;
    private int currentDif, lastDifUsed = 0;
    private Vector3 currentCityEndPos;
    private float distanceToWin;
    private bool hasCreatedChurch;
    // Variáveis usadas para não repetir o mesmo segmento duas vezes seguidas
    private int nextArrayPointer, prevArrayPointer;
    private List<int> arrayIndexes;

	void Start ()
    {
        matrizDeSegmentos = new Transform[5][];
        matrizDeSegmentos[0] = segmentosDif1;
        matrizDeSegmentos[1] = segmentosDif2;
        matrizDeSegmentos[2] = segmentosDif3;
        matrizDeSegmentos[3] = segmentosDif4;
        matrizDeSegmentos[4] = segmentosDif5;
        distanceToWin = cenaPrincipal.distanceToWin;
        hasCreatedChurch = false;
        currentCityEndPos = pointerToCityBeginning.position;
    }

    void Update ()
    {
        // Atualiza a dificuldade atual do jogo
        UpdateDifficulty();

        // Se a cidade já cresceu o bastante para terminar o jogo, cria a igreja
        if (Vector3.Distance(pointerToCityBeginning.position, currentCityEndPos) >= distanceToWin && GameMode.Mode == GameMode.GameModes.Classic)
        {
            if (!hasCreatedChurch)
            {
                CreateChurch();
                hasCreatedChurch = true;
            }
        }
        
        // Se o jogador se aproximar do fim da cidade, gera novos objetos
        else if (Vector3.Distance(player.position, currentCityEndPos) < 30)
        {
            GenerateMoreObjects();
        }

        Debug.DrawLine(currentCityEndPos + Vector3.down, currentCityEndPos + Vector3.up, Color.blue);
	}

    private void GenerateMoreObjects()
    {
        int indiceDoSegmento;

        // Se a dificuldade é a mesma da última iteração
        if (lastDifUsed == currentDif)
        {
            // Sorteia o próximo segmento a ser istanciado, sem repetir o último usado
            indiceDoSegmento = GetNextIndex(arrayIndexes, prevArrayPointer);

            // Registra qual segmento acaba de ser instanciado, pois esse não deverá repetir na próxima iteração
            prevArrayPointer = indiceDoSegmento;
        }

        // Se a dificuldade trocou
        else
        {
            // Obtém os índices dos segmentos da nova dificuldade
            arrayIndexes = new List<int>();
            for (int i = 0; i < matrizDeSegmentos[currentDif-1].Length; i++)
            {
                arrayIndexes.Add(i);
            }

            // Sorteia um segmento para instanciar
            indiceDoSegmento = Random.Range(0, matrizDeSegmentos[currentDif - 1].Length);

            // Registra que a atual dificuldade acaba de ser usada para instanciar um segmento
            lastDifUsed = currentDif;

            // Registra qual segmento acaba de ser instanciado, pois esse não deverá repetir na próxima iteração
            prevArrayPointer = indiceDoSegmento;
        }

        // Instancia o novo segmento
        Transform instance = Instantiate(
            matrizDeSegmentos[currentDif - 1][indiceDoSegmento],
            matrizDeSegmentos[currentDif - 1][indiceDoSegmento].position + currentCityEndPos + new Vector3(9.46f, 0f),
            Quaternion.identity,
            this.transform);
        instance.gameObject.SetActive(true);
        GetComponent<DespawnController>().AdicionarNovoSegmento(instance);

        // Recalcula o fim da cidade
        currentCityEndPos = currentCityEndPos + Vector3.right * matrizDeSegmentos[currentDif - 1][indiceDoSegmento].GetComponent<SegmentoDeCidade>().GetLength();
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

        // Cria prédios ao lado da igreja (segmento inicial)
        instance = Instantiate(segmentoInicial, segmentoInicial.position + currentCityEndPos + new Vector3(9.5f, /*0.37f*/1.51f),
            new Quaternion(0, 0, 0, 0), this.transform);
        instance.gameObject.SetActive(true);
    }

    private void UpdateDifficulty()
    {
        if (cenaPrincipal.GetProgress() <= 0.2)
            currentDif = 1;
        else if (cenaPrincipal.GetProgress() <= 0.4)
            currentDif = 2;
        else if (cenaPrincipal.GetProgress() <= 0.6)
            currentDif = 3;
        else if (cenaPrincipal.GetProgress() <= 0.8)
            currentDif = 4;
        else
            currentDif = 5;
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
