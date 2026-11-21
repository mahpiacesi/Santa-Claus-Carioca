using UnityEngine;

public class Coletavel : MonoBehaviour
{
    private GerenciadorJogo gerenciadorJogo;

    void Start()
    {
        gerenciadorJogo = FindFirstObjectByType<GerenciadorJogo>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            gerenciadorJogo.Pontuar();
            Destroy(gameObject);
        }
    }
}