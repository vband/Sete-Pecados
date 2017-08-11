using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetField3D : MonoBehaviour {

    public GameObject Magnet;
    public float AttractionForce;

	void Update () {
        GetComponent<Rigidbody>().AddForce((Magnet.transform.position - transform.position) * AttractionForce * Time.deltaTime);
	}
}
