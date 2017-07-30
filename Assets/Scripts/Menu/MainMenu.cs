using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;
using System;

public class MainMenu : MonoBehaviour
{
    public Text titulo;
    [Space(20)]
    public Button BotaoJogar;
    public Button BotaoOpcoes;
    public Button BotaoSair;
    [Space(20)]
    public Button BotaoClassico;
    public Button BotaoMinigames;
    public Button BotaoSemFim;
    public Text DescricaoClassico;
    public Text DescricaoMinigames;
    public Text DescricaoSemFim;
    public Button BotaoVoltarModoDeJogo;
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
    [Space(20)]
    public Image LogoGDP;

    private float VOLUME;
    private int Menu, InputKey;
    private bool MudoKey;
    private const int INICIAL = 0, OPCOES = 1, MODODEJOGO = 2, CREDITOS = 3;
    private const int JOYSTICK = 0, VIRTUAL = 1;

    private void Awake() //nao remover o conteudo desse awake, sob pena de parar de funcionar
    {
        //=============== FAZ A LEITURA DOS PREFS E SALVA EM UMA VARIAVEL ===========//
        VOLUME = PlayerPrefs.GetFloat("VOLUME", 1);
        MudoKey = Convert.ToBoolean(PlayerPrefs.GetInt("MUDO", 0));
        InputKey = PlayerPrefs.GetInt("INPUTCONFIG", 0);
    }

