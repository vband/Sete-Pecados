using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasSizeController : MonoBehaviour
{
    // Esse script apenas ajusta o tamanho do canvas para ser
    // igual ao tamanho da câmera

    public Camera mainCamera;

    public void Start ()
    {
        Vector2 screenSize;

        screenSize.y = mainCamera.orthographicSize * 2;
        screenSize.x = mainCamera.aspect * screenSize.y;
        GetComponent<RectTransform>().sizeDelta = screenSize;
    }
}
