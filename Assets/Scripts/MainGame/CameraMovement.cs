using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraMovement : MonoBehaviour
{
    public float cameraSpeed;
    public GameObject player;
    public GameObject cenaPrincipal;
    public GameObject Limite_direita;

    //private Vector3 offset;
    
    public float colDepth = 4f;
    public float zPosition = 0f;
    [HideInInspector] public Vector2 screenSize;

    private Transform leftCollider;
    private Vector3 cameraPos;
    private Rigidbody2D rb;
    private float yPosition;


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
        leftCollider.gameObject.AddComponent<Rigidbody2D>();
        leftCollider.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        //faz do colisor filho da camera para que se mova junto com ela
        leftCollider.parent = transform;
        //dá a layer de "LeftCollider" ao colisor
        leftCollider.gameObject.layer = LayerMask.NameToLayer("LeftCollider");
        // Dá a tag de "LeftCollider" ao colisor
        leftCollider.gameObject.tag = "LeftCollider";
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

        yPosition = transform.position.y;
    }

    private void Update()
    {
        //DisableListener();
    }

    private void FixedUpdate()
    {
        
    }

    void LateUpdate()
    {
        if (SceneController.paused == false)
        {
            Movimentacao();
        }
        // Trava a coordenada y da câmera
        transform.position = new Vector3(transform.position.x, yPosition, transform.position.z);
    }

    void Movimentacao()
    {

        // Variáveis locais
        float actualCameraSpeed, playerPositionRelativeToCamera, t, speedModifier;
        float defaultSpeedModifier = 1.15f;
        float maxSpeedModifier = 3f;
        float minSpeedModifier = 0.3f;

        // Se o jogador estiver à direita do centro da tela
        if (player.transform.position.x > transform.position.x)
        {
            // Determina que quanto mais perto da borda direita, mais rápido a câmera irá se mover
            playerPositionRelativeToCamera = player.transform.position.x - transform.position.x;
            t = playerPositionRelativeToCamera / screenSize.x;
            speedModifier = maxSpeedModifier * t + defaultSpeedModifier * (1 - t);
        }

        // Se o jogador estiver à esquerda do centro da tela
        else if (player.transform.position.x < transform.position.x)
        {
            // Determina que quanto mais perto da borda esquerda, mais lento a câmera irá se mover
            playerPositionRelativeToCamera = transform.position.x - player.transform.position.x;
            t = playerPositionRelativeToCamera / screenSize.x;
            speedModifier = minSpeedModifier * t + defaultSpeedModifier * (1 - t);
        }

        // Se o jogador estiver no centro da tela
        else
        {
            // A câmera se move à mesma velocidade do jogador
            speedModifier = defaultSpeedModifier;
        }

        actualCameraSpeed = cameraSpeed * speedModifier;

        // Movimenta a câmera
        Vector2 pos = (Vector2) transform.position;
        Vector2 speed = new Vector2(actualCameraSpeed, 0);
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