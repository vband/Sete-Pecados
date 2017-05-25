﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraMovement : MonoBehaviour
{
    public float cameraSpeed;
    public GameObject player;
    public GameObject Limite_direita;

    //private Vector3 offset;
    
    public float colDepth = 4f;
    public float zPosition = 0f;
    [HideInInspector] public Vector2 screenSize;

    private Transform leftCollider;
    private Vector3 cameraPos;
    private Rigidbody2D rb;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //offset = transform.position - player.transform.position;

        //Cria o objeto vazio
        leftCollider = new GameObject().transform;
        //Dã nome a ele 
        leftCollider.name = "LeftCollider";
        //Adiciona o colisor
        leftCollider.gameObject.AddComponent<BoxCollider2D>();
        //faz do colisor filho da camera para que se mova junto com ela
        leftCollider.parent = transform;
        //dá a layer de "LeftCollider" ao colisor
        leftCollider.gameObject.layer = LayerMask.NameToLayer("LeftCollider");
        //determina que as colisões entre os inimigos e o leftCollider serão ignoradas
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemies"), LayerMask.NameToLayer("LeftCollider"));
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Environment"), LayerMask.NameToLayer("LeftCollider"));
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("PowerUps"), LayerMask.NameToLayer("LeftCollider"));
        //Gera as cordenadas do mundo
        cameraPos = Camera.main.transform.position;
        screenSize.x = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0))) * 0.5f;
        screenSize.y = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height))) * 0.5f;
        //modifica as cordanadas papa equivaler a extremidades   
        leftCollider.localScale = new Vector3(colDepth, screenSize.y * 2, colDepth);
        leftCollider.position = new Vector3(cameraPos.x - screenSize.x - (leftCollider.localScale.x * 0.5f), cameraPos.y, zPosition);

        //limite direita
        Limite_direita.transform.position = new Vector3(cameraPos.x + screenSize.x , cameraPos.y, zPosition);



    }

    private void Update()
    {
        DisableListener();
    }

    private void FixedUpdate()
    {
        
    }

    void LateUpdate()
    {
        if (!SceneController.paused)
        {
            Movimentacao();
        }
    }

    void Movimentacao()
    {
        // Se o jogador se mover para a direita
        /*if (player.transform.position.x > (transform.position.x - offset.x))
        {
            transform.position = new Vector3(player.transform.position.x, offset.y, offset.z);
            //transform.position = player.transform.position + offset;
        }
        // Se o jogador se mover para a esquerda ou ficar parado
        else
        {
            transform.position = new Vector3(transform.position.x, offset.y, offset.z);
        }*/

        Vector2 pos = (Vector2) transform.position;
        Vector2 speed = new Vector2(cameraSpeed,0);
        rb.MovePosition(pos + speed);
    }
    
    void DisableListener()
    {
        if (SceneManager.GetActiveScene() != SceneManager.GetSceneByName("Main") ) {
            GetComponent<AudioListener>().enabled = false;
        }
        else
        {
            GetComponent<AudioListener>().enabled = true;
        }
        
    }
}