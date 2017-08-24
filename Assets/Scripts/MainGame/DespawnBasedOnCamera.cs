using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Esse script serve para despawnar objetos que possuem exatamente 1 sprite
// Quando o objeto sai do quadro da câmera, ele é destruído após um tempo pré-determinado

public class DespawnBasedOnCamera : MonoBehaviour
{
    public float maxTime = 5f; // Tempo que leva pra destruir
    private float timer = -1; // Tempo atual do cronômetro (-1 significa que o cronômetro não foi iniciado)
    private bool hasAppeared = false, hasDisappeared = false;
    private SpriteRenderer spriteRenderer;

	void Start ()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
	
	void Update ()
    {
		if (!hasAppeared && spriteRenderer.isVisible)
        {
            // Objeto apareceu na tela
            hasAppeared = true;
        }

        if (!hasDisappeared && hasAppeared && !spriteRenderer.isVisible)
        {
            // Objeto desapareceu da tela, logo, inicia o cronômetro
            timer = maxTime;
            hasDisappeared = true;
        }

        if (timer >= 0)
        {
            // Cronômetro prossegue
            timer -= Time.deltaTime;

            if (timer < 0)
            {
                // Tempo acabou
                Destroy(gameObject);
            }
        }
	}
}
