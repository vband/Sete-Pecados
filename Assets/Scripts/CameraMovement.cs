using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
    public GameObject player;

    private Vector3 offset;
    private float horizontalInput;

    
    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    void Update(){}

    private void LateUpdate()
    {
        movimentacao();
    }

    void movimentacao()
    {
        transform.position = player.transform.position + offset;
    }
}
