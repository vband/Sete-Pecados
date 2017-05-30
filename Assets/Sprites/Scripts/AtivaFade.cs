using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtivaFade : MonoBehaviour {

    public GameObject fadeimage;
    private void Awake()
    {
        fadeimage.SetActive(true);
    }

}
