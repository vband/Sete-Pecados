using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public GameObject player;
    public GameObject cam;
    public EdgeCollider2D EC2D;
    private Vector3 offset;
    private Vector3 screenDimensionsInWorldUnits;


    void Start()
    {
        offset = transform.position - player.transform.position;
        screenDimensionsInWorldUnits = cam.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(
            Screen.width, Screen.height, 0));
       
    }

    void Update(){
       
    }

    private void LateUpdate()
    {
        movimentacao();
    }

    void movimentacao()
    {
        

        // Se o jogador se mover para a direita
        if (player.transform.position.x > (cam.transform.position.x - offset.x))
        {
            transform.position = player.transform.position + offset;
        }
        // Se o jogador se mover para a esquerda ou ficar parado
        else
        {
            transform.position = new Vector3(cam.transform.position.x, player.transform.position.y + offset.y, offset.z);  
        }
        



    }
}
