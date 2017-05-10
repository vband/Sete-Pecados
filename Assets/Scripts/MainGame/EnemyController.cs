﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public Transform player;
    public float speed;

    private Rigidbody2D rb2D;
    private SpriteRenderer sprite;


    [Space(20)]
    public Image black;
    public Animator fade;

    void Start ()
    {
        rb2D = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
	}
	
	void Update ()
    {
		// Verifica se o player está para a direita
        if (player.position.x > transform.position.x)
        {
            // Anda para a direita
            rb2D.AddForce(Vector2.right * speed);
            sprite.flipX = false;
        }
        // Verifica se o player está para a esquerda
        else if (player.position.x < transform.position.x)
        {
            // Anda para a esquerda
            rb2D.AddForce(Vector2.left * speed);
            sprite.flipX = true;
        }
    }

    // Essa função é chamada quando o inimigo colidir
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Se o inimigo colidir com o player...
        if (collision.gameObject.tag == "Player")
        {
            // se destrói
            //gameObject.SetActive(false);

            //SceneManager.LoadScene("Ira");
            StartCoroutine(fading("Ira"));
        }
    }

    IEnumerator fading(string NextScene)
    {
        fade.SetBool("Fade", true);
        yield return new WaitUntil(() => black.color.a == 1);
        SceneManager.LoadScene(NextScene);
    }

}