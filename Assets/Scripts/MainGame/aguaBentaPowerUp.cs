using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aguaBentaPowerUp : MonoBehaviour {

    public float AutoDestroyTimer;

	// Use this for initialization
	void Start () {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Environment"), LayerMask.NameToLayer("AguaBentaPowerUp"));
    }
	
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Enemies"))
        {
            transform.parent.GetComponent<PlayerMovement>().sobeCarinha();
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

    //sem uso, mas pode ser uma possivel implementacao mais elegante
    void AutoDestroyStart(float time)
    {
        AutoDestroy(time);
    }

    IEnumerator AutoDestroy(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
    }
}
