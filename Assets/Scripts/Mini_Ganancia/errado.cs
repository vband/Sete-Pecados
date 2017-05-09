using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class errado : MonoBehaviour
{

    private string menu;

    [Space(20)]
    public Image black;
    public Animator fade = new Animator();

    // Use this for initialization
    void Start()
    {
        menu = "MainMenu";

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Errado()
    {

        StartCoroutine(fading(menu));

    }

    IEnumerator fading(string NextScene)
    {
        fade.SetBool("Fade", true);
        yield return new WaitUntil(() => black.color.a == 1);
        SceneManager.LoadScene(NextScene);
    }
}
