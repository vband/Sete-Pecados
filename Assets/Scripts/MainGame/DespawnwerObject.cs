using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnwerObject : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "car")
            Destroy(collision.gameObject);
    }
}
