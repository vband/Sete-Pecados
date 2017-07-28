using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightBehaviour : MonoBehaviour {

    private Transform highlight;
    private float originalScale;
    private float rescaleTime; // Tempo em que a mudança na escala ocorreu

	void Start ()
    {
        Transform[] children = GetComponentsInChildren<Transform>();
        foreach (Transform child in children)
        {
            if (child.name ==  "Highlight")
            {
                highlight = child;
            }
        }
        originalScale = highlight.localScale.x;
        rescaleTime = 0;
	}
	
	void Update ()
    {
        
    }

    public void Rescale(float percent)
    {
        rescaleTime = Time.time;

        if (highlight.localScale.x > 1)
        {
            float newScale = originalScale * percent + 1;
            highlight.localScale = new Vector3(newScale, newScale, 1);
        }
    }

    public float GetScale()
    {
        return highlight.localScale.x;
    }

    public bool IsGreat()
    {
        if (Time.time - rescaleTime > 0.05f)
        {
            return false;
        }

        if (Mathf.Abs(highlight.localScale.x - 1) <= 0.1f)
        {
            return true;
        }
        return false;
    }
}