    void Start()
    {
        Menu = INICIAL;

        Cursor.visible = true;
        Time.timeScale = 1;

        //=============== CARREGA VALORES NOS BOTOES ===========//

        SliderVolume.value = VOLUME;

        CaixaVolumeMudo.isOn = MudoKey;
        AudioListener.pause = MudoKey;

        if (InputKey == JOYSTICK)
        {
            Joystick.isOn = true;
        }
        else if (InputKey == VIRTUAL)
        {
            BotaoVirtual.isOn = true;
        }

        AtualizaMenu(Menu);

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

                BotaoClassico.gameObject.SetActive(false);
                BotaoMinigames.gameObject.SetActive(false);
                BotaoSemFim.gameObject.SetActive(false);
                DescricaoClassico.gameObject.SetActive(false);
                DescricaoMinigames.gameObject.SetActive(false);
                DescricaoSemFim.gameObject.SetActive(false);
                BotaoVoltarModoDeJogo.gameObject.SetActive(false);
                TextoVolume.gameObject.SetActive(false);
                SliderVolume.gameObject.SetActive(false);
                CaixaVolumeMudo.gameObject.SetActive(false);
                BotaoVoltarOpcoes.gameObject.SetActive(false);
                BotaoCreditos.gameObject.SetActive(false);
                ConteudoDosCreditos.SetActive(false);
                BotaoVoltarCreditos.gameObject.SetActive(false);
                Joystick.gameObject.SetActive(false);
                BotaoVirtual.gameObject.SetActive(false);
                LogoGDP.gameObject.SetActive(false);
                break;
            case MODODEJOGO:
                titulo.gameObject.SetActive(true);

                BotaoJogar.gameObject.SetActive(false);
                BotaoOpcoes.gameObject.SetActive(false);
                BotaoSair.gameObject.SetActive(false);

                BotaoClassico.gameObject.SetActive(true);
                BotaoMinigames.gameObject.SetActive(true);
                BotaoSemFim.gameObject.SetActive(true);
                DescricaoClassico.gameObject.SetActive(true);
                DescricaoMinigames.gameObject.SetActive(true);
                DescricaoSemFim.gameObject.SetActive(true);
                BotaoVoltarModoDeJogo.gameObject.SetActive(true);

                TextoVolume.gameObject.SetActive(false);
                SliderVolume.gameObject.SetActive(false);
                CaixaVolumeMudo.gameObject.SetActive(false);
                BotaoVoltarOpcoes.gameObject.SetActive(false);
                BotaoCreditos.gameObject.SetActive(false);
                ConteudoDosCreditos.SetActive(false);
                BotaoVoltarCreditos.gameObject.SetActive(false);
                Joystick.gameObject.SetActive(false);
                BotaoVirtual.gameObject.SetActive(false);
                LogoGDP.gameObject.SetActive(false);
                break;

            case OPCOES:
                titulo.gameObject.SetActive(true);

                BotaoJogar.gameObject.SetActive(false);
                BotaoOpcoes.gameObject.SetActive(false);
                BotaoSair.gameObject.SetActive(false);
                BotaoClassico.gameObject.SetActive(false);
                BotaoMinigames.gameObject.SetActive(false);
                BotaoSemFim.gameObject.SetActive(false);
                DescricaoClassico.gameObject.SetActive(false);
                DescricaoMinigames.gameObject.SetActive(false);
                DescricaoSemFim.gameObject.SetActive(false);
                BotaoVoltarModoDeJogo.gameObject.SetActive(false);

                TextoVolume.gameObject.SetActive(true);
                SliderVolume.gameObject.SetActive(true);
                CaixaVolumeMudo.gameObject.SetActive(true);
                BotaoVoltarOpcoes.gameObject.SetActive(true);
                BotaoCreditos.gameObject.SetActive(true);

                ConteudoDosCreditos.SetActive(false);
                BotaoVoltarCreditos.gameObject.SetActive(false);
#if UNITY_ANDROID
                Joystick.gameObject.SetActive(true);
                BotaoVirtual.gameObject.SetActive(true);
                LogoGDP.gameObject.SetActive(false);
#elif UNITY_STANDALONE
                Joystick.gameObject.SetActive(false);
                BotaoVirtual.gameObject.SetActive(false);
                LogoGDP.gameObject.SetActive(true);
#endif
                break;
            case CREDITOS:
                titulo.gameObject.SetActive(false);
                BotaoJogar.gameObject.SetActive(false);
                BotaoOpcoes.gameObject.SetActive(false);
                BotaoSair.gameObject.SetActive(false);
                BotaoClassico.gameObject.SetActive(false);
                BotaoMinigames.gameObject.SetActive(false);
                BotaoSemFim.gameObject.SetActive(false);
                DescricaoClassico.gameObject.SetActive(false);
                DescricaoMinigames.gameObject.SetActive(false);
                DescricaoSemFim.gameObject.SetActive(false);
                BotaoVoltarModoDeJogo.gameObject.SetActive(false);
                TextoVolume.gameObject.SetActive(false);
                SliderVolume.gameObject.SetActive(false);
                CaixaVolumeMudo.gameObject.SetActive(false);
                BotaoVoltarOpcoes.gameObject.SetActive(false);
                BotaoCreditos.gameObject.SetActive(false);

                ConteudoDosCreditos.SetActive(true);
                BotaoVoltarCreditos.gameObject.SetActive(true);

                Joystick.gameObject.SetActive(false);
                BotaoVirtual.gameObject.SetActive(false);
                LogoGDP.gameObject.SetActive(false);
                break;
            default: //carrega inicial, caso receba um parametro invalido
                titulo.gameObject.SetActive(true);
                BotaoJogar.gameObject.SetActive(true);
                BotaoOpcoes.gameObject.SetActive(true);
                BotaoSair.gameObject.SetActive(true);

                BotaoClassico.gameObject.SetActive(false);
                BotaoMinigames.gameObject.SetActive(false);
                BotaoSemFim.gameObject.SetActive(false);
                DescricaoClassico.gameObject.SetActive(false);
                DescricaoMinigames.gameObject.SetActive(false);
                DescricaoSemFim.gameObject.SetActive(false);
                BotaoVoltarModoDeJogo.gameObject.SetActive(false);
                TextoVolume.gameObject.SetActive(false);
                SliderVolume.gameObject.SetActive(false);
                CaixaVolumeMudo.gameObject.SetActive(false);
                BotaoVoltarOpcoes.gameObject.SetActive(false);
                BotaoCreditos.gameObject.SetActive(false);
                ConteudoDosCreditos.SetActive(false);
                BotaoVoltarCreditos.gameObject.SetActive(false);
                Joystick.gameObject.SetActive(false);
                BotaoVirtual.gameObject.SetActive(false);
                LogoGDP.gameObject.SetActive(false);
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
        MudoKey = CaixaVolumeMudo.isOn;
        AudioListener.pause = MudoKey;
        SalvarPreferencias();
    }

    public void AtualizaInput(int config)
    {
        if (config == JOYSTICK)
        {
            InputKey = JOYSTICK;
        }
        else if (config == VIRTUAL)
        {
            InputKey = VIRTUAL;
        }

        SalvarPreferencias();
    }

    //=========VOID DE SALVAMENTO==========//
    public void SalvarPreferencias() //chamar sempre que alterar opcoes
    {
        
        PlayerPrefs.SetInt("MUDO", Convert.ToInt32(MudoKey));
        PlayerPrefs.SetFloat("VOLUME", SliderVolume.value);
        PlayerPrefs.SetInt("INPUTCONFIG", InputKey);

        PlayerPrefs.Save();
    }

    public void Jogar()
    {
        AtualizaMenu(MODODEJOGO);
    }

    public void Classico()
    {
        LivesController.InitVidas();
        GameObject.Find("FadeImage").GetComponent<FadeController>().CallFading("Main");
    }

    public void Minigames()
    {

    }

    public void SemFim()
    {

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

    public void VoltarModoDeJogo()
    {
        AtualizaMenu(INICIAL);
    }

    public void Sair()
    {
        Application.Quit();
    }
}