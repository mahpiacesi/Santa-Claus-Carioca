using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GerenciadorJogo : MonoBehaviour
{
    [SerializeField] private int tempoInicial = 30;
    [SerializeField] private GameObject telaInicio;
    [SerializeField] private GameObject telaFimDeJogo;
    [SerializeField] private GameObject telaSucesso;
    [SerializeField] private TextMeshProUGUI coletadosTexto;
    [SerializeField] private TextMeshProUGUI relogioTexto;

    private int coletaveisQtd;
    private int coletaveisObtidos;
    private int tempoRestante;

    private void Start()
    {
        coletaveisQtd = FindObjectsByType<Coletavel>(FindObjectsSortMode.None).Length;
        tempoRestante = tempoInicial;
        AtualizarTextoColetados();
        AtualizarTextoRelogio();
        telaInicio.SetActive(true);
        telaFimDeJogo.SetActive(false);
        telaSucesso.SetActive(false);
        Time.timeScale = 0f;
    }

    public void IniciarJogo()
    {
        telaInicio.SetActive(false); // Esconde a tela inicial
        Time.timeScale = 1f;         // Faz o jogo rodar
        StartCoroutine(Cronometro()); // Começa a contar o tempo só agora!
    }

    private IEnumerator Cronometro()
    {
        while (tempoRestante > 0)
        {
            AtualizarTextoRelogio();
            yield return new WaitForSeconds(1f);
            tempoRestante--;
        }

        AtualizarTextoRelogio();
        FimDeJogo(false);
    }

    public void Pontuar()
    {
        coletaveisObtidos++;
        AtualizarTextoColetados();

        if (coletaveisObtidos == coletaveisQtd)
        {
            StopAllCoroutines(); // NOVO: Para o cronômetro imediatamente ao vencer
            FimDeJogo(true);     // NOVO: Coletou tudo = true (Vitória)
        }
        
    }

    private void FimDeJogo(bool venceu)
    {
        Time.timeScale = 0f;

        if (venceu)
        {
            telaSucesso.SetActive(true);
        }
        else
        {
            telaFimDeJogo.SetActive(true);
        }
    }

    public void Recomecar()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void AtualizarTextoColetados()
    {
        coletadosTexto.text = coletaveisObtidos + "/" + coletaveisQtd;
    }

    private void AtualizarTextoRelogio()
    {
        relogioTexto.text = tempoRestante / 60 + ":" + (tempoRestante % 60).ToString("00");
    }
}