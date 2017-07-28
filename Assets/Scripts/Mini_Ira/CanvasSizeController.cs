using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasSizeController : MonoBehaviour
{
    // Esse script apenas ajusta o tamanho do canvas para ser
    // igual ao tamanho da câmera

    public Camera mainCamera;

	void Start ()
    {
        // Obtém o tamanho da tela
        Vector2 screenSize;
        screenSize.y = mainCamera.orthographicSize * 2;
        screenSize.x = mainCamera.aspect * screenSize.y;

        // Seta o tamanho do canvas para ser igual ao tamanho da tela
        GetComponent<RectTransform>().sizeDelta = screenSize;
    }

    void Update ()
    {
		
	}
}
