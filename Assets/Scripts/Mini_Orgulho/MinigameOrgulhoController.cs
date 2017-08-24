using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinigameOrgulhoController : MonoBehaviour
{
    public RectTransform AnuncioPrefab;
    public Canvas canvasAnuncios;
    public List<Sprite> anuncioImagens;
    public Image timeBar;

    public int numberOfAds;
    public float maxTime;

    private float timeLeft;
    private int adsClosed = 0;

	void Start ()
    {
        // Inicialização
        timeLeft = maxTime;

        // Cria os anúncios
        int adIndex = 0;
        for (int i = 0; i < numberOfAds; i++)
        {
            // Instancia
            RectTransform instance = Instantiate(AnuncioPrefab, canvasAnuncios.transform);

            // Define o que acontece quando clicam no botão
            instance.GetComponentInChildren<Button>().onClick.AddListener(delegate { CloseAd(instance.gameObject); });

            // Define a imagem
            instance.GetComponent<Image>().sprite = anuncioImagens[adIndex];
            adIndex = (adIndex + 1) % anuncioImagens.Count;

            // Desloca o anúncio aleatoriamente pela tela
            Vector3 adSize = instance.rect.size;
            Vector2 maxShift = new Vector2(
                canvasAnuncios.GetComponent<RectTransform>().rect.width/2 - adSize.x/2,
                canvasAnuncios.GetComponent<RectTransform>().rect.height/2 - adSize.y/2
            );
            instance.localPosition = new Vector3(
                instance.localPosition.x + Random.Range(-maxShift.x, maxShift.x),
                instance.localPosition.y + Random.Range(-maxShift.y, maxShift.y)
            );
        }
	}
	
	void Update ()
    {
        if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsTag("Play"))
        {
            // Atualiza o tempo
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                timeBar.fillAmount = timeLeft / maxTime;
            }

            // Ganha
            if (adsClosed == numberOfAds)
            {
                Win();
            }

            // Perde
            if (timeLeft <= 0)
            {
                Lose();
            }
        }
    }

    private void Win()
    {
        GetComponent<Animator>().SetTrigger("Win");
        GameObject.Find("Player").GetComponent<PlayerMovement>().StartDelaySobeCarinha();
        GameObject.Find("FadeImage").GetComponent<FadeController>().CallFading("Main");
    }

    private void Perfect()
    {

    }

    private void Lose()
    {
        GetComponent<Animator>().SetTrigger("Lose");
        LivesController.RemVidas();
        GameObject.Find("FadeImage").GetComponent<FadeController>().CallFading("Main");
    }

    // Chamada quando um anúncio é fechado
    public void CloseAd(GameObject ad)
    {
        Destroy(ad);
        adsClosed++;
    }
}
