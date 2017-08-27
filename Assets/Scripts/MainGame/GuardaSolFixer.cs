using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardaSolFixer : MonoBehaviour
{
    private Vector3 startPos;

	void Start ()
    {
        startPos = transform.position;
	}

    private void FixedUpdate()
    {
        if (transform.position.x != startPos.x)
        {
            transform.position = new Vector3(startPos.x, transform.position.y, transform.position.z);
        }
    }
}
