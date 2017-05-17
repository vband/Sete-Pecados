using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aguaBentaPowerUp : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Environment"), LayerMask.NameToLayer("AguaBentaPowerUp"));
        
    }
	
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if(collision.gameObject.name == "Enemy1" ||
           collision.gameObject.name == "Enemy1(Clone)")
        {
            
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }

    }
}
