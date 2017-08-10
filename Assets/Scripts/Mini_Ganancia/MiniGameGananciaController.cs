using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameGananciaController : MonoBehaviour {

    public GameObject Cam;
    public Text display_Tempo;
    [HideInInspector] public bool isPlaying = false;

    private bool perdeu = false;
    private bool ganhou = false;
    private bool perfect = true;
    [SerializeField] private float tempo;

    // Use this for initialization
    void Start() {
        //inicializa o contador de tempo
        display_Tempo.GetComponent<Text>().text = tempo.ToString("F1");
    }

    // Update is called once per frame
    void Update() {

        if (isPlaying)
        {
            AtualizaTempo();

            if (perdeu)
                Cam.GetComponent<Animator>().SetBool("perdeu", true);
            else if (ganhou && perfect)
                Cam.GetComponent<Animator>().SetBool("perfect", true);
            else if (ganhou)
                Cam.GetComponent<Animator>().SetBool("ganhou", true);
        }
    }

    public void SetPerdeu()
    {
        perdeu = true;
    }

    public bool GetPerdeu()
    {
        return (perdeu | ganhou) ;
    }

    public void SetUnperfect()
    {
        perfect = false;
    }

    private void AtualizaTempo()
    {
        if (tempo < 0)
            ganhou = true;
        else if (!ganhou && !perdeu)
        {
            tempo -= Time.deltaTime;
            display_Tempo.GetComponent<Text>().text = tempo.ToString("F1");
        }
    }
}
