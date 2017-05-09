﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public GameObject player;
    public GameObject cam;
    private Vector3 offset;


    public float colDepth = 4f;
    public float zPosition = 0f;
    private Vector2 screenSize;
   
    private Transform leftCollider;
    private Vector3 cameraPos;


    void Start()
    {
        offset = transform.position - player.transform.position;
       
        //Cria o objeto vazio
        leftCollider = new GameObject().transform;
        //Dã nome a ele 
        leftCollider.name = "LeftCollider";
        //Adiciona o colisor
        leftCollider.gameObject.AddComponent<BoxCollider2D>();
        //faz do colisor filho da camera para que se mova junto com ela
        leftCollider.parent = transform;
        //Gera as cordenadas do mundo
        cameraPos = Camera.main.transform.position;
        screenSize.x = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0))) * 0.5f;
        screenSize.y = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height))) * 0.5f;
        //modifica as cordanadas papa equivaler a extremidades   
        leftCollider.localScale = new Vector3(colDepth, screenSize.y * 2, colDepth);
        leftCollider.position = new Vector3(cameraPos.x - screenSize.x - (leftCollider.localScale.x * 0.5f), cameraPos.y, zPosition);



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