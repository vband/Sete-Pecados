using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentoDeCidade : MonoBehaviour
{
    public SpriteRenderer street;

    void Start ()
    {

	}

    public float GetLength()
    {
        return street.size.x * street.transform.localScale.x;
    }
}
