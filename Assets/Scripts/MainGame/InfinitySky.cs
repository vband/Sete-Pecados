using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfinitySky : MonoBehaviour
{

    // Gameobjects usados como referencia
    public GameObject player;
    public GameObject mainCamera;
    // Parâmetro que guarda a proporção entre a velocidade do background e a velocidade do player.
    // Por exemplo: se o parâmetro for igual a 0.5, o background irá se mover à metade da velocidade do player.
    public float bgVelocityRelativeToPlayer;

    // Prefabs para clonar
    public Transform skypf;
    // Variável que armazera a posicao xy do ultimo prefab clonado
    private Vector3 currentPos;
    // Largura da sprite do background
    private float skyWidth;
    // Lista que armazena todos os backgrounds já gerados
    private List<Transform> backgorundList;

    void Start()
    {
        // Inicializa a posição do background inicial
        currentPos = skypf.transform.position;
        // Obtém o tamanho da sprite, em coordenadas de mundo
        skyWidth = skypf.GetComponent<SpriteRenderer>().bounds.size.x;
        // Instancia a primeira imagem do background
        Transform instance = Instantiate(skypf, skypf.position, new Quaternion(0, 0, 0, 0)) as Transform;
        // Insere o backgorund inicial na lista
        backgorundList = new List<Transform>();
        backgorundList.Add(instance);
    }


    void FixedUpdate()
    {
        GenerateNewBackground();
        MoveBackgorund();
        currentPos = backgorundList[backgorundList.Count - 1].position;
    }

    private void GenerateNewBackground()
    {
        //condicionais testam a distancia do ultimo prefab gerado e instancia os novos com base na distancia, garantindo q isso aconteca fora da visao do jogador
        if (Vector3.Distance(player.transform.position, currentPos) < 50)
        {
            currentPos += skyWidth * Vector3.right;
            Transform instance = Instantiate(skypf, currentPos, new Quaternion(0, 0, 0, 0)) as Transform;
            // Insere o novo background na lista
            backgorundList.Add(instance);

            //DEBUG
            //Debug.Log(backgorundList.Count.ToString());
        }
    }

    private void MoveBackgorund()
    {
        float playerVelocityX = player.GetComponent<Rigidbody2D>().velocity.x;

        if ((playerVelocityX < 0)||(player.transform.position.x < mainCamera.transform.position.x))
        {
            playerVelocityX = 0;
        }

        float bgVelocity = playerVelocityX * bgVelocityRelativeToPlayer;

        foreach (Transform bg in backgorundList)
        {
            //bg.GetComponent<Rigidbody2D>().velocity.Set(playerVelocityX * bgVelocityRelativeToPlayer, 0);
            bg.GetComponent<Rigidbody2D>().MovePosition((Vector2)bg.position + Vector2.right * bgVelocity);
        }
    }
}