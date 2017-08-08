using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MudoMenuPausa : MonoBehaviour
{

    private bool StateInPlayerPrefs;

    // Use this for initialization
    void Awake()
    {
        StateInPlayerPrefs = Convert.ToBoolean(PlayerPrefs.GetInt("MUDO", 0));
    }

    void Start()
    {
        GetComponent<Toggle>().isOn = StateInPlayerPrefs;
        GetComponent<Toggle>().onValueChanged.AddListener(delegate { OnChangeState(); });
    }

    public void OnChangeState()
    {
        StateInPlayerPrefs = GetComponent<Toggle>().isOn;
        PlayerPrefs.SetInt("MUDO", Convert.ToInt32(StateInPlayerPrefs));
    }

}
