using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;

public class MainMenu : MonoBehaviour
{
    public Text titulo;
    [Space(20)]
    public Button BotaoJogar;
    public Button BotaoOpcoes;
    public Button BotaoSair;
    [Space(20)]
    public Text TextoVolume;
    public Slider SliderVolume;
    public Toggle CaixaVolumeMudo;
    public Button BotaoVoltarOpcoes;
    [Space(20)]
    public Button BotaoCreditos;
    public Button BotaoVoltarCreditos;
    public GameObject ConteudoDosCreditos;
    [Space(20)]
    public Toggle Joystick;
    public Toggle BotaoVirtual;

    private float VOLUME;
    private int menu, inputConfig;
    private const int INICIAL = 0, OPCOES = 1, CREDITOS = 2;
    private const int JOYSTICK = 0, VIRTUAL = 1;

    void Start()
    {
        menu = INICIAL;
        AtualizaMenu(menu);

        Cursor.visible = true;
        Time.timeScale = 1;
  
        //=============== CARREGA SALVOS OU DEFINE AS CONFIG PADRAO NO ELSE ===========//
        if (PlayerPrefs.HasKey("VOLUME"))
        {
            VOLUME = PlayerPrefs.GetFloat("VOLUME");
            SliderVolume.value = VOLUME;
        }
        else
        {
            PlayerPrefs.SetFloat("VOLUME", 1);
            SliderVolume.value = 1;
        }

        if (PlayerPrefs.HasKey("MUDO"))
        {
            if(PlayerPrefs.GetInt("MUDO") == 0)
            {
                CaixaVolumeMudo.isOn = false;
                AtualizaMudo();
            }
            else
            {
                CaixaVolumeMudo.isOn = true;
                AtualizaMudo();
            }
        }
        else
        {
            PlayerPrefs.SetInt("MUDO", 0);
            CaixaVolumeMudo.isOn = false;
            AtualizaMudo();
        }

        if (PlayerPrefs.HasKey("INPUTCONFIG"))
        {
            if(PlayerPrefs.GetInt("INPUTCONFIG") == JOYSTICK)
            {
                Joystick.isOn = true;
                AtualizaInput(JOYSTICK);
            } else
            {
                BotaoVirtual.isOn = true;
                AtualizaInput(VIRTUAL);
            }
        }
        else
        {
            PlayerPrefs.SetInt("INPUTCONFIG", JOYSTICK);
            Joystick.isOn = true;
            AtualizaInput(JOYSTICK);
        }

    }
    //========= VOIDS DE ATUALIZACAO ==========//

    private void AtualizaMenu(int menu)
    {

        switch (menu)
        {
            case INICIAL:
                titulo.gameObject.SetActive(true);
                BotaoJogar.gameObject.SetActive(true);
                BotaoOpcoes.gameObject.SetActive(true);
                BotaoSair.gameObject.SetActive(true);

                TextoVolume.gameObject.SetActive(false);
                SliderVolume.gameObject.SetActive(false);
                CaixaVolumeMudo.gameObject.SetActive(false);
                BotaoVoltarOpcoes.gameObject.SetActive(false);
                BotaoCreditos.gameObject.SetActive(false);
                ConteudoDosCreditos.SetActive(false);
                BotaoVoltarCreditos.gameObject.SetActive(false);
                Joystick.gameObject.SetActive(false);
                BotaoVirtual.gameObject.SetActive(false);
                break;
            case OPCOES:
                titulo.gameObject.SetActive(true);

                BotaoJogar.gameObject.SetActive(false);
                BotaoOpcoes.gameObject.SetActive(false);
                BotaoSair.gameObject.SetActive(false);

                TextoVolume.gameObject.SetActive(true);
                SliderVolume.gameObject.SetActive(true);
                CaixaVolumeMudo.gameObject.SetActive(true);
                BotaoVoltarOpcoes.gameObject.SetActive(true);
                BotaoCreditos.gameObject.SetActive(true);

                ConteudoDosCreditos.SetActive(false);
                BotaoVoltarCreditos.gameObject.SetActive(false);

                Joystick.gameObject.SetActive(true);
                BotaoVirtual.gameObject.SetActive(true);
                break;
            case CREDITOS:
                titulo.gameObject.SetActive(false);
                BotaoJogar.gameObject.SetActive(false);
                BotaoOpcoes.gameObject.SetActive(false);
                BotaoSair.gameObject.SetActive(false);

                TextoVolume.gameObject.SetActive(false);
                SliderVolume.gameObject.SetActive(false);
                CaixaVolumeMudo.gameObject.SetActive(false);
                BotaoVoltarOpcoes.gameObject.SetActive(false);
                BotaoCreditos.gameObject.SetActive(false);

                ConteudoDosCreditos.SetActive(true);
                BotaoVoltarCreditos.gameObject.SetActive(true);

                Joystick.gameObject.SetActive(false);
                BotaoVirtual.gameObject.SetActive(false);
                break;
            default: //carrega inicial, caso receba um parametro invalido
                titulo.gameObject.SetActive(true);
                BotaoJogar.gameObject.SetActive(true);
                BotaoOpcoes.gameObject.SetActive(true);
                BotaoSair.gameObject.SetActive(true);

                TextoVolume.gameObject.SetActive(false);
                SliderVolume.gameObject.SetActive(false);
                CaixaVolumeMudo.gameObject.SetActive(false);
                BotaoVoltarOpcoes.gameObject.SetActive(false);
                BotaoCreditos.gameObject.SetActive(false);
                ConteudoDosCreditos.SetActive(false);
                BotaoVoltarCreditos.gameObject.SetActive(false);
                Joystick.gameObject.SetActive(false);
                BotaoVirtual.gameObject.SetActive(false);
                break;
        }
    }

    public void AtualizaVolume()
    {
        AudioListener.volume = SliderVolume.value;
        SalvarPreferencias();
    }

    public void AtualizaMudo()
    {
        AudioListener.pause = CaixaVolumeMudo.isOn;
        SalvarPreferencias();
    }

    public void AtualizaInput(int config)
    {
        if(config == JOYSTICK)
        {
            inputConfig = JOYSTICK;
        } else if (config == VIRTUAL)
        {
            inputConfig = VIRTUAL;
        }

        SalvarPreferencias();
    }

    //=========VOID DE SALVAMENTO==========//
    private void SalvarPreferencias() //chamar sempre que alterar opcoes
    {
        if (CaixaVolumeMudo.isOn == true)
        {
            PlayerPrefs.SetInt("MUDO", 1);
        }
        else
        {
            PlayerPrefs.SetInt("MUDO", 0);
        }

        PlayerPrefs.SetFloat("VOLUME", SliderVolume.value);

        PlayerPrefs.SetInt("INPUTCONFIG", inputConfig);
    }

    public void Jogar()
    {
        LivesController.InitVidas();
        GameObject.Find("FadeImage").GetComponent<FadeController>().CallFading("Main");
    }

    public void Opcoes()
    {
        AtualizaMenu(OPCOES);
    }

    public void MostrarCreditos()
    {
        AtualizaMenu(CREDITOS);
    }

    public void VoltarOpcoes()
    {
        AtualizaMenu(INICIAL);
    }

    public void VoltarCreditos()
    {
        AtualizaMenu(OPCOES);
    }

    public void Sair()
    {
        Application.Quit();
    }
}