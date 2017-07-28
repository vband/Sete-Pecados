using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CircleFadeController : MonoBehaviour {
    
    public bool ExecutaFade()
    {
        GetComponent<Animator>().SetBool("CircleFade", true);

        StartCoroutine(esperaTerminar());

        GetComponent<Animator>().SetBool("CircleFade", false);

        return true;
    }

    public IEnumerator esperaTerminar()
    {
        GetComponent<Animator>().SetBool("CircleFade", true);
        yield return new WaitUntil(() => GetComponent<RectTransform>().localScale.x > 1190);
        GetComponent<Animator>().SetBool("CircleFade", false);
    }

}
