using UnityEngine;

public class Personagem : MonoBehaviour
{
    public float velocidade = 0.05f;

    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer spriteRenderer; // NOVO: Para virar o sprite do Santa Claus
    Vector2 direcao;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // NOVO: Pega o SpriteRenderer do Inspector
    }

    void Update()
    {
        direcao.x = Input.GetAxisRaw("Horizontal");
        direcao.y = Input.GetAxisRaw("Vertical");
        direcao = direcao.normalized;

        bool andando = direcao != Vector2.zero;
        anim.SetBool("IsMoving", andando);

        // NOVO: Vira o sprite para a esquerda quando direcao.x < 0 e desvira quando > 0
        if (direcao.x != 0)
        {
            spriteRenderer.flipX = direcao.x < 0;
        }

        if (!andando)
            return;

        if (Mathf.Abs(direcao.x) > Mathf.Abs(direcao.y))
            anim.SetInteger("Direction", direcao.x > 0 ? 1 : 3);
        else
            anim.SetInteger("Direction", direcao.y > 0 ? 2 : 0);
    }

    void FixedUpdate()
    {
        rb.linearVelocity = direcao * velocidade;
    }
}