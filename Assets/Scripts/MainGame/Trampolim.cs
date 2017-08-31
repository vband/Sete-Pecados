using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampolim : MonoBehaviour
{ 
    public float forca;
    public LayerMask playerLayer;

    //private Vector3 origin;
    private Color color;

    private void Start()
    {
        //origin = GetComponentsInParent<Transform>()[0].position;
        color = Color.blue;
    }

    private void FixedUpdate()
    {
        BouncePlayer();
    }

    // Se o jogador estiver pisando em cima do guarda-sol, joga ele pra cima
    private void BouncePlayer()
    {
        BoxCollider2D collider = GetComponent<BoxCollider2D>();

        RaycastHit2D hitCenter, hitLeft, hitRight;

        //float distToTop = collider.bounds.extents.y;
        float distToSide = collider.bounds.extents.x - 0.1f;

        Vector3 originCenter = transform.position + new Vector3(0, 0, 0);
        Vector3 originLeft = transform.position + new Vector3(-distToSide, 0, 0);
        Vector3 originRight = transform.position + new Vector3(distToSide, 0, 0);

        float distance = 0.8f;

        //Faz três raycasts para saber se o jogador está em cima do guarda-sol
        hitCenter = Physics2D.Raycast(originCenter, Vector2.up, distance, playerLayer);
        hitLeft = Physics2D.Raycast(originLeft, Vector2.up, distance, playerLayer);
        hitRight = Physics2D.Raycast(originRight, Vector2.up, distance, playerLayer);
        Debug.DrawRay(originCenter, Vector3.up * distance, color);
        Debug.DrawRay(originLeft, Vector3.up * distance, color);
        Debug.DrawRay(originRight, Vector3.up * distance, color);



        // Testa se algum dos raycasts acertou o jogador
        if (hitLeft.collider != null || hitCenter.collider != null || hitRight.collider != null)
        {
            Collider2D bouncingObject = null;
            if (hitLeft.collider != null)
            {
                bouncingObject = hitLeft.collider;
                color = Color.green;
            }
            else if (hitCenter.collider != null)
            {
                bouncingObject = hitCenter.collider;
                color = Color.green;
            }
            else if (hitRight.collider != null)
            {
                bouncingObject = hitRight.collider;
                color = Color.green;
            }
            else
            {
                color = Color.blue;
            }

            // Lança o jogador para o alto
            if (bouncingObject.gameObject.GetComponent<PlayerMovement>().isJumping == false
                && !bouncingObject.gameObject.GetComponent<PlayerMovement>().isCollidingWithScreenBorder)
            {
                bouncingObject.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, forca));
            }

            if (bouncingObject.gameObject.GetComponent<PlayerMovement>().isJumping == false
                && bouncingObject.gameObject.GetComponent<PlayerMovement>().isCollidingWithScreenBorder)
            {
                bouncingObject.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, forca/10));
            }
        }
    }
}
