using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnController : MonoBehaviour
{
    public Transform mainCamera;
    public float despawnDistance; // Um segmento precisará ficar a esta distância da borda da câmera para ser deletado
    public Transform segmentoInicial;

    private List<Transform> arrayDeSegmentos; // Lista que armazena todos os segmentos da cidade

	void Start ()
    {
        // Inicialização
        arrayDeSegmentos = new List<Transform>();
        arrayDeSegmentos.Add(segmentoInicial);
	}
	
	void Update ()
    {
        // Obtém a posição atual da borda esquerda da câmera
        float cameraBorderPos = mainCamera.position.x - mainCamera.GetComponent<Camera>().rect.width/2;

        // Percorre a lista de segmentos verificando se algum deles deve ser deletado
        foreach (Transform segmento in arrayDeSegmentos)
        {
            if (cameraBorderPos - segmento.position.x > despawnDistance)
            {
                arrayDeSegmentos.Remove(segmento);
                Destroy(segmento.gameObject);
                break;
            }
        }
	}

    // A cada novo segmento criado, ele precisa ser adicionado à lista
    public void AdicionarNovoSegmento(Transform segmento)
    {
        arrayDeSegmentos.Add(segmento);
    }
}
