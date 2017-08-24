using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aguaBentaPowerUp : MonoBehaviour {

    public float AutoDestroyTimer;

	private void Update()
    {
        rotaciona();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemies") || collision.gameObject.layer == LayerMask.NameToLayer("Invejoso"))
        {
            transform.parent.GetComponent<PlayerMovement>().sobeCarinha();
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

    private void rotaciona()
    {
        if (transform.parent.GetComponent<PlayerMovement> ().inverte == true) //esquerda
        {
            this.transform.localPosition = new Vector3(1.3f, -0.409f, 0);
            this.transform.rotation = Quaternion.AngleAxis(-180, Vector3.forward);
        }
        else if (transform.parent.GetComponent<PlayerMovement>().inverte == false) //direita
        {
            this.transform.localPosition = new Vector3(- 1.3f, 0.24f, 0);
            this.transform.rotation = Quaternion.AngleAxis(0, Vector3.forward);
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
