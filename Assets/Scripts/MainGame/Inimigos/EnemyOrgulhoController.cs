using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOrgulhoController : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public float jumpMaxTime;
    public LayerMask environmentLayer;
    public float chaseDistance; // Distância entre o inimigo e o jogador para que o inimigo comece a andar
    public float stopDistance; // Distância entre o inimigo e o jogador para que o inimigo pare de andar

    private Transform player;
    private Rigidbody2D rb2D;
    private Animator animator;
    //private bool isJumping = false;
    private float jumpTimer;
    private enum State { Chase, Idle, Selfie};
    private State state = State.Idle;

    void Start ()
    {
        player = GameObject.Find("Player").transform;
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (!SceneController.paused)
        {
            /*
            if (isJumping)
            {
                KeepJumping();
            }
            */

            /*
            if (IsStuck())
            {
                //StartJumping();
            }
            */

            if (state == State.Idle)
            {
                // Fica parada estalando os dedos...

                // Checa se o player se aproximou
                float dist = Mathf.Abs(player.position.x - transform.position.x);
                if (dist <= chaseDistance)
                {
                    animator.SetBool("Walking", true);
                    state = State.Chase;
                }
            }

            else if (state == State.Chase)
            {
                float dist = Mathf.Abs(player.position.x - transform.position.x);

                if (dist < chaseDistance)
                {
                    MoveLeft();
                }

                if (dist < stopDistance)
                {
                    if (Random.Range(0f, 100f) < 50f) // 50% de chance para que o inimigo crie uma escada quando se aproxima do player
                    {
                        //StartJumping();
                        animator.SetBool("Walking", false);
                        animator.SetBool("Climbing stairs", true);
                        state = State.Selfie;
                        rb2D.bodyType = RigidbodyType2D.Kinematic;
                        rb2D.velocity = Vector3.zero;
                    }
                    else
                    {
                        animator.SetBool("Walking", false);
                        animator.SetBool("Taking selfie", true);
                        state = State.Selfie;
                    }
                }
            }

            else if (state == State.Selfie)
            {
                // Fica parada tirando selfie...
            }
        }
    }

    private void MoveLeft()
    {
        rb2D.AddForce(Vector2.left * speed * Time.fixedDeltaTime);
    }

    /*
    private void StartJumping()
    {
        if (IsGrounded())
        {
            isJumping = true;
            jumpTimer = 0;
        }
    }

    private void KeepJumping()
    {
        if (jumpTimer < jumpMaxTime)
        {
            rb2D.AddForce(Vector2.up * jumpForce * Time.fixedDeltaTime);
            jumpTimer += Time.fixedDeltaTime;
        }
        else
        {
            isJumping = false;
        }
    }
    

    private bool IsGrounded()
    {
        float distanceToGround = GetComponent<BoxCollider2D>().bounds.extents.y;
        Vector3 origin = transform.position - new Vector3(0, distanceToGround, 0);
        float distance = 0.2f;
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, distance, environmentLayer);

        if (hit.collider != null)
        {
            return true;
        }
        return false;
    }

    private bool IsStuck()
    {
        if (GetComponent<Rigidbody2D>().velocity.x == 0)
        {
            return true;
        }
        return false;
    }

    private bool IsBelowPlatform()
    {
        RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position, Vector2.up, 2.7f, environmentLayer);

        Debug.DrawRay(transform.position, Vector3.up * 2.7f, Color.cyan, 100f);

        if (hit.collider != null)
        {
            return true;
        }
        return false;
    }
    */
}
